using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OsintCommand.API.Dtos
{
    public class GroupDto
    {
        public string Id { get; set; }
        public string FakeAccountId { get; set; } // liên kết với FakeAccount
        public string GroupUid { get; set; }
        public string GroupName { get; set; }
        public string GroupAvatar { get; set; }
        public string GroupLink { get; set; }
        public DateTime JoinedDate { get; set; }
        public string Status { get; set; } // Đang hoạt động, Không tồn tại, v.v.
    }
}
