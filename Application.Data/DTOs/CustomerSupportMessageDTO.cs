using System.ComponentModel.DataAnnotations;

namespace Application.Data.DTOs
{
    public class CustomerSupportMessageDTO
    {
        public Guid MessageID { get; set; }
        [Required(ErrorMessage = "Bắt buộc phải nhập lời nhắn")]
        public string? MessageContent { get; set; }
        public Guid? UserID { get; set; }
        public DateTime? CreatedAt { get; set; }
        [Required(ErrorMessage = "Bắt buộc phải nhập Tên")]
        public string? FirstName { get; set; }
        [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
        [Required(ErrorMessage = "Bắt buộc phải nhập Email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [RegularExpression(@"^(03|05|07|08|09)\d{8}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string? PhoneNumber { get; set; }
        public byte? Status { get; set; }

    }
}
