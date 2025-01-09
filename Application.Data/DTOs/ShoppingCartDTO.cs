namespace Application.Data.DTOs
{
    public class ShoppingCartDTO
    {
        public Guid? UserID { get; set; }
        public Guid? ProductDetailID { get; set; }
        public int? QuantityCart { get; set; }
    }
}
