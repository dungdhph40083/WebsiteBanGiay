namespace Application.Data.DTOs
{
    public class OrderDetailDto
    {
        public Guid? OrderID { get; set; }
        public Guid? ProductID { get; set; }
        public Guid? SizeID { get; set; }
        public Guid? ColorID { get; set; }
        public Guid? SaleID { get; set; }
        public Guid? ShippingMethodID { get; set; }
        public Guid? VoucherID { get; set; }
        public int? Quantity { get; set; }
        public long? Price { get; set; }
        public long? TotalUnitPrice { get; set; }
        public int? Discount { get; set; }
        public long? SumTotalPrice { get; set; }
    }
}
