using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class Address
	{
		[Key]
		public Guid AddressID { get; set; }
		[ForeignKey(nameof(User))]
		public Guid UserID { get; set; }
		[ForeignKey(nameof(Role))]
		public Guid RoleID { get; set; }
		public string? Name {  get; set; }
		public string? PhoneNumber { get; set; }
		public string? Description { get; set; }
		public string? DefaultAddress { get; set; }
		public byte Status {  get; set; }
		public virtual User? User { get; set; }
		public virtual Role? Role { get; set; }
	}
}
