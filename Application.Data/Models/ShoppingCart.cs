using Application.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
	public class ShoppingCart
	{
		[Key]
        [Required(ErrorMessage = ValidateErrorResult.EMPTY_FIELD_NOT_ALLOWED)]
        public Guid CartID { get; set; }
		[ForeignKey(nameof(User))]
		public Guid? UserID { get; set; }
        [Range(1, 20, ErrorMessage = ValidateErrorResult.OUT_OF_RANGE)]
        public int QuantityCart { get; set; }
		[ForeignKey(nameof(Product))]
		public Guid? ProductID { get; set; }
		[ForeignKey(nameof(Size))]
		public Guid? SizeID { get; set; }
		[ForeignKey(nameof(Color))]
		public Guid? ColorID { get; set; }
		public long RawPrice { get; set; }
		public long Discount { get; set; }
		public long FinalPrice { get; set; }
		public DateTime DateAdded { get; set; }
        [Required(ErrorMessage = ValidateErrorResult.EMPTY_FIELD_NOT_ALLOWED)]
        public bool IsCheckedOut { get; set;}
		[ForeignKey(nameof(Voucher))]
		public Guid? VoucherID { get; set; }
		
		public virtual User? User { get; set; }
		public virtual Voucher? Voucher { get; set; }
		public virtual Size? Size { get; set; }
		public virtual Color? Color { get; set; }
		public virtual Product? Product { get; set; }
	}
}
