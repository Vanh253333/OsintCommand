using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OsintCommand.API.Entities
{
    public class Script
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("channel")]
        public string Channel { get; set; }

        [BsonElement("createdBy")]
        public string CreatedBy { get; set; }

        [BsonElement("updatedBy")]
        public string UpdatedBy { get; set; }

        [BsonElement("createdAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updateAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime UpdateAt { get; set; }

        [BsonElement("order")]
        public int Order { get; set; }

        [BsonElement("config")]
        public string Config { get; set; } // JSON string representing the action configuration
    }
}
