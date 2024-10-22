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
		public Guid ProductDetailID { get; set; }
        [ForeignKey(nameof(Product))]
		public Guid? ProductID { get; set; }
		[ForeignKey(nameof(Image))]
		public Guid? ImageID { get; set; }
		public string? Material { get; set; }
		public int Quantity { get; set; }
		public long Price { get; set; }
        public string? Brand { get; set; }
        public string? PlaceOfOrigin { get; set; }
        public string? Type { get; set; }
        public DateTime WarrantyPeriod { get; set; }
		public string? Instructions { get; set; }
		public string? Features { get; set; }
		public virtual Product? Product { get; set; }
		public virtual Image? Image { get; set; }
    }
}
