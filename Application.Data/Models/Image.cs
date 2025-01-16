using Application.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
	public class Image
	{
		[Key]
        public Guid ImageID { get; set; }
		public string? ImageFileName { get; set; }
		[ForeignKey(nameof(Product))]
		public Guid? ProductID { get; set; }
		public virtual Product? Product { get; set; }
	}
}
