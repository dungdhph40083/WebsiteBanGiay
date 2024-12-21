using Application.Data.Enums;
using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly HttpClient _client;
        private readonly Guid UserID = Guid.Parse("BBD122D1-8961-4363-820E-3AD1A87064E4");

        public CheckoutController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            // Gọi API để lấy danh sách người dùng
            var user = await _client.GetFromJsonAsync<User>($@"https://localhost:7187/api/User/{UserID}");

            // Gọi API để lấy giỏ hàng của người dùng với ID
            var cartItems = await _client.GetFromJsonAsync<List<ShoppingCart>>($"https://localhost:7187/api/ShoppingCart/User/{UserID}");

            // Tính tổng giá trị đơn hàng
            var totalAmount = cartItems?.Sum(item => item.QuantityCart * item.Price) ?? 0;

            // Truyền thông tin người dùng và giỏ hàng vào ViewBag
            ViewBag.DefaultUser = user;
            ViewBag.CartItems = cartItems;
            ViewBag.TotalAmount = totalAmount;

            return View();
        }

        public ActionResult InstantCheckout(string PaymentMethod)
        {
            if (PaymentMethod == PaymentMethods.CashOnDelivery)
            {

            }

            return RedirectToAction(nameof(Index));
        }
    }
}
