using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class Size
	{
		[Key]
		public Guid SizeID { get; set; }
		public string? Name { get; set; }
		public byte Status { get; set; }
		public DateTime CreatedAt {  get; set; }
		public DateTime UpdatedAt { get; set; }

	}
}
