using Application.Data.Models;
using Application.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Application.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        // Hành động Index để lấy dữ liệu thống kê
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            // Nếu không có startDate và endDate, mặc định là ngày hôm nay
            startDate ??= DateTime.Now.Date;  // Đặt giờ là 00:00:00
            endDate ??= DateTime.Now.Date.AddDays(1).AddSeconds(-1);  // Đặt giờ là 23:59:59



            // Tạo URL cho API với startDate và endDate
            var uri = $"https://localhost:7187/api/Statistics/status?startDate={startDate.Value:yyyy-MM-ddTHH:mm:ss}&endDate={endDate.Value:yyyy-MM-ddTHH:mm:ss}";

            // Gửi yêu cầu tới API
            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

                // Chuyển dữ liệu JSON vào ViewModel
                var viewModel = new StatisticsViewModel
                {
                    StartDate = startDate.Value,
                    EndDate = endDate.Value,
                    ThanhCong = data.thanhCong ,
                    ThatBai = data.thatBai ,
                    Hoantra = data.hoantra ,
                    TotalAmountThanhCong = data.totalAmountThanhCong ?? 0m,
                    TotalAmountThatBai = data.totalAmountThatBai ??0m,
                    TotalAmountHoantra = data.totalAmountHoantra ??0m,
                    TotalCategories = data.totalCategories ?? 0m,
                    TotalProducts = data.totalProducts ?? 0m,
                    TotalUsers = data.totalUsers ?? 0m,
                    TotalVouchers = data.totalVouchers ?? 0m,
                    BannedAccounts= data.bannedAccounts??0m,
                    TotalContacts= data.totalContacts ??0m
                };

                return View(viewModel);
            }

            return View(new StatisticsViewModel());
        }



        public IActionResult Dashboard()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
