
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.DTOs
{
    public class OrderDetailDto
    {
		public Guid OrderDetailID { get; set; }

		public Guid? OrderID { get; set; }

		public Guid? ProductID { get; set; }

		public Guid? SizeID { get; set; }

		public Guid? ColorID { get; set; }

		public Guid? VoucherID { get; set; }

		public Guid? ShippingMethodID { get; set; }
		public int Quantity { get; set; }
		public long Price { get; set; }
		public long TotalUnitPrice { get; set; }
		public int Discount { get; set; }
		public long SumTotalPrice { get; set; }
		public DateTime? CreatedAt { get; set; }
	}
}
