using Application.Data.Enums;
using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;
using System.Net.Http.Headers;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class UserCartController : Controller
    {
        HttpClient Client = new HttpClient();
        private readonly HttpClient _httpClient;
        public UserCartController()
        {
            _httpClient = new HttpClient();
        }
        // Fake data; xóa sau khi có Đăng nhập/Đăng ký!!!
        // USER ID LÀ MỘT USER ĐỊNH SẴN NÊN NÓ CŨNG LÀ FAKE DATA

        public async Task<ActionResult> Index()
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token không tồn tại. Vui lòng đăng nhập lại.");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string URL = $@"HTTPS://LOCALHOST:7187/API/SHOPPINGCART";

            var Response = await _httpClient.GetFromJsonAsync<List<ShoppingCart>>(URL);
            return View(Response);
        }

        public async Task<ActionResult> Add2Cart(Guid? ID, int? Quantity, bool? AdditionMode)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token không tồn tại. Vui lòng đăng nhập lại.");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            if (ID != null)
            {
                try
                {
                    string URL = $@"https://localhost:7187/api/ShoppingCart/Add2Cart/{ID}?Quantity={Quantity ?? 0}&AdditionMode={AdditionMode}";
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
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token không tồn tại. Vui lòng đăng nhập lại.");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            foreach (var Item in BigCart)
            {
                string URL =
        $@"https://localhost:7187/api/ShoppingCart/Add2Cart/{Item.ProductDetailID}?Quantity={Item.QuantityCart ?? 0}&AdditionMode=false";

                var Response = await Client.PutAsync(URL, null);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> ApplyVoucher(Guid ID, string VoucherCode)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token không tồn tại. Vui lòng đăng nhập lại.");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
                string URL = $@"https://localhost:7187/api/ShoppingCart/ApplyVoucher/{ID}?VoucherCode={VoucherCode}";
                var Response = await Client.PatchAsync(URL, null);
            }
            catch (Exception Exc)
            {
                Console.WriteLine($"{Exc.Message} ({Exc.HResult})");
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> UnapplyVoucher(Guid ID)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token không tồn tại. Vui lòng đăng nhập lại.");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
                string URL = $@"https://localhost:7187/api/ShoppingCart/UnapplyVoucher/{ID}";
                var Response = await Client.PatchAsync(URL, null);
            }
            catch (Exception Exc)
            {
                Console.WriteLine($"{Exc.Message} ({Exc.HResult})");
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
