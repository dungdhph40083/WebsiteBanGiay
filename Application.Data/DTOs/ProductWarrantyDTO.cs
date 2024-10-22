using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.DTOs
{
    public class ProductWarrantyDTO
    {
        public Guid? ProductID { get; set; }
        public DateTime WarrantyPeriod { get; set; }
        public string? WarrantyConditions { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
