using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.DTOs
{
    public class ReturnDTO
    {
        public Guid? OrderID { get; set; }
        public string? Reason { get; set; }
        public DateTime? ReturnDate { get; set; }
        public long? RefundAmount { get; set; }
        public byte? Status { get; set; }
    }
}
