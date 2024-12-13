using Application.Data.Enums;
using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class UserCartController : Controller
    {
        HttpClient Client = new HttpClient();

        public async Task<ActionResult> Index()
        {
            string URL = $@"HTTPS://LOCALHOST:7187/API/SHOPPINGCART";

            var Response = await Client.GetFromJsonAsync<List<ShoppingCart>>(URL);
            return View(Response);
        }

        public async Task<ActionResult> Add2Cart(Guid? ID, int? Quantity, bool? AdditionMode)
        {
            // Fake data; xóa sau khi có Đăng nhập/Đăng ký!!!
            Guid UserID = Guid.Parse("bbd122d1-8961-4363-820e-3ad1a87064e4");

            if (ID != null)
            {
                try
                {
                    string URL = $@"https://localhost:7187/api/ShoppingCart/Add2Cart/{UserID}/{ID}?Quantity={Quantity ?? 0}&AdditionMode={AdditionMode}";
                    var Response = await Client.PutAsync(URL, null);

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

    }
}
