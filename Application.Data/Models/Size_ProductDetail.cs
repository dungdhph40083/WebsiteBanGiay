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
		public Guid Size_ProductDetail_ID { get; set; }
		[ForeignKey(nameof(Product))]
		public Guid ProductID { get; set; }
		[ForeignKey(nameof(Size))]
		public Guid SizeID { get; set; }

		public virtual Product? Product { get; set; }
		public virtual Size? Size { get; set; }
	}			
}
