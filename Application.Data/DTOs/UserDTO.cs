using Application.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Application.Data.Enums;

namespace Application.Data.DTOs
{
    public class UserDTO
    {
        public Guid? RoleID { get; set; }
        public Guid? ImageID { get; set; }
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
        // 
        [Length(9, 30, ErrorMessage = "SĐT quá ngắn hoặc quá dài!")]
        [RegularExpression(@"^\d*$", ErrorMessage = "SĐT không khớp.")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Không khớp.")]
        public byte? Status { get; set; }

        /*
         * I don't know why I'm constantly getting déjà vu from just looking at this code
         * Or when doing something else...
         * Or when drawing anything...
         * Or when chatting...
         * Or when modding a game...
         * Or when just trying to rest...
         * Or when I'm figuring out why I'm having the weirdest déjà vus ever...
         * ...
         * Am I going crazy?
         */
    }
}
