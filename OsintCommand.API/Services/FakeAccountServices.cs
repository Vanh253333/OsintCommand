using AutoMapper;
using MongoDB.Driver;
using OfficeOpenXml;
using OsintCommand.API.Contexts;
using OsintCommand.API.Dtos;
using OsintCommand.API.Entities;
using OsintCommand.API.Repositories;
using OsintCommand.API.Response;

namespace OsintCommand.API.Services
{
    public interface IFakeAccountServices
    {
        Task<ApiResponse<PaginatedData<FakeAccountDto>>> GetAllPaginatedAsync(int page, int pageSize);
        Task<ApiResponse<PaginatedData<FakeAccountDto>>> GetFilteredAsync(FakeAccountQueryRequestDto queryRequestDto);
        Task<ApiResponse<FakeAccountDto>> GetByIdAsync(string id);
        Task<ApiResponse<FakeAccountDto>> CreateAsync(FakeAccountCreateDto dto);
        Task<ApiResponse<FakeAccountDto>> UpdateAsync(string id, FakeAccountUpdateDto dto);
        Task<ApiResponse<FakeAccountDto>> DeleteAsync(string id);
        Task<ApiResponse<object>> ImportFileAsync(IFormFile file);
        Task<ApiResponse<PaginatedData<GroupDto>>> GetGroupsByFakeAccountIdAsync(string fakeAccountId, GroupSearchRequestDto dto);
        Task<ApiResponse<PaginatedData<FriendDto>>> GetFriendsByFakeAccountIdAsync(string fakeAccountId, FriendSearchRequestDto dto);
        Task<ApiResponse<object>> AssignScriptsToFakeAccountAsync(string fakeAccountId, List<string> scriptIds);
        Task<ApiResponse<object>> AssignScriptToFakeAccountAsync(string fakeAccountId, string scriptId);
        Task<ApiResponse<PaginatedData<ScriptDto>>> GetScriptByFakeAccountIdAsync(string fakeaccountId, ScriptSearchRequestDto dto);
        Task<ApiResponse<ProxyAssignOneResult>> AssignManualToOneAsync(string accountId, ProxyAssignManualSingleDto dto, CancellationToken ct = default);
        Task<ApiResponse<ProxyAssignBatchResult>> AssignFromGroupBatchAsync(ProxyAssignFromGroupDto dto, CancellationToken ct = default);
        Task<ApiResponse<object>> DetachAsync(string accountId, string proxyId, CancellationToken ct = default);

    }

    public class FakeAccountServices : IFakeAccountServices
    {
        private readonly IFakeAccountRepository _fakeAccountRepository;
        private readonly IGroupFakeAccountRepository _groupFakeAccountRepository;
        private readonly IFriendFakeAccountRepository _friendFakeAccountRepository;
        private readonly IScriptFakeAccountRepository _scriptFakeAccountRepository ;
        private readonly IScriptRepository _scriptRepository;
        private readonly IActionRepository _actionRepository;
        private readonly IProxyRepository _proxyRepository;
        private readonly MongoDbContext _dbContext;
        private readonly IMapper _mapper;


