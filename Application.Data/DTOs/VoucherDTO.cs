using Application.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.DTOs
{
    public class VoucherDTO
    {
        public Guid? CategoryID { get; set; }
        public Guid? ProductID { get; set; }
        public long DiscountPrice { get; set; }
        [Precision(5, 2)]
        public decimal DiscountPercent { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public byte Status { get; set; }
    }
}
