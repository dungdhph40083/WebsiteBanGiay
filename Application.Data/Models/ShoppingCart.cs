using Application.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
	public class ShoppingCart
	{
		[Key]
        public Guid CartID { get; set; }
		[ForeignKey(nameof(User))]
		public Guid? UserID { get; set; }
		[ForeignKey(nameof(ProductDetail))]
		public Guid? ProductDetailID { get; set; }
        public int? QuantityCart { get; set; }
        public long? Price { get; set; }
		public virtual User? User { get; set; }
        public virtual ProductDetail? ProductDetail { get; set; }
        public virtual Voucher? Voucher { get; set; }
	}
}
