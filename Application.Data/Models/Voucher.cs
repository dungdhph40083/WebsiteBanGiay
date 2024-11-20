using Application.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class Voucher
	{
		[Key]
		[Required(ErrorMessage = ValidateErrorResult.BAD_INPUT)]
		public Guid VoucherID { get; set; }
		[ForeignKey(nameof(Category))]
		public Guid? CategoryID { get; set; }
		[ForeignKey(nameof(Product))]
		public Guid? ProductID { get; set; }
        [Range(-1, 10_000_000, ErrorMessage = ValidateErrorResult.OUT_OF_RANGE)]
        public int UsesLeft { get; set; }
        [MaxLength(80, ErrorMessage = ValidateErrorResult.OUT_OF_RANGE)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ValidateErrorResult.EMPTY_FIELD_NOT_ALLOWED)]
        public string VoucherCode { get; set; } = null!;
        [Range(0, 1_000_000_000_000_000, ErrorMessage = ValidateErrorResult.OUT_OF_RANGE)]
        public long DiscountPrice { get; set; }
		[Precision(5, 2)]
        [Range(0.00, 100.00, ErrorMessage = ValidateErrorResult.OUT_OF_RANGE)]
        public decimal DiscountPercent { get; set; }
		public string? Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime LastUpdatedOn { get; set;}
        [Required(ErrorMessage = ValidateErrorResult.BAD_INPUT)]
        public byte Status { get; set; }
		public virtual Category? Category { get; set; }
		public virtual Product? Product { get; set; }
    }
}