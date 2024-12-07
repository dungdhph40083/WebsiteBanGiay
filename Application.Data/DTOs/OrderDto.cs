namespace Application.Data.DTOs
{
    public class OrderDto
    {
        public Guid? UserID { get; set; }
        public Guid? PaymentMethodID { get; set; }
        public byte? Status { get; set; }
        public string? ShippingAddress { get; set; }
    }
}
