using Application.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.DTOs
{
    public class Size_ProductDetailDTO
    {
        [Required(ErrorMessage = "Required.")]
        public Guid ProductDetailID { get; set; }
        [Required(ErrorMessage = "Required.")]
        public Guid SizeID { get; set; }
    }
}
