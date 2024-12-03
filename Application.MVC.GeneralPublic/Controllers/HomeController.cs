using Application.Data.Models;
using Application.MVC.GeneralPublic.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class HomeController : Controller
    {
        HttpClient Client = new HttpClient();

        public async Task<ActionResult> Index()
        {
            await FetchInfo();
            return View();
        }

        private async Task FetchInfo()
        {
            string URL_Prods = $@"https://localhost:7187/api/Product/get-all";

            var Products = await Client.GetFromJsonAsync<List<Product>>(URL_Prods);
            // Lấy 5 con từ đầu danh sách sau khi được sắp xếp theo ngày từ cuối lên đầu

            // Sau đó cho vào ViewBag
            ViewBag.Top5Products = Products?.OrderByDescending(Req => Req.CreatedAt).Take(5) ?? new List<Product>();
        }

        public IActionResult Privacy()
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
