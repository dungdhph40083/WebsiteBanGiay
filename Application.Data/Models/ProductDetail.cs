using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
	public class ProductDetail
	{
		[Key]
		public Guid ProductDetailID { get; set; }
        [ForeignKey(nameof(Product))]
		public Guid? ProductID { get; set; }
        [ForeignKey(nameof(Size))]
        public Guid? SizeID { get; set; }
        [ForeignKey(nameof(Color))]
		public Guid? ColorID { get; set; }
        [ForeignKey(nameof(Category))]
        public Guid? CategoryID { get; set; }
        public string? Material { get; set; }
		public int? Quantity { get; set; }
        public string? Brand { get; set; }
        public string? PlaceOfOrigin { get; set; }
		public byte? Status { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public virtual Product? Product { get; set; }
		public virtual Color? Color { get; set; }
		public virtual Size? Size { get; set; }
		public virtual Category? Category { get; set; }
    }
}
