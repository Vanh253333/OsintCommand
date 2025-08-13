using Microsoft.AspNetCore.Mvc;

namespace OsintCommand.API.Dtos
{
    public class ScriptSearchRequestDto
    {
        //public string? ScriptType;
        public string? ScriptName;
        public int Page = 1;
        public int PageSize = 10;
    }
}
