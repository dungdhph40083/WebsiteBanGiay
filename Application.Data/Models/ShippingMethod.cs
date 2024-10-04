using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class ShippingMethod
    {
        [Key]
        public Guid ShippingMethodID { get; set; }
        
        public Guid OrderDetailID { get; set; }
        public string? MethodName { get; set; }
        public long ShippingFee { get; set; }
        public DateTime EstimatedDeliveryTime { get; set; }
        public virtual OrderDetail? OrderDetail { get; set; }
    }
}
