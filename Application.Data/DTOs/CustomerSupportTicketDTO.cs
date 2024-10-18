using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.DTOs
{
    public class CustomerSupportTicketDTO
    {
        public Guid TicketID { get; set; }
        public Guid? UserID { get; set; } 
        public string? Subject { get; set; }
        public byte Status { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
