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
		public Guid? UserID { get; set; }
		public int QuantityCart { get; set; }
		[ForeignKey(nameof(Product))]
		public Guid? ProductID { get; set; }
		[ForeignKey(nameof(Size))]
		public Guid? SizeID { get; set; }
		[ForeignKey(nameof(Color))]
		public Guid? ColorID { get; set; }
		public long? Price { get; set; }
		public long Discount { get; set;}
		public DateTime? DateAdded { get; set; }
		public bool IsCheckedOut { get; set;}
		[ForeignKey(nameof(Voucher))]
		public Guid? VoucherID { get; set; }
		
		public virtual User? User { get; set; }
		public virtual Voucher? Voucher { get; set; }
		public virtual Size? Size { get; set; }
		public virtual Color? Color { get; set; }
		public virtual Product? Product { get; set; }
	}
}
