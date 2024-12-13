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

        public async Task<ActionResult> Add2Cart(Guid? ID)
        {
            Guid UserID = Guid.Parse("bbd122d1-8961-4363-820e-3ad1a87064e4");

            if (ID != null)
            {
                try
                {
                    string URL = $@"https://localhost:7187/api/ShoppingCart/Add2Cart/{UserID}/{ID}?Quantity=1&AdditionMode=true";
                    var Response = await Client.PutAsync(URL, null);

                    Console.WriteLine(JsonConvert.SerializeObject(Response, Formatting.Indented));

                    return RedirectToAction(nameof(Index), Controller2String.Eat(nameof(UserCartController)));
                }
                catch (Exception Exc)
                {
                    Console.WriteLine($"{Exc.Message} ({Exc.HResult})");
                    throw;
                }
            }
            // TODO: redirect to Login page
            else return RedirectToAction(nameof(Index));
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
