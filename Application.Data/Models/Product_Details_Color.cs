using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
	}
}
