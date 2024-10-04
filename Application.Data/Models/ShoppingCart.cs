using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class ShoppingCart
	{
		[Key]
		public Guid CartID { get; set; }
		[ForeignKey(nameof(User))]
		public Guid UserID { get; set; }
		public int QuantityCard {  get; set; }
		[Precision(14, 2)]
		public decimal? Size { get; set; }
		public string? Color { get; set; }
		public long? Price { get; set; }
		[Precision(14, 2)]
		public decimal? Discount { get; set;}
		public DateTime? Created { get; set; }
		public bool IsCheckedOut { get; set;}
		[ForeignKey(nameof(Voucher))]
		public Guid VoucherID { get; set; }
		
		public virtual User? User { get; set; }
		public virtual Voucher? Voucher { get; set; }

	}
}
