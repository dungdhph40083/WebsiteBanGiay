using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class Product_Details_Color
	{
		[Key]
		public Guid ProductDetailsColorID { get; set; }
		
		public Guid ColorID { get; set; }
		
		public Guid ProductDetailsID { get; set; }

		public virtual ICollection<Color> Colors { get; set; } = new List<Color>();
		public virtual ICollection<ProductDetail> ProductDetails { get; set; } = new List<ProductDetail>();
	}
}
