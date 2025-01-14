using Application.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Application.Data.Enums;

namespace Application.Data.DTOs
{
    public class UserEditDTO
    {
        public Guid? RoleID { get; set; }
        public Guid? ImageID { get; set; }

        [MinLength(3, ErrorMessage = "Tên người dùng quá ngắn!")]
        [MaxLength(30, ErrorMessage = "Tên người dùng quá dài!")]
        [Required(ErrorMessage = "Bắt buộc phải nhập tài khoản")]
        public string Username { get; set; } = null!;

        [MinLength(8, ErrorMessage = "Mật khẩu quá ngắn.")]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W)[A-Za-z\d\W]{8,}$",
            ErrorMessage = "Mật khẩu phải có 8 ký tự, bao gồm ký tự viết hoa, viết thường và ký tự đặc biệt."
            )]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Bắt buộc phải nhập Họ")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Bắt buộc phải nhập Tên")]
        public string? LastName { get; set; }
        [EmailAddress(ErrorMessage = "Email sai!")]
        [Required(ErrorMessage = "Bắt buộc phải nhập Email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Bắt buộc phải nhập Địa chỉ")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [RegularExpression(@"^(03|05|07|08|09)\d{8}$", ErrorMessage = "Số điện thoại không hợp lệ.")]

        public string? PhoneNumber { get; set; }
        //[Required(ErrorMessage = "Không khớp.")]
        //public byte? Status { get; set; }

    }
}
