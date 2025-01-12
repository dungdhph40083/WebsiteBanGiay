using System.ComponentModel.DataAnnotations;

namespace Application.Data.DTOs
{
    public class ProductDetailMultiDTO
    {
        [Required(ErrorMessage = "Vui lòng chọn sản phẩm")]
        public Guid? ProductID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public Guid? CategoryID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập chất liệu")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Chất liệu phải có độ dài từ 3 đến 100 ký tự.")]
        public string? Material { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thương hiệu")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Thương hiệu phải có độ dài từ 3 đến 100 ký tự.")]
        public string? Brand { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập xuất xứ")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Xuất xứ phải có độ dài từ 3 đến 100 ký tự.")]
        public string? PlaceOfOrigin { get; set; }
        public byte? Status { get; set; }
        public List<ProductDetailVariationDTO> Variations { get; set; } = [];
    }
}
