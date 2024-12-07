using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Application.Data.Models
{
    public class CustomerSupportMessage
    {
        [Key]
        public long MessageID { get; set; }
        public string? MessageContent { get; set; }
        [ForeignKey(nameof(User))]
        public Guid? UserID { get; set; }
        public virtual User? User { get; set; }
    }
}