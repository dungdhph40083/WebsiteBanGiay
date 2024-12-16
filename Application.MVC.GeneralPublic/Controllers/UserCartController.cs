using Application.Data.Enums;
using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class UserCartController : Controller
    {
        HttpClient Client = new HttpClient();

        // Fake data; xóa sau khi có Đăng nhập/Đăng ký!!!
        // USER ID LÀ MỘT USER ĐỊNH SẴN NÊN NÓ CŨNG LÀ FAKE DATA
        Guid UserID = Guid.Parse("bbd122d1-8961-4363-820e-3ad1a87064e4");

        public async Task<ActionResult> Index()
        {
            string URL = $@"HTTPS://LOCALHOST:7187/API/SHOPPINGCART/USER/{UserID}";

            var Response = await Client.GetFromJsonAsync<List<ShoppingCart>>(URL);
            return View(Response);
        }

        public async Task<ActionResult> Add2Cart(Guid? ID, int? Quantity, bool? AdditionMode)
        {
            // TODO:
            // 1. nút cập nhật giỏ hàng (HARD) DONEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE
            // 2. nút xóa (EASY)  DONE
            // 3. căn chỉnh (NORMAL)  DONE
            // 4. thêm cột thông tin (size) (EASY)  DONE
            // 5. làm thông báo thay đổi chưa được lưu (EASY)  DONE

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

        public async Task<ActionResult> UpdateWholeCart(List<ShoppingCart> BigCart)
        {
            foreach (var Item in BigCart)
            {
                string URL =
        $@"https://localhost:7187/api/ShoppingCart/Add2Cart/{UserID}/{Item.ProductDetailID}?Quantity={Item.QuantityCart ?? 0}&AdditionMode=false";

                var Response = await Client.PutAsync(URL, null);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
