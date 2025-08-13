using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OsintCommand.API.Entities
{
    public class GroupFakeAccount
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("groupId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string GroupId { get; set; }

        [BsonElement("fakeAccountId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FakeAccountId { get; set; }

        // thời gian gán
    }
}
