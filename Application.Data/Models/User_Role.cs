using Application.Data.Enums;
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
        [Required(ErrorMessage = ValidateErrorResult.BAD_INPUT)]
        public Guid User_RoleID { get; set; }
		[ForeignKey(nameof(User))]
		[Required(ErrorMessage = ValidateErrorResult.EMPTY_FIELD_NOT_ALLOWED)]
		public Guid UserID { get; set; }
        [ForeignKey(nameof(Role))]
        [Required(ErrorMessage = ValidateErrorResult.EMPTY_FIELD_NOT_ALLOWED)]
        public Guid RoleID { get; set; }

		public virtual User? User { get; set; }
		public virtual Role? Role { get; set; }
	}
}
