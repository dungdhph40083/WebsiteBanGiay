using Application.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class Sale
    {
        [Key]
        public Guid SaleID { get; set; }
        [ForeignKey(nameof(Category))]
        public Guid? CategoryID { get; set; }
        [ForeignKey(nameof(Product))]
        public Guid? ProductID { get; set; }
        public string? Name { get; set; }
        public DateTime? StartingAt { get; set; }
        public DateTime? EndingAt { get; set; }
        public byte? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Category? Category { get; set; }
    }
}
