using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class Product_Details_Size
	{
		[Key]
		public Guid ProductDetailsSizeID { get; set; }
		public Guid ProductID { get; set; }
		public Guid SizeID { get; set; }
	}			
}
