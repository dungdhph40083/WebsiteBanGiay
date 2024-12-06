using Application.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class User
    {
        [Key]
        [Required(ErrorMessage = ValidateErrorResult.BAD_INPUT)]
        public Guid UserID { get; set; }
        [MinLength(3, ErrorMessage = ValidateErrorResult.USERNAME_TOO_SHORT)]
        [MaxLength(30, ErrorMessage = ValidateErrorResult.USERNAME_TOO_LONG)]
        [Required(ErrorMessage = ValidateErrorResult.EMPTY_FIELD_NOT_ALLOWED)]
        public string Username { get; set; } = null!;
        [MinLength(8, ErrorMessage = ValidateErrorResult.PASSWORD_TOO_SHORT)]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W)[A-Za-z\d\W]{8,}$",
            ErrorMessage = ValidateErrorResult.PASSWORD_NOT_SECURE_ENOUGH
            )]
        [Required(ErrorMessage = ValidateErrorResult.EMPTY_FIELD_NOT_ALLOWED)]
        public string Password { get; set; } = null!;
        [ForeignKey(nameof(Role))]
        public Guid? RoleID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [EmailAddress(ErrorMessage = ValidateErrorResult.BAD_EMAIL_FORMAT)]
        public string? Email { get; set; }
        [ForeignKey(nameof(Image))]
        public Guid? ImageID { get; set; }
        public string? Address { get; set; }
        [Length(9, 30, ErrorMessage = ValidateErrorResult.BAD_PHONE_NUMBER)]
        [RegularExpression(@"^\d*$", ErrorMessage = ValidateErrorResult.BAD_PHONE_NUMBER)]
        public string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        [Required(ErrorMessage = ValidateErrorResult.BAD_INPUT)]
        public byte Status { get; set; }
        public virtual Image? Image { get; set; }
        public virtual Role? Role { get; set; }
	}
}
