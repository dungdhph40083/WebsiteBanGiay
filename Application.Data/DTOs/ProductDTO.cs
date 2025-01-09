using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Data.DTOs
{
    public class ProductDTO
    {
        public Guid? ImageID { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc.")]
        [StringLength(200, ErrorMessage = "Tên sản phẩm không được vượt quá 200 ký tự.")]
        [RegularExpression(@"^[a-zA-Z0-9\sàáảãạâấầẩẫậăắằẳẵặđèéẻẽẹêếềểễệìíỉĩịòóỏõọôốồổỗộơớờởỡợùúủũụưứừửữựýỳỷỹỵ]+$", ErrorMessage = "Tên sản phẩm chỉ được chứa chữ cái, số, dấu và khoảng trắng.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Mô tả sản phẩm là bắt buộc.")]
        [StringLength(500, ErrorMessage = "Mô tả sản phẩm không được vượt quá 500 ký tự.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Giá sản phẩm là bắt buộc.")]
        [Range(10000, 200000000, ErrorMessage = "Giá sản phẩm phải nằm trong khoảng từ 10,000 đến 200,000,000.")]
        public long? Price { get; set; }

    }
}
