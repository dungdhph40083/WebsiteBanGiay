using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class Category
    {
        [Key]
        public Guid CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        // public virtual ICollection<Category_Product> Category_Products { get; set; } = new List<Category_Product>();
        // public virtual ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
    }
}
