using System.ComponentModel.DataAnnotations;

namespace Application.Data.DTOs
{
    public class LoginDTO
    {
        [MinLength(3, ErrorMessage = "Tên người dùng quá ngắn!")]
        [MaxLength(30, ErrorMessage = "Tên người dùng quá dài!")]
        [Required(ErrorMessage = "Bắt buộc.")]
        public string Username { get; set; } = null!;
        [MinLength(8, ErrorMessage = "Mật khẩu quá ngắn.")]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W)[A-Za-z\d\W]{8,}$",
            ErrorMessage = "Mật khẩu phải có 8 ký tự, bao gồm ký tự viết hoa, viết thường và ký tự đặc biệt."
            )]
        public string Password { get; set; } = null!;
    }
}
