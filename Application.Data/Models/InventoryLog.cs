using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class InventoryLog
	{
		[Key]
		public Guid LogID { get; set; }
		[ForeignKey(nameof(Product_Inventory))]
		public Guid ProductDetailsID { get; set; }
		[Precision(14,2)]
		public decimal? Size { get; set; }
		public string? Color { get; set; }
		public long QuantityInStock { get; set; }
		public byte Status { get; set; }
		public DateTime? CreateDateAt { get; set; }
		public DateTime? CreateDateBy { get;set; }
		public virtual Product_Inventory? Product_Inventory { get; set; }
	}
}
