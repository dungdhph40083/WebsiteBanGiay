using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class CustomerSupportTicket
	{
		[Key]
		public Guid TicketID { get; set; }
		[ForeignKey(nameof(User))]
		public Guid? UserID { get; set; }
		public string? Subject { get; set; }
		public string? Message { get; set; }
		public byte Status { get; set; }
		public DateTime? CreateAt { get; set; }
		public DateTime? CreateBy { get; set; }
		public virtual User? User { get; set; }
		public virtual ICollection<CustomerSupportMessage> CustomerSupportMessages { get; set; } = new List<CustomerSupportMessage>();
	}
}
