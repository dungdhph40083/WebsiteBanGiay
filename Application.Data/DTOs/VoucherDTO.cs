using Application.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Application.Data.DTOs
{
    public class VoucherDTO
    {
        public Guid? CategoryID { get; set; }
        public Guid? ProductID { get; set; }
        [Range(-1, 10_000_000, ErrorMessage = "Must be 0 ~ 10M, set -1 for inf.")]
        public int UsesLeft { get; set; }
        [MaxLength(80, ErrorMessage = "Voucher code too long!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required.")]
        public string VoucherCode { get; set; } = null!;
        [Range(0, 1_000_000_000_000_000, ErrorMessage = "Must be 0 ~ 10Q.")]
        public long DiscountPrice { get; set; }
        [Precision(5, 2)]
        [Range(0.00, 100.00, ErrorMessage = "Must be 0% ~ 100%.")]
        public decimal DiscountPercent { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Invalid.")]
        public byte Status { get; set; }
    }
}
