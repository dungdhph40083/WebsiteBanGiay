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

        public async Task<IActionResult> Index(Guid ID)
        {
            // Gọi API để lấy danh sách người dùng
            var users = await _client.GetFromJsonAsync<List<User>>("https://localhost:7187/api/User");

            // Tìm user với UserID đã cố định
            var user = users?.FirstOrDefault(u => u.UserID == UserID);

            // Gọi API để lấy giỏ hàng của người dùng với ID
            var cartItems = await _client.GetFromJsonAsync<List<ShoppingCart>>($"https://localhost:7187/api/ShoppingCart/User/{ID}");

            // Tính tổng giá trị đơn hàng
            var totalAmount = cartItems?.Sum(item => item.QuantityCart * item.Price) ?? 0;

            // Truyền thông tin người dùng và giỏ hàng vào ViewBag
            ViewBag.DefaultUser = user;
            ViewBag.CartItems = cartItems;
            ViewBag.TotalAmount = totalAmount;

            return View();
        }
    }
}
