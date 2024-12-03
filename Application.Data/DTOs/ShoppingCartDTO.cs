using Application.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Data.DTOs
{
    public class ShoppingCartDTO
    {
        public Guid? UserID { get; set; }
        public Guid? ProductID { get; set; }
        public Guid? SizeID { get; set; }
        public Guid? ColorID { get; set; }
        public Guid? VoucherID { get; set; }
        [Range(1, 20, ErrorMessage = "Min 1 to max 20 items.")]
        public int QuantityCart { get; set; }
        [Required(ErrorMessage = "Required.")]
        public bool IsCheckedOut { get; set; }
    }
}
