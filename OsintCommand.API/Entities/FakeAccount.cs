using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OsintCommand.API.Entities
{
    public class FakeAccount
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("accountName")]
        public string? AccountName { get; set; }

        [BsonElement("platform")]
        public string? Platform { get; set; }

        [BsonElement("uid")]
        public string Uid { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("twoFA")]
        public string TwoFA { get; set; }

        [BsonElement("email")]
        public string? Email { get; set; }

        [BsonElement("emailPassword")]
        public string? EmailPassword { get; set; }

        [BsonElement("securityLevel")]
        public string SecurityLevel { get; set; }

        [BsonElement("accountType")]
        public string AccountType { get; set; }

        [BsonElement("isActive")]
        public bool IsActive { get; set; }

        [BsonElement("job")]
        public string Job { get; set; }

        [BsonElement("createdBy")]
        public string CreatedBy { get; set; }

        [BsonElement("updatedBy")]
        public string UpdatedBy { get; set; }

        [BsonElement("createdAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime UpdatedAt { get; set; }

        public string? ProxyId { get; set; }
        public ProxyType? ProxyType { get; set; }
        public string? ProxyHost { get; set; }
        public int? ProxyPort { get; set; }
    }
}