        public FakeAccountServices(
            IFakeAccountRepository fakeAccountRepository,
            IGroupFakeAccountRepository groupFakeAccountRepository,
            IFriendFakeAccountRepository friendFakeAccountRepository,
            IScriptFakeAccountRepository scriptFakeAccountRepository,
            IScriptRepository scriptRepository,
            IActionRepository actionRepository,
            IProxyRepository proxyRepository,
            MongoDbContext context,
            IMapper mapper
            )
        {
            _fakeAccountRepository = fakeAccountRepository;
            _groupFakeAccountRepository = groupFakeAccountRepository;
            _friendFakeAccountRepository = friendFakeAccountRepository;
            _scriptFakeAccountRepository = scriptFakeAccountRepository;
            _scriptRepository = scriptRepository;
            _actionRepository = actionRepository;
            _proxyRepository = proxyRepository;
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<FakeAccountDto>> GetByIdAsync(string id)
        {
            var entity = await _fakeAccountRepository.GetByIdAsync(id);
            var resultDto = _mapper.Map<FakeAccountDto>(entity);
            return new ApiResponse<FakeAccountDto>(200, "Success", resultDto);
        }

        public async Task<ApiResponse<FakeAccountDto>> CreateAsync(FakeAccountCreateDto dto)
        {
            // Validate nghiệp vụ (trùng UID...)
            if (await _fakeAccountRepository.ExistsByUidAsync(dto.Uid))
                return new ApiResponse<FakeAccountDto>(409, "UID đã tồn tại!", null);

            var entity = _mapper.Map<FakeAccount>(dto);
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            await _fakeAccountRepository.AddAsync(entity);

            var resultDto = _mapper.Map<FakeAccountDto>(entity);
            return new ApiResponse<FakeAccountDto>(201, "Fake account created successfully", resultDto);
        }

        public async Task<ApiResponse<FakeAccountDto>> UpdateAsync(string id, FakeAccountUpdateDto dto)
        {
            var entity = await _fakeAccountRepository.GetByIdAsync(id);
            if (entity == null)
                return new ApiResponse<FakeAccountDto>(404, "Fake account not found", null);

            _mapper.Map(dto, entity);
            entity.UpdatedAt = DateTime.UtcNow;
            await _fakeAccountRepository.UpdateAsync(id, entity);

            var resultDto = _mapper.Map<FakeAccountDto>(entity);
            return new ApiResponse<FakeAccountDto>(200, "Fake account updated successfully", resultDto);
        }

        public async Task<ApiResponse<FakeAccountDto>> DeleteAsync(string id)
        {
            var entity = await _fakeAccountRepository.GetByIdAsync(id);
            if (entity == null)
                return new ApiResponse<FakeAccountDto>(404, "Fake account not found", null);

            await _fakeAccountRepository.DeleteAsync(id);
            //var dto = _mapper.Map<FakeAccountDto>(entity);
            return new ApiResponse<FakeAccountDto>(200, "Fake account deleted successfully", null);
        }


        public async Task<ApiResponse<object>> ImportFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return (new ApiResponse<object>(400, "Invalid file import.", null));

            var results = new List<FakeAccountsImportResultDto>();
            var ext = Path.GetExtension(file.FileName).ToLower();
            if (ext == ".xlsx")
            {
                // If you use EPPlus for Noncommercial personal use.
                ExcelPackage.License.SetNonCommercialPersonal("My Name"); //This will also set the Author property to the name provided in the argument.

                using (var stream = file.OpenReadStream())
                using (var package = new ExcelPackage(stream))
                {

                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;


                    //// List tên các cột bạn yêu cầu, đúng thứ tự!
                    //var requiredColumns = new List<string> { "Platform", "Uid", "Password", "TwoFA", "Email", "EmailPassword" };

                    //// Đọc header thực tế từ file
                    //List<string> headers = new List<string>();
                    //for (int col = 1; col <= requiredColumns.Count; col++)
                    //{
                    //    headers.Add(worksheet.Cells[1, col].Text?.Trim());
                    //}

                    //// Kiểm tra đủ cột chưa
                    //for (int i = 0; i < requiredColumns.Count; i++)
                    //{
                    //    if (!string.Equals(headers[i], requiredColumns[i], StringComparison.OrdinalIgnoreCase))
                    //    {
                    //        return new ApiResponse<object>(400, $"Thiếu hoặc sai cột dữ liệu: Yêu cầu '{requiredColumns[i]}' ở vị trí {i + 1}, thực tế: '{headers[i]}'", null);
                    //    }
                    //}


                    for (int row = 2; row <= rowCount; row++) // Bỏ dòng header
                    {
                        var dto = new ImportFakeAccountDto
                        {
                            Platform = worksheet.Cells[row, 1].Text?.Trim(),
                            Uid = worksheet.Cells[row, 2].Text?.Trim(),
                            Password = worksheet.Cells[row, 3].Text?.Trim(),
                            TwoFA = worksheet.Cells[row, 4].Text?.Trim(),
                            Email = worksheet.Cells[row, 5].Text?.Trim(),
                            EmailPassword = worksheet.Cells[row, 6].Text?.Trim()
                        };

                        var result = await ImportSingleAccount(dto);
                        results.Add(result);
                    }
                }
            }
            else if (ext == ".csv")
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvHelper.CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
                {
                    //csv.Read();
                    //csv.ReadHeader();
                    //var headerRow = csv.HeaderRecord;

                    //var requiredColumns = new[] { "Platform", "Uid", "Password", "TwoFA", "Email", "EmailPassword" };
                    //foreach (var col in requiredColumns)
                    //{
                    //    if (!headerRow.Contains(col))
                    //    {
                    //        return new ApiResponse<object>(400, $"Thiếu cột bắt buộc: {col}", null);
                    //    }
                    //}

                    var records = csv.GetRecords<ImportFakeAccountDto>().ToList();
                    int row = 2;
                    foreach (var dto in records)
                    {
                        var result = await ImportSingleAccount(dto);
                        results.Add(result);
                        row++;
                    }
                }
            }
            else
            {
                return new ApiResponse<object>(400, "Invalid file format. Only .csv and .xlsx are allowed.", null);
            }

