using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.DTOs
{
    public class PaymentMethodDTO
    {
        public string? MethodName { get; set; }
        public string? Description { get; set; }
        public byte? Status { get; set; }
    }
}
