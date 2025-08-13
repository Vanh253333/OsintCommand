using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OsintCommand.API.Entities
{
    public class Friend
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("fakeAccountId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FakeAccountId { get; set; }

        [BsonElement("friendUid")]
        public string FriendUid { get; set; }

        [BsonElement("friendName")]
        public string FriendName { get; set; }

        [BsonElement("friendAvatar")]
        public string FriendAvatar { get; set; }

        [BsonElement("gender")]
        public string Gender { get; set; }

        [BsonElement("link")]
        public string Link { get; set; }
        [BsonElement("connectedDate")]
        public DateTime ConnectedDate { get; set; }

    }
}