            int success = results.Count(r => r.Success);
            int fail = results.Count - success;

            var summary = new
            {
                Success = success,
                Fail = fail,
                Details = results
            };

            return new ApiResponse<object>(200, "Result", summary);
        }

        private async Task<FakeAccountsImportResultDto> ImportSingleAccount(ImportFakeAccountDto dto)
        {
            var validator = new ImportFakeAccountDtoValidator();
            var result = validator.Validate(dto);

            if (!result.IsValid)
            {
                var message = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
                return new FakeAccountsImportResultDto(dto.Platform, dto.Uid, dto.Password, dto.TwoFA, dto.Email, dto.EmailPassword, "Sai định dạng", false);
            }

            if (await _fakeAccountRepository.ExistsByUidAsync(dto.Uid))
                return new FakeAccountsImportResultDto(dto.Platform, dto.Uid, dto.Password, dto.TwoFA, dto.Email, dto.EmailPassword, "UID đã tồn tại", false);

            var fakeAccount = _mapper.Map<FakeAccount>(dto);
            await _fakeAccountRepository.AddAsync(fakeAccount);
            return new FakeAccountsImportResultDto(dto.Platform, dto.Uid, dto.Password, dto.TwoFA, dto.Email, dto.EmailPassword, "Thành công", true);

        }

        public async Task<ApiResponse<PaginatedData<FakeAccountDto>>> GetFilteredAsync(FakeAccountQueryRequestDto queryRequestDto)
        {
            var (entities, total) = await _fakeAccountRepository.GetFilteredPaginatedAsync(queryRequestDto);
            var dtos = _mapper.Map<IEnumerable<FakeAccountDto>>(entities);
            return new ApiResponse<PaginatedData<FakeAccountDto>>(
                200,
                "Success",
                new PaginatedData<FakeAccountDto>(dtos, total, queryRequestDto.Page, queryRequestDto.PageSize)
            );
        }

        public async Task<ApiResponse<PaginatedData<FakeAccountDto>>> GetAllPaginatedAsync(int page, int pageSize)
        {
            var (entities, total) = await _fakeAccountRepository.GetAllPaginatedAsync(page, pageSize);
            var dtos = _mapper.Map<IEnumerable<FakeAccountDto>>(entities);
            return new ApiResponse<PaginatedData<FakeAccountDto>>(
                200,
                "Success",
                new PaginatedData<FakeAccountDto>(dtos, total, page, pageSize)
            );
        }


        public async Task<ApiResponse<PaginatedData<GroupDto>>> GetGroupsByFakeAccountIdAsync(string fakeAccountId, GroupSearchRequestDto dto)
        {
            var (groups, total) = await _groupFakeAccountRepository.SearchGroupsByFakeAccountAsync(fakeAccountId, dto);
            var dtos = _mapper.Map<IEnumerable<GroupDto>>(groups);
            return new ApiResponse<PaginatedData<GroupDto>>(200, "Success", new PaginatedData<GroupDto>(dtos, total, dto.Page, dto.PageSize));
        }

        public async Task<ApiResponse<PaginatedData<FriendDto>>> GetFriendsByFakeAccountIdAsync(string fakeAccountId, FriendSearchRequestDto dto)
        {
            var (friends, totalCount) = await _friendFakeAccountRepository.SearchFriendsByFakeAccountAsync(fakeAccountId, dto);
            var dtos = _mapper.Map<IEnumerable<FriendDto>>(friends);
            var paginated = new PaginatedData<FriendDto>(dtos, totalCount, dto.Page, dto.PageSize);
            return new ApiResponse<PaginatedData<FriendDto>>(200, "Success", paginated);
        }

        //public async Task<ApiResponse<IEnumerable<ScriptDto>>> GetScriptsByFakeAccountIdAsync(string fakeAccountId)
        //{
        //    var (scripts, totalCount) = await _scriptFakeAccountRepository.GetScriptsOfFakeAccount(fakeAccountId);
        //    var dtos = _mapper.Map<IEnumerable<ScriptDto>>(scripts);
        //    return new ApiResponse<IEnumerable<ScriptDto>>(200, "Success", dtos);
        //}


