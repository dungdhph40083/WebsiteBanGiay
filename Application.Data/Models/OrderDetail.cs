using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class OrderDetail
    {
        [Key]
        public Guid OrderDetailID { get; set; }
        [ForeignKey(nameof(Order))]
        public Guid? OrderID { get; set; }
        [ForeignKey(nameof(ProductDetail))]
        public Guid? ProductDetailID { get; set; }
        [ForeignKey(nameof(Sale))]
        public Guid? SaleID { get; set; }
        [ForeignKey(nameof(ShippingMethod))]
        public Guid? ShippingMethodID { get; set; }
        [ForeignKey(nameof(Voucher))]
        public Guid? VoucherID { get; set; }
        public int? Quantity { get; set; }
        public long? Price { get; set; }
        public long? TotalUnitPrice { get; set; }
        public long? Discount { get; set; }
        public long? SumTotalPrice { get; set; }
        public DateTime? CreatedAt { get; set; }
        public virtual ProductDetail? ProductDetail { get; set; }
        public virtual ShippingMethod? ShippingMethod { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Sale? Sale { get; set; }
        public virtual Voucher? Voucher { get; set; }
    }
}
