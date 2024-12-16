namespace Application.Data.DTOs
{
    public class SaleDTO
    {
        public Guid? CategoryID { get; set; }
        public Guid? ProductID { get; set; }
        public string? Name { get; set; }
        public long? DiscountPrice { get; set; }
        public DateTime? StartingAt { get; set; }
        public DateTime? EndingAt { get; set; }
        public byte? Status { get; set; }

    }
}
