using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.DTOs
{
    public class ShippingMethodDTO
    {
        public string? MethodName { get; set; }
        public long ShippingFee { get; set; }
        public DateTime EstimatedDeliveryTime { get; set; }
    }
}
