using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.DTOs
{
    public class UserRegisterDTO
    {

        // hide this when adding new entries!!!!!!!!!!!!!!!!!!!!!!!!

        [MinLength(3, ErrorMessage = "Tên người dùng quá ngắn!")]
        [MaxLength(30, ErrorMessage = "Tên người dùng quá dài!")]
        [Required(ErrorMessage = "Bắt buộc.")]
        public string Username { get; set; } = null!;
        [MinLength(8, ErrorMessage = "Mật khẩu quá ngắn.")]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W)[A-Za-z\d\W]{8,}$",
            ErrorMessage = "Mật khẩu phải có 8 ký tự, bao gồm ký tự viết hoa, viết thường và ký tự đặc biệt."
            )]
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [EmailAddress(ErrorMessage = "Email sai!")]
        public string? Email { get; set; }
        public string? Address { get; set; }
        [Length(9, 30, ErrorMessage = "SĐT quá ngắn hoặc quá dài!")]
        [RegularExpression(@"^\d*$", ErrorMessage = "SĐT không khớp.")]
        public string? PhoneNumber { get; set; }
    }
}
