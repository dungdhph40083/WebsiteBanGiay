namespace Application.MVC.Models
{
    public class StatisticsViewModel
    {
        public DateTime? StartDate { get; set; }  // Thêm thuộc tính StartDate
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
        public int? ThanhCong { get; set; }
        public int? ThatBai { get; set; }
        public int? Hoantra { get; set; }
        public decimal? TotalAmountThanhCong { get; set; }
        public decimal? TotalAmountThatBai { get; set; }
        public decimal? TotalAmountHoantra { get; set; }
        public decimal? TotalProducts { get; set; }
        public decimal? TotalCategories { get; set; }
        public decimal? TotalUsers { get; set; }
        public decimal? BannedAccounts { get; set; }
        public decimal? TotalVouchers { get; set; }
        public decimal? TotalContacts { get; set; }
    }

}
