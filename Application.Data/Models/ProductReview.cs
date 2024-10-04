using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class ProductReview
	{
		[Key]
		public Guid ReviewID { get; set; }
		[ForeignKey(nameof(ProductDetail))]
		public Guid ProductDetailID { get; set; }
		[ForeignKey(nameof(User))]
		public Guid UserID { get; set; }
		public float RatingStar {  get; set; }
		public string? Comment { get; set;}
		public DateTime? ReviewDate { get; set; }
		public virtual ProductDetail? ProductDetail { get; set; }
		public virtual User? User { get; set; }
	}
}
