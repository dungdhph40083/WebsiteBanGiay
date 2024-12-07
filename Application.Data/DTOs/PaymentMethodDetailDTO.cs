using Application.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.DTOs
{
    public class PaymentMethodDetailDTO
    {
        public Guid? PaymentMethodID { get; set; }
        public long TotalMoney { get; set; }
        public byte Status { get; set; }
        public string? Description { get; set; }
    }
}
