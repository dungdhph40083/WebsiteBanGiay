using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class OrderDetail
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey(nameof(Order))]
        public Guid? OrderID { get; set; }
        public Order? Order { get; set; }
        [ForeignKey(nameof(Product))]
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        public long Price { get; set; }
        public long TotalUnitPrice { get; set; }
        public int Size { get; set; }
        public string? Color { get; set; }
        public int Discount { get; set; }
        public long TotalSumPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey(nameof(ShippingMethod))]
        public Guid? ShippingMethodID { get; set; }
        public virtual Product? Product { get; set; }
        public virtual ShippingMethod? ShippingMethod { get; set; }

	}
}
