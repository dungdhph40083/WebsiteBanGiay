using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class Size_ProductDetail
	{
		[Key]
		public Guid Size_ProductDetailID { get; set; }
		[ForeignKey(nameof(ProductDetail))]
		public Guid ProductDetailID { get; set; }
		[ForeignKey(nameof(Size))]
		public Guid SizeID { get; set; }

		public virtual ProductDetail? ProductDetail { get; set; }
		public virtual Size? Size { get; set; }
	}			
}
