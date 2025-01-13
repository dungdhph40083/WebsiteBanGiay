using Application.Data.Enums;
using Application.Data.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class UserCartController : Controller
    {
        HttpClient Client = new HttpClient();
        //private readonly HttpClient _httpClient;
        //public UserCartController()
        //{
        //    _httpClient = new HttpClient();
        //}
        // Fake data; xóa sau khi có Đăng nhập/Đăng ký!!!
        // USER ID LÀ MỘT USER ĐỊNH SẴN NÊN NÓ CŨNG LÀ FAKE DATA

        public async Task<ActionResult> Index()
        {
            try
            {
                Guid ID = GetCurrentUserId();
                string URL_Voucher = $@"https://localhost:7187/api/Voucher/WhatVoucherAreTheyUsing/{ID}";
                string URL = $@"https://localhost:7187/api/ShoppingCart/User/{ID}";

                var Response = await Client.GetFromJsonAsync<List<ShoppingCart>>(URL);
                var Voucher = JsonConvert.DeserializeObject<Voucher>(await Client.GetAsync(URL_Voucher).Result.Content.ReadAsStringAsync());
                ViewBag.VoucherInfo = Voucher;
                return View(Response);
            }
            catch (UnauthorizedAccessException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new List<ShoppingCart>());
            }
        }

        public async Task<ActionResult> Add2Cart(Guid? ID, int? Quantity, bool? AdditionMode)
        {
            Console.WriteLine("Quantity: " + Quantity ?? "null");

            Guid UserID = GetCurrentUserId();

            if (UserID != null && ID != null)
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
            Guid ID = GetCurrentUserId();

            foreach (var Item in BigCart)
            {
                string URL = $@"https://localhost:7187/api/ShoppingCart/Add2Cart/{ID}/{Item.ProductDetailID}?Quantity={Item.QuantityCart ?? 0}&AdditionMode=false";

                var Response = await Client.PutAsync(URL, null);
            }

            string URL_Voucher = $@"https://localhost:7187/api/Voucher/WhatVoucherAreTheyUsing/{ID}";
            var VoucherChecker = JsonConvert.DeserializeObject<Voucher>(await Client.GetAsync(URL_Voucher).Result.Content.ReadAsStringAsync());
            if (VoucherChecker != null)
            {
                var Response = await Client.PatchAsync($@"https://localhost:7187/api/ShoppingCart/ApplyVoucher/{ID}?VoucherCode={VoucherChecker.VoucherCode}", null);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> ApplyVoucher(string VoucherCode)
        {
            Guid ID = GetCurrentUserId();

            try
            {
                Console.WriteLine($"\nUser ID is {ID}\nVoucher code is {VoucherCode}\n");
                Console.WriteLine($"Voucher code is null or white space: {string.IsNullOrWhiteSpace(VoucherCode)}");

                string URL = $@"https://localhost:7187/api/ShoppingCart/ApplyVoucher/{ID}?VoucherCode={VoucherCode}";
    
                var Response = await Client.PatchAsync(URL, null);

                var Msg = Response.Content.ReadAsStringAsync();
                if (Msg.Result == SuccessResult.VOUCHER_APPLIANCE_SUCCESS)
                {
                    ViewBag.Success = "Sử dụng Voucher thành công!";
                }
                if (Msg.Result == SuccessResult.VOUCHER_DISCARDED_SUCCESS)
                {
                    ViewBag.Success = "Bỏ Voucher thành công!";
                }
            }
            catch (Exception Exc)
            {
                Console.WriteLine($"{Exc.Message} ({Exc.HResult})");
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        private Guid GetCurrentUserId()
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token không tồn tại. Vui lòng đăng nhập lại.");
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