        public async Task<ApiResponse<object>> AssignScriptsToFakeAccountAsync(string fakeAccountId, List<string> scriptIds)
        {
            try
            {
                await _scriptFakeAccountRepository.AssignScriptsToFakeAccount(fakeAccountId, scriptIds);
                return new ApiResponse<object>(200, "Scripts assigned successfully", null);
            }
            catch (Exception ex) 
            {
                var response = new ApiResponse<object>(
                    code: 500,
                    message: "An error occurred while assigning scripts.",
                    data: new { error = ex.Message }
                );
                return response;
            }
        }

        public async Task<ApiResponse<PaginatedData<ScriptDto>>> GetScriptByFakeAccountIdAsync(string fakeAccountId, ScriptSearchRequestDto dto)
        {

            //var (entities, total) = await _scriptFakeAccountRepository.SearchScriptsByFakeAccountIdAsync(fakeAccountId, dto.ScriptName, dto.Page, dto.PageSize);
            //var dtos = _mapper.Map<IEnumerable<ScriptDto>>(entities);

            //var scriptIds = await _scriptFakeAccountRepository.GetScriptIdsByFakeAccountIdAsync(fakeAccountId);
            //if (!scriptIds.Any()) return (new List<ScriptDto>(), 0);

            //var skip = (dto.Page - 1) * dto.PageSize;

            //var totalCount = await _scriptRepository.CountByIdsFilteredAsync(scriptIds, dto.ScriptName);
            //var scripts = await _scriptRepository.GetByIdsFilteredAsync(scriptIds, dto.ScriptName, skip, dto.PageSize);

            //var actionCounts = await _actionRepository.CountActionsByScriptIdsAsync(scripts.Select(s => s.Id).ToList());

            //var result = scripts.Select(script => new ScriptDto
            //{
            //    Id = script.Id,
            //    Name = script.Name,
            //    Description = script.Description,
            //    ActionCount = actionCounts.TryGetValue(script.Id, out var count) ? count : 0
            //}).ToList();

            //return new ApiResponse<PaginatedData<ScriptDto>>(
            //    200,
            //    "Success",
            //    new PaginatedData<ScriptDto>(dtos, total, dto.Page, dto.PageSize)
            //);
            throw new NotImplementedException("GetScriptByFakeAccountIdAsync method is not implemented yet.");  
        }

        public async Task<ApiResponse<object>> AssignScriptToFakeAccountAsync(string fakeAccountId, string scriptId)
        {
            try
            {
                await _scriptFakeAccountRepository.AssignScriptToFakeAccount(fakeAccountId, scriptId);
                return new ApiResponse<object>(200, "Scripts assigned successfully", null);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<object>(
                    code: 500,
                    message: "An error occurred while assigning scripts.",
                    data: new { error = ex.Message }
                );
                return response;
            }
        }


        // A) Gán 1 account ↔ 1 proxy (manual, KHÔNG transaction cũng được)
        public async Task<ApiResponse<ProxyAssignOneResult>> AssignManualToOneAsync(string accountId, ProxyAssignManualSingleDto dto, CancellationToken ct = default)
        {
            // 1) Tạo ProxyEntity (manual) và insert
            var proxy = Utils.ParseToEntity(dto.ProxyRaw, dto.Type, null, 1);
            var inserted = await _proxyRepository.InsertManualAsync(new[] { proxy }, ct);
            var p = inserted[0];

            // 2) Tăng InUseCount và gán account (transaction để đồng bộ)
            using var s = await _dbContext.StartSessionAsync(null,ct);
            s.StartTransaction();
            try
            {
                var ok = await _proxyRepository.TryIncInUseAsync(p.Id, p.MaxUse, s, ct);
                if (!ok) throw new InvalidOperationException("Proxy hết slot");

                await _fakeAccountRepository.BulkSetProxyAsync(new[] { (accountId, p) }, s, ct);

                await s.CommitTransactionAsync(ct);

                var data = new ProxyAssignOneResult(accountId, p.Id);
                return ApiResponse<ProxyAssignOneResult>.Ok(data, "Assigned");
            }
            catch(Exception ex) 
            { 
                await s.AbortTransactionAsync(ct);
                return ApiResponse<ProxyAssignOneResult>.Fail(500, ex.Message);
            }
        }

