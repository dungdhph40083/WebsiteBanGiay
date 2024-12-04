using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class OrderTracking
    {
        [Key]
        public Guid TrackingID { get; set; }
        [ForeignKey(nameof(OrderDetail))] 
        public Guid? OrderDetailID { get; set; }
        public byte ShippingStatus { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual OrderDetail? OrderDetail { get; set; }
    }
}
