using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class PaymentMethodDetail
    {
        [Key]
        public Guid PaymentMethodDetailID { get; set; }
        [ForeignKey(nameof(PaymentMethod))]
        public Guid? PaymentMethodID { get; set; } 
        public long? TotalMoney { get; set; }
        public byte? Status { get; set; }
        public string? Description { get; set; }
        public virtual PaymentMethod? PaymentMethod { get; set; }
    }
}
