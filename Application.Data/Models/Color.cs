using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class Color
	{
		[Key]
		public Guid ColorID { get; set; }
		public string? ColorName { get; set; }
		public byte Status { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set;}
	}
}
