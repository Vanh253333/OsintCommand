using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OsintCommand.API.Dtos
{
    public class FriendDto
    {
        public string Id { get; set; }
        public string FakeAccountId { get; set; }
        public string FriendUid { get; set; }
        public string FriendName { get; set; }
        public string FriendAvatar { get; set; }
        public string Gender { get; set; }
        public string Link { get; set; }
        public DateTime ConnectedDate { get; set; }
    }
}
