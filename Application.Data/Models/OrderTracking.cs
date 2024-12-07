using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class OrderTracking
    {
        [Key]
        public Guid TrackingID { get; set; }
        [ForeignKey(nameof(Order))] 
        public Guid? OrderID { get; set; }
        public byte? ShippingStatus { get; set; }
        public virtual Order? Order { get; set; }
    }
}
