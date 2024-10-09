using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class Inventory_ProductDetail
	{
		[Key]
		public Guid Inventory_ProductDetailID { get; set; }
		[ForeignKey(nameof(InventoryLog))]
		public Guid? LogID { get; set; }
		[ForeignKey(nameof(ProductDetail))]
		public Guid? ProductDetailsID { get; set; }
		public virtual InventoryLog? InventoryLog { get; set; }
		public virtual ProductDetail? ProductDetail { get; set; }
	}
}
