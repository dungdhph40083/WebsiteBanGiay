﻿using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;

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

        public async Task<ActionResult> InstantCheckout(OrderDto Details, string PaymentMethod, bool HasExternalInfo)
        {
            Details.UserID = UserID;
            Details.HasExternalInfo = true;
            if (!HasExternalInfo)
            {
                string URL = $@"https://localhost:7187/api/User/{UserID}";
                var UserData = await _client.GetFromJsonAsync<User>(URL);

                Details.HasExternalInfo = false;
                Details.FirstName = UserData!.FirstName;
                Details.LastName = UserData!.LastName;
                Details.Email = UserData!.Email;
                Details.PhoneNumber = UserData!.PhoneNumber;
                Details.ShippingAddress = UserData!.Address;
            }

            if (PaymentMethod == PaymentMethods.CashOnDelivery)
            {

                try
                {
                    string URL = $@"https://localhost:7187/api/OrderDetails/Checkout?PaymentMethod={PaymentMethods.CashOnDelivery}";
                    var Response = await _client.PostAsJsonAsync(URL, Details);

                    var Content = JsonConvert.DeserializeObject<List<OrderDetail>>
                        (await Response.Content.ReadAsStringAsync());

                    var RouteID = Content?.First().OrderID;

                    return RedirectToAction(nameof(MyOrdersController.Details), Controller2String.Eat(nameof(MyOrdersController)), new {ID = RouteID});
                }
                catch (Exception Msg)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Msg.Message);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    return RedirectToAction(nameof(Index));
                }
            }
            else return RedirectToAction(nameof(Index));
        }
    }
}