        // B) Gán N account từ GROUP (batch)
        public async Task<ApiResponse<ProxyAssignBatchResult>> AssignFromGroupBatchAsync(ProxyAssignFromGroupDto dto, CancellationToken ct = default)
        {
            try
            {
                var proxies = await _proxyRepository.GetByGroupAsync(dto.GroupId, ct);
                if (proxies.Count == 0)
                    return ApiResponse<ProxyAssignBatchResult>.Fail(404, "Empty proxy group");

                // Áp dụng rule N account / proxy
                foreach (var p in proxies) p.MaxUse = Math.Max(1, dto.AccountsPerProxy);

                // Sắp xếp: round-robin theo InUseCount hoặc random
                if (dto.Mode.Equals(1))
                    // mẹo để randomize (xáo trộn) thứ tự danh sách
                    proxies = proxies.OrderBy(_ => Guid.NewGuid()).ToList();
                else
                    // sắp xếp lại danh sách proxies theo số lượng tài khoản đang dùng proxy đó (InUseCount), theo thứ tự tăng dần.
                    proxies = proxies.OrderBy(x => x.InUseCount).ToList();

                // Chọn accounts
                var accIds = dto.AccountIds;
                var accounts = await _fakeAccountRepository.FindByIdsAsync(accIds, onlyWithoutProxy: !dto.IncludeAccountsWithProxy, ct);
                if (accounts.Count == 0)
                    return ApiResponse<ProxyAssignBatchResult>.Ok(new ProxyAssignBatchResult(0, accIds), "No accounts selected");

                // Phân phối trước (ngoài transaction)
                var pairs = new List<(string AccountId, Proxy Proxy)>();
                var notAssigned = new List<string>();
                int cursor = 0;

                foreach (var a in accounts)
                {
                    Proxy pick = null;
                    for (int step = 0; step < proxies.Count; step++)
                    {
                        var i = (cursor + step) % proxies.Count;
                        if (proxies[i].InUseCount < proxies[i].MaxUse)
                        { pick = proxies[i]; proxies[i].InUseCount++; cursor = i + 1; break; }
                    }
                    if (pick == null) { notAssigned.Add(a.Id); continue; }
                    pairs.Add((a.Id, pick));
                }

                if (pairs.Count == 0)
                    return ApiResponse<ProxyAssignBatchResult>.Ok(new ProxyAssignBatchResult(0, notAssigned), "No slots left");

                // Vào transaction: tăng inUse cho các proxy được dùng + gán account
                using var s = await _dbContext.StartSessionAsync(null, ct);
                s.StartTransaction();
                try
                {
                    // Tăng InUseCount (CAS) cho từng account-pair (an toàn cạnh tranh)
                    foreach (var grp in pairs.GroupBy(x => x.Proxy.Id))
                    {
                        var proxy = proxies.First(p => p.Id == grp.Key);
                        var ok = await _proxyRepository.TryIncInUseAsync(proxy.Id, proxy.MaxUse, s, ct);
                        if (!ok)
                        {
                            await s.AbortTransactionAsync(ct);
                            return ApiResponse<ProxyAssignBatchResult>.Fail(409, "Proxy has no slot left");
                        }
                    }

                    await _fakeAccountRepository.BulkSetProxyAsync(pairs, s, ct);

                    await s.CommitTransactionAsync(ct);
                    return ApiResponse<ProxyAssignBatchResult>.Ok(new ProxyAssignBatchResult(pairs.Count, notAssigned), "Assigned");
                }
                catch (Exception ex)
                {
                    await s.AbortTransactionAsync(ct);
                    return ApiResponse<ProxyAssignBatchResult>.Fail(500, ex.Message);
                }
            }
            catch (Exception ex) 
            {
                return ApiResponse<ProxyAssignBatchResult>.Fail(500, ex.Message);
            }
        }

        // C) Detach 1 account khỏi proxy
        public async Task<ApiResponse<object>> DetachAsync(string accountId, string proxyId, CancellationToken ct = default)
        {
            try
            {
                using var s = await _dbContext.StartSessionAsync(null, ct);
                s.StartTransaction();
                try
                {
                    await _fakeAccountRepository.DetachProxyAsync(accountId, proxyId, s, ct);
                    await _proxyRepository.TryDecInUseAsync(proxyId, s, ct);

                    await s.CommitTransactionAsync(ct);
                    return ApiResponse<object>.NoContent("Detached");
                }
                catch (Exception ex)
                {
                    await s.AbortTransactionAsync(ct);
                    return ApiResponse<object>.Fail(500, ex.Message);
                }
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.Fail(500, ex.Message);
            }
        }
    }
}
