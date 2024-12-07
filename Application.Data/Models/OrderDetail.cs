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
        [ForeignKey(nameof(Product))]
        public Guid? ProductID { get; set; }
        [ForeignKey(nameof(Size))]
        public Guid? SizeID { get; set; }
        [ForeignKey(nameof(Color))]
        public Guid? ColorID { get; set; }
        [ForeignKey(nameof(Sale))]
        public Guid? SaleID { get; set; }
        [ForeignKey(nameof(ShippingMethod))]
        public Guid? ShippingMethodID { get; set; }
        [ForeignKey(nameof(Voucher))]
        public Guid? VoucherID { get; set; }
        public int? Quantity { get; set; }
        public long? Price { get; set; }
        public long? TotalUnitPrice { get; set; }
        public int? Discount { get; set; }
        public long? SumTotalPrice { get; set; }
        public DateTime? CreatedAt { get; set; }
        public virtual Product? Product { get; set; }
        public virtual ShippingMethod? ShippingMethod { get; set; }
        public virtual Size? Size { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Color? Color { get; set; }
        public virtual Sale? Sale { get; set; }
        public virtual Voucher? Voucher { get; set; }
    }
}
