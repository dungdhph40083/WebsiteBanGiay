using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class Category
    {
        [Key]
        public Guid CategoryID { get; set; }
        [ForeignKey(nameof(Category_Product))]
        public Guid ProductID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public virtual Category_Product? Category_Product { get; set; }
    }
}
