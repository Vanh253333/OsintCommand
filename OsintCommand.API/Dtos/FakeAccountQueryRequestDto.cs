namespace OsintCommand.API.Dtos
{
    public class FakeAccountQueryRequestDto
    {
        public string? Uid { get; set; }
        public string? Email { get; set; }
        public List<string>? Platform { get; set; }
        public List<string>? Job { get; set; }
        public string? AccountType { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
