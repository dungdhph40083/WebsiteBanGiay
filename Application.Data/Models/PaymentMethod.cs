using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class PaymentMethod
    {
        [Key]
        public Guid PaymentMethodID { get; set; }
        public string? MethodName { get; set; }
        public string? Description { get; set; }
        public byte? Status { get; set; }
        public DateTime? CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }
    }
}
