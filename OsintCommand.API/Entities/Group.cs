using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OsintCommand.API.Entities
{
    public class Group
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("fakeAccountId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FakeAccountId { get; set; } // liên kết với FakeAccount

        [BsonElement("groupUid")]
        public string GroupUid { get; set; }

        [BsonElement("groupName")]
        public string GroupName { get; set; }

        [BsonElement("groupAvatar")]
        public string GroupAvatar { get; set; }

        [BsonElement("groupLink")]
        public string GroupLink { get; set; }

        [BsonElement("joinedDate")]
        public DateTime JoinedDate { get; set; }

        [BsonElement("status")]
        public string Status { get; set; } // Đang hoạt động, Không tồn tại, v.v.
        

    }
}
