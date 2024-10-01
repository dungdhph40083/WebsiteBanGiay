using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class ProductWarranty
    {
        [Key]
        public Guid WarrantyID { get; set; }
        [ForeignKey(nameof(Product))]
        public Guid ProductID { get; set; }
        public DateTime WarrantyPeriod { get; set; }
        public string? WarrantyConditions { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public virtual Product? Product { get; set; }
    }
}
