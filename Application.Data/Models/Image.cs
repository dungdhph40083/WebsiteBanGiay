using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class Image
	{
		[Key]
		public Guid ImageID { get; set; }
		public string? ImageName { get; set; }
		public byte[]? ImageData { get; set; }
		public byte Status { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? UpdateDate { get; set; }
	}
}
