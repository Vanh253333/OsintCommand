using System.ComponentModel.DataAnnotations;

namespace OsintCommand.API.Dtos
{
    public class FakeAccountUpdateDto
    {
        //public string? AccountName { get; set; }
        public string? Platform { get; set; }
        public string? Uid { get; set; }
        public string? Password { get; set; }
        public string? TwoFA { get; set; }
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }
        public string? EmailPassword { get; set; }
        public string? AccountType { get; set; }
        public string? Job { get; set; }
    }
}
