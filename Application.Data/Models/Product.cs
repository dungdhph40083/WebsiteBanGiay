using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class Product
    {
        [Key]
        public Guid ProductID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [ForeignKey(nameof(Image))]
        public Guid? ImageID { get; set; }
        public long Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual Image? Image { get; set; }
	}
}