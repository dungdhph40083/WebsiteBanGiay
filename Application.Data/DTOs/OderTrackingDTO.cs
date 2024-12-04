using Application.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.DTOs
{
    public class OderTrackingDTO
    {
        public Guid TrackingID { get; set; }
        public Guid? OrderDetailID { get; set; }
        public byte ShippingStatus { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
