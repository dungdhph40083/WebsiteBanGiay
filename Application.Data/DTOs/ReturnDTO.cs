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
        public byte? Status { get; set; }
    }
}
