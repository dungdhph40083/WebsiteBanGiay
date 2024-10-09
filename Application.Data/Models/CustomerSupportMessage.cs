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
        [ForeignKey(nameof(CustomerSupportTicket))]
        public Guid? TicketID { get; set; }
    }
}