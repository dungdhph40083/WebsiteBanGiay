using System.ComponentModel.DataAnnotations;

namespace Application.Data.Models
{
    public class ShippingMethod
    {
        [Key]
        public Guid ShippingMethodID { get; set; }
        public string? MethodName { get; set; }
        public long ShippingFee { get; set; }
        public DateTime EstimatedDeliveryTime { get; set; }
        public virtual OrderDetail? OrderDetail { get; set; }
    }
}
