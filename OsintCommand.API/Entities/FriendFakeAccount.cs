using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OsintCommand.API.Entities
{
    public class FriendFakeAccount 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("friendId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FriendId { get; set; }

        [BsonElement("fakeAccountId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FakeAccountId { get; set; }

        // thời gian gán
    }
}
