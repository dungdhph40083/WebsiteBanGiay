
using Application.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.DTOs
{
    public class ShoppingCartDTO
    {
        public Guid? UserID { get; set; }
        public int QuantityCart { get; set; }
        public Guid? SizeID { get; set; }
        public Guid? ColorID { get; set; }
        public long? Price { get; set; }
        public long Discount { get; set; }
        public DateTime? DateAdded { get; set; }
        public bool IsCheckedOut { get; set; }
        public Guid? VoucherID { get; set; }
    }
}
