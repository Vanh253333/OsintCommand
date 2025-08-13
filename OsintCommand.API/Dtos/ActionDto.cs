using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Linq;

namespace OsintCommand.API.Dtos
{
    public class ActionDto
    {
        public string? Id { get; set; }
        //public string ScriptId { get; set; }
        public int InteractType { get; set; }
        public string Name { get; set; }
        public Dictionary<string, object> Config { get; set; } // JSON string representing the action configuration
        public int Order { get; set; }
    }
}
