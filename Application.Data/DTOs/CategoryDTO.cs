using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.DTOs
{
    public class CategoryDTO
    {
        public Guid CategoryID { get; set; }
        // public Guid ProductID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
