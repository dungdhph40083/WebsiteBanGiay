namespace Application.Data.DTOs
{
    public class ProductDetailDTO
    {
        public Guid? ProductID { get; set; }
        public Guid? SizeID { get; set; }
        public Guid? ColorID { get; set; }
        public Guid? ImageID { get; set; }
        public Guid? CategoryID { get; set; }
        public Guid? SaleID { get; set; }
        public string? Material { get; set; }
        public int? Quantity { get; set; }
        public string? Brand { get; set; }
        public string? PlaceOfOrigin { get; set; }
        public byte? Status { get; set; }
    }
}
