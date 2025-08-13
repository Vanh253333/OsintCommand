namespace OsintCommand.API.Dtos
{
    public class FriendSearchRequestDto
    {
        public string? FriendName { get; set; }
        public string? FriendUid { get; set; }
        public string? Gender { get; set; }
        public string? Link { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
