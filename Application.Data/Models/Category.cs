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
    }
}
