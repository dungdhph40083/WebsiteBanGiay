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
		public Guid User_RoleID { get; set; }
		[ForeignKey(nameof(User))]
		public Guid UserID { get; set; }
        [ForeignKey(nameof(Role))]
        public Guid RoleID { get; set; }

		public virtual User? User { get; set; }
		public virtual Role? Role { get; set; }
	}
}
