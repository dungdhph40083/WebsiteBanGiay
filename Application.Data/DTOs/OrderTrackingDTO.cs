using Application.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.DTOs
{
    public class OrderTrackingDTO
    {
        public Guid TrackingID { get; set; }
        public Guid? OrderID { get; set; }
        public byte? Status { get; set; }
        public bool? HasPaid { get; set; }
    }
}
