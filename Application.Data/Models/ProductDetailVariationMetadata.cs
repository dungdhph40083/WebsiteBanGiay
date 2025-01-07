using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
	public class ProductDetailVariationMetadata
	{
		public int VariationsAdded { get; set; }
		public int VariationsNotAdded { get; set; }
		public List<ProductDetail> Variations { get; set; } = [];
    }
}
