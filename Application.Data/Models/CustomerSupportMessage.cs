using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Application.Data.Models
{
    public class CustomerSupportMessage
    {
        [Key]
        public Guid MessageID { get; set; }
        public string? MessageContent { get; set; }
        [ForeignKey(nameof(User))]
        public Guid? UserID { get; set; }
        public virtual User? User { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public byte? Status { get; set; }
    }
}