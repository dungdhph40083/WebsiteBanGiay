using System.ComponentModel.DataAnnotations;

namespace Application.Data.DTOs
{
    public class ProductDetailDTO
    {
        [Required]
        public Guid? ProductID { get; set; }
        [Required]
        public Guid? SizeID { get; set; }
        [Required]
        public Guid? ColorID { get; set; }
        [Required]
        public Guid? CategoryID { get; set; }
        public string? Material { get; set; }
        public int? Quantity { get; set; }
        public string? Brand { get; set; }
        public string? PlaceOfOrigin { get; set; }
        public byte? Status { get; set; }
    }
}
