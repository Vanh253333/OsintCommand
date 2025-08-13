using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OsintCommand.API.Entities
{
    public class ScriptFakeAccount
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("scriptId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ScriptId { get; set; }

        [BsonElement("fakeAccountId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FakeAccountId { get; set; }

        // thời gian gán
    }
}
