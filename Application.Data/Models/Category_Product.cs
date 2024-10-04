using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class Category_Product
    {
        [Key]
        public Guid CategoryProductID { get; set; }
        
        public Guid ProductID { get; set; }
        
        public Guid CategoryID { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
