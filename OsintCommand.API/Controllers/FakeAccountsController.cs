using Microsoft.AspNetCore.Mvc;
using OsintCommand.API.Dtos;
using OsintCommand.API.Response;
using OsintCommand.API.Services;

/// TODO:
/// proxy
/// 

namespace OsintCommand.API.Controllers
{
    [Route("api/fakeaccounts")]
    [ApiController]
    public class FakeAccountsController : ControllerBase
    {
        private readonly IFakeAccountServices _fakeAccountService;

        public FakeAccountsController(IFakeAccountServices fakeAccountService)
        {
            _fakeAccountService = fakeAccountService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllFakeAccounts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _fakeAccountService.GetAllPaginatedAsync(page, pageSize);
            return Ok(result);
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchFakeAccounts([FromQuery] FakeAccountQueryRequestDto queryRequestDto)
        {
            var result = await _fakeAccountService.GetFilteredAsync(queryRequestDto);
            return Ok(result);
        }


        // GET api/fakeaccount/{id}
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetFakeAccountById([FromRoute] string id)
        {
            var dto = await _fakeAccountService.GetByIdAsync(id);
            if (dto == null)
                return NotFound(new ApiResponse<object>(404, "Fake account not found.", null));
            return Ok(dto);
        }

        // POST api/fakeaccount
        [HttpPost("create")]
        public async Task<IActionResult> CreateFakeAccount([FromBody] FakeAccountCreateDto createDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _fakeAccountService.CreateAsync(createDto);
                return StatusCode(result.Code, result);
            }
            else 
            {
                return BadRequest(new ApiResponse<FakeAccountDto>(400, "Invalid data.", null));
            }        
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteFakeAccount([FromRoute] string id)
        {
            //var result = await _fakeAccountService.DeleteAsync(id);
            //return StatusCode(result.Code, result);
            throw new NotImplementedException("DeleteFakeAccount method is not implemented yet.");
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateFakeAccount([FromRoute] string id, [FromBody] FakeAccountUpdateDto updateDto)
        {
            if(ModelState.IsValid)
            {
                var result = await _fakeAccountService.UpdateAsync(id, updateDto);
                return StatusCode(result.Code, result);
            }
            else 
            {
                return BadRequest(new ApiResponse<FakeAccountDto>(400, "Invalid data.", null));
            }
        }

        [HttpPost("import-file")]
        public async Task<IActionResult> ImportFakeAccounts(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new ApiResponse<object>(400, "Invalid data.", null));

            var result = await _fakeAccountService.ImportFileAsync(file);
            return StatusCode(result.Code, result);
        }

        [HttpGet("{id}/groups")]
        public async Task<IActionResult> GetGroupsOfFakeAccount([FromRoute] string id, [FromQuery] GroupSearchRequestDto dto)
        {
            var groups = await _fakeAccountService.GetGroupsByFakeAccountIdAsync(id, dto);
            return Ok(groups);
        }



        [HttpGet("{id}/friends")]
        public async Task<IActionResult> GetFriendsOfFakeAccount([FromRoute] string id, [FromQuery] FriendSearchRequestDto dto)
        {
            var friends = await _fakeAccountService.GetFriendsByFakeAccountIdAsync(id, dto);
            return Ok(friends);
        }

        [HttpGet("{id}/scripts")]
        public async Task<IActionResult> GetScriptsOfFakeAccount([FromRoute] string id, [FromQuery] ScriptSearchRequestDto dto)
        {
            var scripts = await _fakeAccountService.GetScriptByFakeAccountIdAsync(id, dto);
            return Ok(scripts);
        }

        [HttpPost("{id}/assign-scripts")]
        public async Task<IActionResult> AssignScriptsToFakeAccount([FromRoute] string id, [FromBody] List<string> scriptIds)
        {
            var result = await _fakeAccountService.AssignScriptsToFakeAccountAsync(id, scriptIds);
            return StatusCode(result.Code, result);
        }


        [HttpPost("{id}/assign-script")]
        public async Task<IActionResult> AssignScriptsToFakeAccount([FromRoute] string id, string scriptId)
        {
            var result = await _fakeAccountService.AssignScriptToFakeAccountAsync(id, scriptId);
            return StatusCode(result.Code, result);
        }

        // 1) 1 account – manual
        [HttpPost("{id}/proxy/assign-manual")]
        public async Task<ActionResult<ApiResponse<ProxyAssignOneResult>>> ManualSingle(
            string id,
            [FromBody] ProxyAssignManualSingleDto dto)
        {
            var res = await _fakeAccountService.AssignManualToOneAsync(id, dto);
            return StatusCode(res.Code, res);
        }

        // 2) N accounts – from group
        [HttpPost("proxy/asign-from-group")]
        public async Task<ActionResult<ApiResponse<ProxyAssignBatchResult>>> FromGroupBatch(
            [FromBody] ProxyAssignFromGroupDto dto)
        {
            var res = await _fakeAccountService.AssignFromGroupBatchAsync(dto);
            return StatusCode(res.Code, res);
        }

        // 3) Detach
        [HttpPost("{id}/proxy/detach/{proxyId}")]
        public async Task<ActionResult<ApiResponse<object>>> Detach(
            string id,
            string proxyId)
        {
            var res = await _fakeAccountService.DetachAsync(id, proxyId);
            return StatusCode(res.Code, res);
        }
    }
}
