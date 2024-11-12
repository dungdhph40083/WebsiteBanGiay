namespace Application.Data.DTOs
{
    public class ProductDetailDTO
    {
        public Guid? ProductID { get; set; }
        public Guid? ImageID { get; set; }
        public string? Material { get; set; }
        public int Quantity { get; set; }
        public long Price { get; set; }
        public string? Brand { get; set; }
        public string? PlaceOfOrigin { get; set; }
        public string? Type { get; set; }
        public DateTime WarrantyPeriod { get; set; }
        public string? Instructions { get; set; }
        public string? Features { get; set; }
    }
}
