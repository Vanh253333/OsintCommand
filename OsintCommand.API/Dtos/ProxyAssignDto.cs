using OsintCommand.API.Entities;

namespace OsintCommand.API.Dtos
{
    public class ProxyAssignManualSingleDto
    {
        public string ProxyRaw { get; set; }      // "ip:port[:user:pass]"
        public ProxyType Type { get; set; } = ProxyType.Http;
    }

    public class ProxyAssignFromGroupDto
    {
        public string GroupId { get; set; }
        public List<string> AccountIds { get; set; } = new();
        public int Mode { get; set; } = 0; // 0: "lanluot" |  1: "ngaunhien"
        public int AccountsPerProxy { get; set; } = 1;
        public bool IncludeAccountsWithProxy { get; set; } = true;
    }

    public record ProxyAssignOneResult(string AccountId, string ProxyId);
    public record ProxyAssignBatchResult(int Assigned, List<string> NotAssigned);
}
