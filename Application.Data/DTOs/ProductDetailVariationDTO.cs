using System.ComponentModel.DataAnnotations;

namespace Application.Data.DTOs
{
    public class ProductDetailVariationDTO
    {
        public Guid ProductDetailID { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Vui lòng chọn kích cỡ")]
        public Guid? SizeID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn màu")]
        public Guid? ColorID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [Range(0, 10000000, ErrorMessage = "Số lượng phải bằng lớn hơn 0 và không vượt quá 10 triệu.")]
        public int? Quantity { get; set; }
    }
}
