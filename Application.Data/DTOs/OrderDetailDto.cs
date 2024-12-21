namespace Application.Data.DTOs
{
    public class OrderDetailDto
    {
        public Guid? OrderID { get; set; }
        public Guid? ProductDetailID { get; set; }
        public Guid? SaleID { get; set; }
        public Guid? ShippingMethodID { get; set; }
        public Guid? VoucherID { get; set; }
        public int? Quantity { get; set; }
        public long? Price { get; set; }
        public long? TotalUnitPrice { get; set; }
        public long? Discount { get; set; }
        public long? SumTotalPrice { get; set; }
    }
}
