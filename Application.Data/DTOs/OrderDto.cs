using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.DTOs
{
    public class OrderDto
    {
        public Guid OrderID { get; set; }
        public Guid? UserID { get; set; }
        public DateTime? OrderDate { get; set; }
        public byte Status { get; set; }
        public string? ShippingAddress { get; set; }
        public Guid? PaymentMethodID { get; set; }
    }
}
