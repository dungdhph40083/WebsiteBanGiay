using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class Address
	{
		[Key]
		public Guid AddressID { get; set; }
		public Guid UserID { get; set; }
		public Guid RoleID { get; set; }
		public string? Name {  get; set; }
		public decimal? PhoneNumber { get; set; }
		public string? Description { get; set; }
		public string? DefaultAddress { get; set; }
		public byte Status {  get; set; }
	}
}
