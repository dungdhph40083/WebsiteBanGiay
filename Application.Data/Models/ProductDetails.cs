using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class ProductDetails
	{
		[Key]
		public Guid ProductDetailsID { get; set; }
		public Guid ProductID { get; set; }
		public string? material { get; set; }
		public int Quantity { get; set; }
		public decimal? Price { get; set;}
		public Guid ColorID { get; set; }
		public Guid ImageID { get; set; }
		public Guid SizeID { get; set; }
		public string? Brand { get; set; }
		public string? PlaceOfOrigin { get; set; }
		public string? ShoeType { get; set; }
		public int WarrantyPeriod { get; set; }
		public Guid LogID { get; set; }
		public Guid RatingID { get; set; }
	}
}
