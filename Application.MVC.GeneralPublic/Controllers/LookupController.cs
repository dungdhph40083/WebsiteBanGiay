using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class LookupController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LookupController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: LookupController
        public IActionResult Index()
        {
            return View();
        }

        // POST: LookupController/OrderLookup
        [HttpPost]
        public async Task<IActionResult> OrderLookup(string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
            {
                ViewBag.Error = "Vui lòng nhập mã đơn hàng.";
                return View("Index");
            }

            var client = _httpClientFactory.CreateClient();
            var apiUrl = $"https://localhost:7187/api/Orders/byOrderNumber/{orderNumber}";

            try
            {
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var orderJson = await response.Content.ReadAsStringAsync();
                    var order = JsonSerializer.Deserialize<Order>(orderJson, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (order != null)
                    {
                        ViewBag.Order = order;
                    }
                    else
                    {
                        ViewBag.Error = "Không tìm thấy thông tin đơn hàng với mã đã nhập.";
                    }
                }
                else
                {
                    ViewBag.Error = "Mã đơn hàng và mã hoá đơn không tồn tại";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Đã xảy ra lỗi: {ex.Message}";
            }

            return View("Index");
        }


    }
}
