using Microsoft.AspNetCore.Mvc;
using OsintCommand.API.Dtos;
using OsintCommand.API.Response;
using OsintCommand.API.Services;

/// TODO: 
/// GET ALL : thêm số lượng tk ảo, hành động 
///

namespace OsintCommand.API.Controllers
{
    [Route("api/scripts")]
    [ApiController]
    public class ScriptsController : ControllerBase
    {
        private readonly IScriptServices _scriptServices;
        public ScriptsController(IScriptServices scriptServices)
        {
            _scriptServices = scriptServices;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllFakeAccounts([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            try
            {
                var result = await _scriptServices.GetAllPaginatedAsync(page, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<object>(
                    code: 500,
                    message: "An error occurred while retrieving scripts.",
                    data: new { error = ex.Message }
                );
                return StatusCode(500, response);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateFakeAccount([FromBody] ScriptCreateDto createDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _scriptServices.CreateAsync(createDto);
                return StatusCode(result.Code, result);
            }
            else
            {
                return BadRequest(new ApiResponse<FakeAccountDto>(400, "Invalid data.", null));
            }
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetScriptById([FromRoute] string id)
        {
            try
            {
                var dto = await _scriptServices.GetDetailByIdAsync(id);
                return StatusCode(dto.Code, dto);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<object>(
                    code: 500,
                    message: "An error occurred while retrieving the script.",
                    data: new { error = ex.Message }
                );
                return StatusCode(500, response);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateScript([FromRoute] string id, [FromBody] ScriptUpdateDto updateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _scriptServices.UpdateAsync(id, updateDto);
                return StatusCode(result.Code, result);
            }
            else
            {
                return BadRequest(new ApiResponse<FakeAccountDto>(400, "Invalid data.", null));
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchScripts([FromQuery] string name, [FromQuery] int page, [FromQuery] string pageSize)
        {
            try
            {
                var result = await _scriptServices.SearchScriptPaginatedAsync(name, page, int.Parse(pageSize));
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<object>(
                    code: 500,
                    message: "An error occurred while searching scripts.",
                    data: new { error = ex.Message }
                );
                return StatusCode(500, response);
            }
        }

        [HttpGet("{id}/actions")]
        public async Task<IActionResult> GetActionsByScriptId([FromRoute] string id, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var actions = await _scriptServices.GetActionsByScriptIdAsync(id, page, pageSize);
                return Ok(actions);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<object>(
                    code: 500,
                    message: "An error occurred while retrieving actions for the script.",
                    data: new { error = ex.Message }
                );
                return StatusCode(500, response);
            }

        }

        [HttpPost("{id}/add-action")]
        public async Task<IActionResult> AddActionToScript([FromRoute] string id, [FromBody] ActionAddRequestDto dto)
        {
            try
            {
                var result = await _scriptServices.AddActionToScriptAsync(id, dto);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<object>(
                    code: 500,
                    message: "An error occurred while adding action to the script.",
                    data: new { error = ex.Message }
                );
                return StatusCode(500, response);
            }

        }

        [HttpPut("{id}/update-action/{actionId}")]
        public async Task<IActionResult> UpdateActionInScript([FromRoute] string id, [FromRoute] string actionId, [FromBody] ActionUpdateRequestDto dto)
        {
            try
            {
                var result = await _scriptServices.UpdateActionInScriptAsync(id, actionId, dto);
                return StatusCode(result.Code, result);
                //throw new NotImplementedException("UpdateActionInScript method is not implemented yet.");
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<object>(
                    code: 500,
                    message: "An error occurred while updating action in the script.",
                    data: new { error = ex.Message }
                );
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}/delete-action/{actionId}")]
        public async Task<IActionResult> DeleteActionFromScript([FromRoute] string id, [FromRoute] string actionId)
        {
            try
            {
                await _scriptServices.DeleteActionFromScriptAsync(id, actionId);
                return Ok(new ApiResponse<object>(200, "Action deleted successfully.", null));
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<object>(
                    code: 500,
                    message: "An error occurred while deleting action from the script.",
                    data: new { error = ex.Message }
                );
                return StatusCode(500, response);
            }
        }


    }
}
