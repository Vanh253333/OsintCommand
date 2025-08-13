using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace OsintCommand.API.Dtos
{
    public class ImportFakeAccountDto
    {
        [Required]
        public string Platform { get; set; }
        [Required]
        public string Uid { get; set; }
        [Required]
        public string Password { get; set; }
        public string? TwoFA { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }
        public string? EmailPassword { get; set; }
    }

    public class ImportFakeAccountDtoValidator : AbstractValidator<ImportFakeAccountDto>
    {
        public ImportFakeAccountDtoValidator()
        {
            RuleFor(x => x.Platform).NotEmpty();
            RuleFor(x => x.Uid).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
        }
    }
}
