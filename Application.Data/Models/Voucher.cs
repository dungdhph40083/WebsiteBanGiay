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
		public Guid VoucherID { get; set; }
		[ForeignKey(nameof(Category))]
		public Guid? CategoryID { get; set; }
		[ForeignKey(nameof(Product))]
		public Guid? ProductID {  get; set; }
		public int UsesLeft { get; set; }
		[MaxLength(80)]
		public string VoucherCode { get; set; } = null!;
		public long DiscountPrice { get; set; }
		[Precision(5, 2)]
		public decimal DiscountPercent { get; set; }
		public string? Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime LastUpdatedOn { get; set;}
		public byte Status { get; set; }
		public virtual Category? Category { get; set; }
		public virtual Product? Product { get; set; }
    }
}