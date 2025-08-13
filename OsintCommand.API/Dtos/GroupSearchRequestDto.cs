namespace OsintCommand.API.Dtos
{
    public class GroupSearchRequestDto
    {
        public string? GroupName { get; set; }
        public string? GroupUid { get; set; }
        public string? GroupLink { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
