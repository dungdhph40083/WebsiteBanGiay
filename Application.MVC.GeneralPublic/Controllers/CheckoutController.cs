using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.IdentityModel.Tokens.Jwt;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly HttpClient _client;


        public CheckoutController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            Guid UserID = GetCurrentUserId();
            // Gọi API để lấy danh sách người dùng
            var user = await _client.GetFromJsonAsync<User>($@"https://localhost:7187/api/User/{UserID}");

            // Gọi API để lấy giỏ hàng của người dùng với ID
            var cartItems = await _client.GetFromJsonAsync<List<ShoppingCart>>($"https://localhost:7187/api/ShoppingCart/User/{UserID}");

            // Tính tổng giá trị đơn hàng
            var totalAmount = cartItems?.Sum(item => item.QuantityCart * item.Price) ?? 0;

            string URL_Voucher = $@"https://localhost:7187/api/Voucher/WhatVoucherAreTheyUsing/{UserID}";

            var Voucher = JsonConvert.DeserializeObject<Voucher>(await _client.GetAsync(URL_Voucher).Result.Content.ReadAsStringAsync());

            if (Voucher != null)
            {
                string URL_VoucherValidator = $@"https://localhost:7187/api/Voucher/Validate/{UserID}/{Voucher.VoucherCode}";
                var VoucherResponse = await _client.PostAsync(URL_VoucherValidator, null).Result.Content.ReadAsStringAsync();
                if (VoucherResponse == ValidateErrorResult.VOUCHER_VALID)
                {
                    ViewBag.VoucherInfo = Voucher;
                }
                else
                {
                    switch (VoucherResponse)
                    {
                        default:
                            ViewBag.ErrorMessage = "No";
                            break;
                    }
                }
            }

            // Truyền thông tin người dùng và giỏ hàng vào ViewBag
            ViewBag.DefaultUser = user;
            ViewBag.CartItems = cartItems;
            ViewBag.TotalAmount = totalAmount;

            return View();
        }

        public async Task<ActionResult> InstantCheckout(OrderDto Details, string PaymentMethod, bool HasExternalInfo)
        {
            Guid UserID = GetCurrentUserId();

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
                Details.VoucherID = UserData!.VoucherID;
            }

            string URL_Voucher = $@"https://localhost:7187/api/Voucher/WhatVoucherAreTheyUsing/{UserID}";

            var Voucher = JsonConvert.DeserializeObject<Voucher>(await _client.GetAsync(URL_Voucher).Result.Content.ReadAsStringAsync());

            if (Voucher != null)
            {
                string URL_VoucherValidator = $@"https://localhost:7187/api/Voucher/Validate/{UserID}/{Voucher.VoucherCode}";
                var VoucherResponse = await _client.PostAsync(URL_VoucherValidator, null).Result.Content.ReadAsStringAsync();
                if (VoucherResponse == ValidateErrorResult.VOUCHER_VALID)
                {
                    ViewBag.VoucherInfo = Voucher;
                }
                else
                {
                    switch (VoucherResponse)
                    {
                        default:
                            ViewBag.ErrorMessage = "No";
                            break;
                    }
                }
            }

            if (PaymentMethod == PaymentMethods.CashOnDelivery)
            {
                Details.PaymentMethodID = Guid.Parse(DefaultValues.CoDGUID); // No, it's not "Call of Duty". It's "Cash on Delivery".                                                                                 Did I mention I hate working on this project when being rushed?
                try
                {
                    string URL = $@"https://localhost:7187/api/OrderDetails/Checkout?PaymentMethod={PaymentMethods.CashOnDelivery}";
                    var Response = await _client.PostAsJsonAsync(URL, Details);

                    var Content = JsonConvert.DeserializeObject<List<OrderDetail>>
                        (await Response.Content.ReadAsStringAsync());

                    var RouteID = Content?.First().OrderID;

                    return RedirectToAction(nameof(MyOrdersController.Details), Controller2String.Eat(nameof(MyOrdersController)), new { ID = RouteID });
                }
                catch (Exception Msg)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Msg.Message);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    return RedirectToAction(nameof(Index));
                }
            }
            else if (PaymentMethod == PaymentMethods.VNPay)
            {
                Details.PaymentMethodID = Guid.Parse(DefaultValues.VNPayGUID);
                try
                {
                    string URL = $@"https://localhost:7187/api/Payment/{UserID}";
                    var Response = await _client.PostAsJsonAsync(URL, Details);

                    var Content = Response.Content.ReadAsStringAsync().Result;

                    return Redirect(Content);
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
        private Guid GetCurrentUserId()
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Hãy Đăng Nhập Để Thực Hiện Chức Năng Này.");
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserID");
            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("UserId không tồn tại trong token.");
            }

            return Guid.Parse(userIdClaim.Value);
        }
    }
}
