using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class ProductReviews
	{
		[Key]
		public Guid ReviewID { get; set; }
		public Guid ProductDetailID { get; set; }
		public Guid UserID { get; set; }
		public float RatingStar {  get; set; }
		public string? Comment { get; set;}
		public DateTime? ReviewDate { get; set; }
	}
}
