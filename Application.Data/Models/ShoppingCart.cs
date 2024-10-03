using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class ShoppingCart
	{
		[Key]
		public Guid CartID { get; set; }
		public Guid UserID { get; set; }
		public int QuantityCard {  get; set; }
		public int Size { get; set; }
		public string? Color { get; set; }
		public decimal? Price { get; set; }
		public decimal? Discount { get; set;}
		public DateTime? Created { get; set; }
		public bool IsCheckedOut { get; set;}
		public Guid VoucherID { get; set; }

	}
}
