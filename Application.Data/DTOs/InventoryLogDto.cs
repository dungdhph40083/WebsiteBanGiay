using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.DTOs
{
    public class InventoryLogDto
    {
        public Guid LogID { get; set; }
        public Guid? SizeID { get; set; }
        public Guid? ColorID { get; set; }
        public Guid? ProductDetailID { get; set; }
        public int QuantityInStock { get; set; }
        public byte Status { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
