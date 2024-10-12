using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class Role
	{
		[Key]
		public Guid RoleID { get; set; }
		public string RoleCode { get; set; } = null!;
		public string? RoleName { get; set; }
	}
}
