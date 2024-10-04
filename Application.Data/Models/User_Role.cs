using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class User_Role
	{
		[Key]
		public Guid UserRoleID { get; set; }
		
		public Guid UserID { get; set; }
		
		public Guid RoleID { get; set; }

		public virtual ICollection<User> Users { get; set; } = new List<User>();
		public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
	}
}
