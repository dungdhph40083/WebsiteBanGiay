using Application.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Application.Data.DTOs
{
    public class VoucherDTO
    {
        public Guid? CategoryID { get; set; }
        [Range(1, 100000, ErrorMessage = "Nhập số lượng từ 1 đến 100.000")]
        [Required(ErrorMessage = "Vui lòng nhập số lượng")]

        public int? UsesLeft { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9]{3,15}$", ErrorMessage = "Mã voucher phải chỉ chứa chữ cái và số, với độ dài từ 3 đến 15 ký tự.")]
        [Required(ErrorMessage = "Vui lòng nhập Mã voucher")]

        public string VoucherCode { get; set; } = null!;

        [Range(1000, 500000000, ErrorMessage = "Tổng giá trị tối thiểu phải từ 1.000 đến 500.000.000.")]
        [Required(ErrorMessage = "Vui lòng nhập giá trị tối thiểu")]

        public long? RequiredGrandTotal { get; set; }
        [Range(1, 100, ErrorMessage = "Phần trăm giảm giá phải từ 1 đến 100.")]
        [Required(ErrorMessage = "Vui lòng nhập % muốn giảm")]

        public decimal? DiscountPercent { get; set; }
        [Range(1000, 100000000, ErrorMessage = "Phần giá tiền giảm giá phải từ 1.000 đến 1.000.000.000.")]
        [Required(ErrorMessage = "Vui lòng nhập giá tiền giảm")]

        public long? DiscountPrice { get; set; }
        public bool UseDiscountPrice { get; set; }
        [MaxLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
        [Required(ErrorMessage = "Vui lòng nhập mô tả")]

        public string? Description { get; set; }
        public DateTime? StartingAt { get; set; }
        public DateTime? EndingAt { get; set; }
        public byte? Status { get; set; }
    }
}
