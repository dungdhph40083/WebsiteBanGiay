using Application.Data.Enums;
using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

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
            string URL_Prods = $@"https://localhost:7187/api/ProductDetails";

            var Details = await Client.GetFromJsonAsync<List<ProductDetail>>(URL_Prods);
            Console.WriteLine(JsonConvert.SerializeObject(Details, Formatting.Indented));
            // Lấy 8 con từ đầu danh sách sau khi được sắp xếp theo ngày từ cuối lên đầu

            // Sau đó cho vào ViewBag
            ViewBag.Top8Products = Details?
                .OrderByDescending(Req => Req.Product?.CreatedAt).Take(8).ToList() ?? new List<ProductDetail>();
        }
    }
}
