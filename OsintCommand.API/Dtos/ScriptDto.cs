
namespace OsintCommand.API.Dtos
{
    public class ScriptDto
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Channel { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; } // timestamp đến giây
        public DateTime UpdateAt { get; set; }
        public int ActionCount { get; set; }
        public int Order { get; set; }
        public Dictionary<string, object>? Config { get; set; } // JSON string representing the script configuration
    }


    public class ScriptDetailDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Channel { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int Order { get; set; }
        public int FakeAccountCount { get; set; }
        public int ActionCount { get; set; }
        public Dictionary<string, object>? Config { get; set; } // JSON string representing the script configuration
        public List<ActionDto> Actions { get; set; } = new List<ActionDto>();
        //public IEnumerable<FakeAccountDto> FakeAccounts { get; set; } = new List<FakeAccountDto>();
    }

    
}
