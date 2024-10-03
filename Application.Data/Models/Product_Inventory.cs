using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class Product_Inventory
	{
		[Key]
		public Guid ProductInventoryID { get; set; }
		public Guid LogID { get; set; }
		public Guid ProductDetailsID { get; set; }
	}
}
