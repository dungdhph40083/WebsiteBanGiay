using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class Order
    {
		[Key]
		public Guid OrderID { get; set; }

		[ForeignKey(nameof(User))]
		public Guid? UserID { get; set; }

		public DateTime OrderDate { get; set; }
		public byte Status { get; set; }
		public long TotalPrice { get; set; }
		public string? ShippingAddress { get; set; }

		[ForeignKey(nameof(PaymentMethod))]
		public Guid? PaymentMethodID { get; set; }

		public virtual User? User { get; set; }
		public virtual PaymentMethod? PaymentMethod { get; set; }
	}
}
