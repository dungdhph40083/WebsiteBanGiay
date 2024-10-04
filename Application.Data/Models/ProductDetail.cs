using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class ProductDetail
	{
		[Key]
		public Guid ProductDetailsID { get; set; }
        [ForeignKey(nameof(Product))]
		public Guid ProductID { get; set; }
		public string? Material { get; set; }
		public int Quantity { get; set; }
		public long Price { get; set; }
		[ForeignKey(nameof(Product_Details_Color))]
		public Guid ColorID { get; set; }
		[ForeignKey(nameof(Image))]
		public Guid ImageID { get; set; }
		[ForeignKey(nameof(Product_Details_Size))]
		public Guid SizeID { get; set; }
		public string? Brand { get; set; }
		public string? PlaceOfOrigin { get; set; }
		public string? ShoeType { get; set; }
		public DateTime WarrantyPeriod { get; set; }
        [ForeignKey(nameof(Product_Inventory))]
        public Guid LogID { get; set; }

		public virtual Product? Product { get; set; }
		public virtual Product_Details_Color? Product_Details_Color { get; set; }
		public virtual Image? Image { get; set; }
		public virtual Product_Details_Size? Product_Details_Size { get; set; }
		public virtual ICollection<Product_Inventory> Product_Inventory { get; set; } = new List<Product_Inventory>();
    }
}
