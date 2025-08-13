using AutoMapper;
using MongoDB.Driver;
using Newtonsoft.Json;
using OsintCommand.API.Dtos;
using OsintCommand.API.Entities;
using OsintCommand.API.Mappings;
using OsintCommand.API.Repositories;
using OsintCommand.API.Response;

namespace OsintCommand.API.Services
{
    public interface IScriptServices
    {
        Task<ApiResponse<ScriptDto>> CreateAsync(ScriptCreateDto dto);
        Task<ApiResponse<PaginatedData<ScriptDto>>> GetAllPaginatedAsync(int page, int pageSize);
        Task<ApiResponse<ScriptDetailDto>> GetDetailByIdAsync(string id);
        Task<ApiResponse<ScriptDto>> UpdateAsync(string id, ScriptUpdateDto dto);
        Task<ApiResponse<PaginatedData<ScriptDto>>> SearchScriptPaginatedAsync(string name, int page, int pageSize);
        Task<ApiResponse<PaginatedData<ActionDto>>> GetActionsByScriptIdAsync(string scriptId, int page, int pageSize);
        Task<ApiResponse<Object>> AddActionToScriptAsync(string scriptId, ActionAddRequestDto dto);
        Task<ApiResponse<object>> UpdateActionInScriptAsync(string scriptId, string actionId, ActionUpdateRequestDto dto);
        Task<ApiResponse<Object>> DeleteActionFromScriptAsync(string scriptId, string actionId);
    }
    public class ScriptServices : IScriptServices
    {
        private readonly IScriptFakeAccountRepository _scriptFakeAccountRepo;
        private readonly IActionRepository _actionRepository;
        private readonly IScriptRepository _scriptRepository;
        private readonly IMapper _mapper;

        public ScriptServices(IScriptFakeAccountRepository scriptFakeAccountRepo, IScriptRepository scriptRepository, IActionRepository actionRepository, IMapper mapper)
        {
            _mapper = mapper;
            _scriptRepository = scriptRepository;
            _actionRepository = actionRepository;
            _scriptFakeAccountRepo = scriptFakeAccountRepo;
        }

        public async Task<ApiResponse<ScriptDto>> CreateAsync(ScriptCreateDto dto)
        {
            string mappedChannel = dto.ChannelCode switch
            {
                1 => "facebook",
                2 => "tiktok",
                3 => "telegram",
                _ => "facebook"
            };
            var entity = _mapper.Map<Script>(dto);
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdateAt = DateTime.UtcNow;
            entity.Channel = mappedChannel;
            await _scriptRepository.AddAsync(entity);

            //var resultDto = _mapper.Map<ScriptDto>(entity);
            return new ApiResponse<ScriptDto>(201, "Script created successfully", null);
        }

        // TODO: number of fake account, number of action
        public async Task<ApiResponse<PaginatedData<ScriptDto>>> GetAllPaginatedAsync(int page, int pageSize)
        {
            var (entities, total) = await _scriptRepository.GetAllPaginatedAsync(page, pageSize);
            var dtos = _mapper.Map<IEnumerable<ScriptDto>>(entities);
            return new ApiResponse<PaginatedData<ScriptDto>>(
                200,
                "Success",
                new PaginatedData<ScriptDto>(dtos, total, page, pageSize)
            );
        }

        public async Task<ApiResponse<ScriptDto>> UpdateAsync(string id, ScriptUpdateDto dto)
        {
            var entity = await _scriptRepository.GetByIdAsync(id);
            if (entity == null)
                return new ApiResponse<ScriptDto>(404, "Script not found", null);

            _mapper.Map(dto, entity);
            string mappedChannel = dto.ChannelCode switch
            {
                1 => "facebook",
                2 => "tiktok",
                3 => "telegram",
                _ => "facebook"
            };
            entity.Channel = mappedChannel;
            entity.UpdateAt = DateTime.UtcNow;
            //entity.UpdatedBy = "admin"; // TODO
            await _scriptRepository.UpdateAsync(id, entity);

            var resultDto = _mapper.Map<ScriptDto>(entity);
            return new ApiResponse<ScriptDto>(200, "Script updated successfully", resultDto);
        }

        public async Task<ApiResponse<ScriptDetailDto>> GetDetailByIdAsync(string id)
        {
            var entity = await _scriptRepository.GetByIdAsync(id);
            if (entity == null)
                new ApiResponse<ScriptDto>(404, "Script not found", null);

            var fakeAccountCount = await _scriptFakeAccountRepo.CountFakeAccountByScriptId(id);
            var actions = await _actionRepository.GetByScriptIdAsync(id);

            var resultDto = _mapper.Map<ScriptDetailDto>(entity);
            resultDto.FakeAccountCount = fakeAccountCount;
            resultDto.Actions = _mapper.Map<List<ActionDto>>(actions);
            resultDto.ActionCount = actions.Count;
            return new ApiResponse<ScriptDetailDto>(200, "Success", resultDto);
        }

