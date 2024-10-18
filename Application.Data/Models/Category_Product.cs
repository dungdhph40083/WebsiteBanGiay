using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class Category_Product
    {
        [Key]
        public Guid? Category_Products_ID { get; set; }
        [ForeignKey(nameof(Product))]
        public Guid? ProductID { get; set; }
        [ForeignKey(nameof(Category))]
        public Guid? CategoryID { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Category? Category { get; set; }
    }
}
