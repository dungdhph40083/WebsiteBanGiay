using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class Return
    {
        [Key]
        public Guid ReturnID { get; set; }
        [ForeignKey(nameof(Order))]
        public Guid OrderID { get; set; }
        public string? Reason { get; set; }
        public DateTime ReturnDate { get; set; }
        public long RefundAmount { get; set; }
        public byte Status { get; set; }
        public virtual ICollection<Order>? Order { get; set; } = new List<Order>();
    }
}