        public async Task<ApiResponse<PaginatedData<ActionDto>>> GetActionsByScriptIdAsync(string scriptId, int page, int pageSize)
        {
            var (actions, total) = await _actionRepository.GetByScriptIdAsync(scriptId, page, pageSize);
            var actionDtos = _mapper.Map<List<ActionDto>>(actions);
            return new ApiResponse<PaginatedData<ActionDto>>(
                    200,
                    "Success",
                    new PaginatedData<ActionDto>(actionDtos, total, page, pageSize)
                );
        }

        public async Task<ApiResponse<PaginatedData<ScriptDto>>> SearchScriptPaginatedAsync(string name, int page, int pageSize)
        {

            var (entities, total) = await _scriptRepository.SearchPaginatedAsync(page, pageSize, name);
            var dtos = _mapper.Map<IEnumerable<ScriptDto>>(entities);
            return new ApiResponse<PaginatedData<ScriptDto>>(
                200,
                "Success",
                new PaginatedData<ScriptDto>(dtos, total, page, pageSize)
            );
        }

        public async Task<ApiResponse<object>> AddActionToScriptAsync(string scriptId, ActionAddRequestDto dto)
        {
            //a.Mapping code sang type class
            if (!InteractConfigTypeMapping.Map.TryGetValue(dto.InteractType, out var configType))
                return new ApiResponse<object>(code: 400, message: "Invalid InteractType.", null);

            //b.Deserialize config sang class tương ứng
            object configObj;
            try
            {
                var json = JsonConvert.SerializeObject(dto.Config);
                configObj = JsonConvert.DeserializeObject(json, configType);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<object>(
                    code: 500,
                    message: "An error occurred while parsing config.",
                    data: new { error = ex.Message }
                );
                return response;
            }

            //d.Validate config
            //var context = new ValidationContext(configObj);
            //var results = new List<ValidationResult>();
            //bool isValid = Validator.TryValidateObject(configObj, context, results, true);

            //if (!isValid)
            //    return BadRequest(results);

            //e.Lưu vào MongoDB
            var currentCount = await _actionRepository.GetActionCountByScriptIdAsync(scriptId);

            var entity = _mapper.Map<Entities.Action>(dto);
            entity.ScriptId = scriptId;
            entity.Order = currentCount + 1;

            var normalizedConfig = dto.Config.ToDictionary(
                kv => kv.Key,
                kv => Utils.ConvertJTokenToDotNetObject(kv.Value)
            );
            entity.Config = normalizedConfig;

            if (entity.Name == "")
            {
                entity.Name = Utils.GetEnumDescriptionFromInt<InteractType>(entity.InteractType);
            }
            await _actionRepository.AddAsync(entity);
            return new ApiResponse<object>(201, "Add action successfully.", null);
        }

        public async Task<ApiResponse<object>> UpdateActionInScriptAsync(string scriptId, string actionId, ActionUpdateRequestDto dto)
        {
            // 0) Tìm action cần sửa
            var action = await _actionRepository.GetByIdAsync(actionId);
            if (action is null)
                return new ApiResponse<object>(404, "Action not found.", null);

            if (!string.Equals(action.ScriptId, scriptId, StringComparison.Ordinal))
                return new ApiResponse<object>(400, "Action does not belong to the specified script.", null);

            if (dto.InteractType != null && dto.Config != null)
            {
                // 1) Map/validate InteractType
                if (!InteractConfigTypeMapping.Map.TryGetValue((int)dto.InteractType, out var configType))
                    return new ApiResponse<object>(400, "Invalid InteractType.", null);

                // 2) Parse config theo type tương ứng (như Add)
                object configObj;
                try
                {
                    var json = JsonConvert.SerializeObject(dto.Config);
                    configObj = JsonConvert.DeserializeObject(json, configType);
                }
                catch (Exception ex)
                {
                    return new ApiResponse<object>(
                        code: 500,
                        message: "An error occurred while parsing config.",
                        data: new { error = ex.Message }
                    );
                }

                // 3) (tuỳ chọn) DataAnnotation validate
                // var context = new ValidationContext(configObj);
                // var results = new List<ValidationResult>();
                // if (!Validator.TryValidateObject(configObj, context, results, true))
                //     return new ApiResponse<object>(400, "Invalid config.", results);

                // 4) Chuẩn hoá config -> Dictionary<string, object>
                var normalizedConfig = dto.Config.ToDictionary(
                    kv => kv.Key,
                    kv => Utils.ConvertJTokenToDotNetObject(kv.Value)
                );

                // 5) Cập nhật field
                action.InteractType = (int)dto.InteractType;
                action.Config = normalizedConfig;
            }

            action.Name = dto.Name ?? action.Name;

            // 6) Lưu DB
            await _actionRepository.UpdateAsync(scriptId, action);

            return new ApiResponse<object>(200, "Update action successfully.", null);
        }


        public async Task<ApiResponse<Object>> DeleteActionFromScriptAsync(string scriptId, string actionId)
        {
            var action = await _actionRepository.GetByIdAsync(actionId);
            if (action == null || action.ScriptId != scriptId)
            {
                return new ApiResponse<Object>(404, "Action not found or does not belong to the script.", null);
            }
            await _actionRepository.DeleteAsync(actionId);
            return new ApiResponse<Object>(200, "Action deleted successfully.", null);
        }
    }
}
