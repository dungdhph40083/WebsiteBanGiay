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
        public Guid SizeID { get; set; }
        public Guid ColorID { get; set; }
        public long QuantityInStock { get; set; }
        public byte Status { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
