using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class Product_Details_Size
	{
		[Key]
		public Guid ProductDetailsSizeID { get; set; }
		
		public Guid ProductDetailID { get; set; }
		
		public Guid SizeID { get; set; }

		public virtual ICollection<Product> Products { get; set; } = new List<Product>();
		public virtual ICollection<Size> Sizes { get; set; } = new List<Size>();
	}			
}
