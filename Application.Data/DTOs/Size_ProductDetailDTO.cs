using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.DTOs
{
    public class Size_ProductDetailDTO
    {
        public Guid ProductDetailID { get; set; }
        public Guid SizeID { get; set; }
    }
}
