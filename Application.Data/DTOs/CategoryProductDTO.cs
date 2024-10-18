using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.DTOs
{
    public class CategoryProductDTO
    {
        public Guid? Category_Products_ID { get; set; }
        public Guid? ProductID { get; set; } 
        public Guid? CategoryID { get; set; }
    }
}
