using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
		public virtual ICollection<InventoryLog> InventoryLogs { get; set; } = new List<InventoryLog>();
		public virtual ICollection<ProductDetail> ProductDetails { get; set; } = new List<ProductDetail>();
	}
}
