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
        public long Price { get; set; }
        public byte[]? Image { get; set; }
        [ForeignKey(nameof(Category_Product))]
        public Guid CategoryID { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public Guid OrderID { get; set; }
        public virtual Category_Product? Category_Product { get; set; }
    }
}
