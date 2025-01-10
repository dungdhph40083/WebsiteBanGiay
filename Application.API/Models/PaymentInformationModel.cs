namespace Application.API.Models
{
    public class PaymentInformationModel
    {
        public string OrderType { get; set; }
        public double Amount { get; set; }
        public string OrderDescription { get; set; }
        public string Name { get; set; }

        public string MaHoaDon { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string HinhThucNhanHang { get; set; }
        public string DiaChi { get; set; }
        public string PhuongThucThanhToan { get; set; }
        public DateTime? NgayDatHang { get; set; }
        public decimal? TongTien { get; set; }
    }
}
