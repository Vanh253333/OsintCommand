using System.ComponentModel.DataAnnotations;

namespace OsintCommand.API.Dtos
{
    public class FakeAccountDto
    {
        public string? Id { get; set; }
        public string? AccountName { get; set; }
        public string? Platform { get; set; }
        [Required]
        public string Uid { get; set; }
        [Required]
        public string Password { get; set; }
        public string TwoFA { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }

        public string? EmailPassword { get; set; }
        public string SecurityLevel { get; set; }
        [Required]
        public string AccountType { get; set; }
        [Required]
        public string Job { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; } = "";
        public string? UpdatedBy { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
