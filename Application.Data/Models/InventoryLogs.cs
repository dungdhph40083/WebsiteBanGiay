using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class InventoryLogs
	{
		[Key]
		public Guid LogID { get; set; }
		public Guid ProductDetailsID { get; set; }
		public decimal? Size { get; set; }
		public string? Color { get; set; }
		public string? QuantityInStock { get; set; }
		public byte Status { get; set; }
		public DateTime? CreateDateAt { get; set; }
		public DateTime? CreateDateBy { get;set; }
	}
}
