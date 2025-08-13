using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Linq;

namespace OsintCommand.API.Entities
{
    public class Action
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("scriptId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ScriptId { get; set; }

        [BsonElement("interactType")]
        public int InteractType { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("config")]
        public Dictionary<string, object> Config { get; set; } = null;// JSON string representing the action configuration

        [BsonElement("order")]
        public int Order { get; set; }
    }
}


