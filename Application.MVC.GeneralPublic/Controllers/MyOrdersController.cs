﻿using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class MyOrdersController : Controller
    {
        // Fake data; xóa sau khi có Đăng nhập/Đăng ký!!!
        HttpClient Client = new HttpClient();
        private readonly HttpClient _httpClient;
        public MyOrdersController()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ActionResult> Index()
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token không tồn tại. Vui lòng đăng nhập lại.");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string URL = $@"https://localhost:7187/api/Orders/User";

            var Response = await Client.GetFromJsonAsync<List<Order>>(URL);

            return View(Response);
        }

        public async Task<ActionResult> Details(Guid ID)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token không tồn tại. Vui lòng đăng nhập lại.");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string URL_OList = $@"https://localhost:7187/api/OrderDetails/Order/{ID}";
            string URL_Order = $@"https://localhost:7187/api/Orders/{ID}";

            var Items = await Client.GetFromJsonAsync<List<OrderDetail>>(URL_OList);
            ViewBag.OrderItems = Items ?? new List<OrderDetail>();

            var Response = await Client.GetFromJsonAsync<Order>(URL_Order);

            return View(Response);
        }

        public async Task<ActionResult> QuickEdit(Guid ID)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token không tồn tại. Vui lòng đăng nhập lại.");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            // Gọi API để lấy danh sách người dùng
            var user = await Client.GetFromJsonAsync<User>($@"https://localhost:7187/api/User");

            // Truyền thông tin người dùng vào ViewBag
            ViewBag.DefaultUser = user;

            string URL_Order = $@"https://localhost:7187/api/Orders/{ID}";

            var Response = await Client.GetFromJsonAsync<OrderDto>(URL_Order);

            return View(Response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> QuickEdit(Guid ID, OrderDto NewDetail, bool HasExternalInfo)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token không tồn tại. Vui lòng đăng nhập lại.");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var User = await Client.GetFromJsonAsync<User>($@"https://localhost:7187/api/User");
            // fake data 2 - khi tích hợp thì if User exists then proceed, if not then throw into login

            string URL_Order = $@"https://localhost:7187/api/Orders/{ID}";

            if (!HasExternalInfo)
            {
                NewDetail.FirstName = User!.FirstName;
                NewDetail.LastName = User!.LastName;
                NewDetail.Email = User!.Email;
                NewDetail.PhoneNumber = User!.PhoneNumber;
                NewDetail.ShippingAddress = User!.Address;
            }
            else
            {
                NewDetail.FirstName ??= User!.FirstName;
                NewDetail.LastName ??= User!.LastName;
                NewDetail.PhoneNumber ??= User!.PhoneNumber;
                NewDetail.ShippingAddress ??= User!.Address;
            }
            var Response = await Client.PatchAsJsonAsync(URL_Order, NewDetail);

            return RedirectToAction(nameof(Details), new {ID});
        }

        public async Task<ActionResult> DestroyOrder(Guid ID)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token không tồn tại. Vui lòng đăng nhập lại.");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
                string URL = $@"https://localhost:7187/api/Orders/UpdateStatus/{ID}?StatusCode={(int)OrderStatus.Canceled}";
                var Response = await Client.PatchAsync(URL, null);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                return View();
            }
        }

        public async Task<ActionResult> ReceivedOrder(Guid ID)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token không tồn tại. Vui lòng đăng nhập lại.");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
                string URL = $@"https://localhost:7187/api/Orders/UpdateStatus/{ID}?StatusCode={(int)OrderStatus.Received}";
                var Response = await Client.PatchAsync(URL, null);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                return View();
            }
        }

        public async Task<ActionResult> DestroyRequest2Refund(Guid ID)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token không tồn tại. Vui lòng đăng nhập lại.");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
                string URL = $@"https://localhost:7187/api/Orders/UpdateStatus/{ID}?StatusCode={(int)OrderStatus.ReceivedAgain}";
                var Response = await Client.PatchAsync(URL, null);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                return View();
            }
        }

        public async Task<ActionResult> Request2Refund(Guid ID)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token không tồn tại. Vui lòng đăng nhập lại.");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
                string URL = $@"https://localhost:7187/api/Orders/UpdateStatus/{ID}?StatusCode={(int)OrderStatus.Refunding}";
                var Response = await Client.PatchAsync(URL, null);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                return View();
            }
        }

        public async Task<ActionResult> Request2RefundAgain(Guid ID)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token không tồn tại. Vui lòng đăng nhập lại.");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
                string URL = $@"https://localhost:7187/api/Orders/UpdateStatus/{ID}?StatusCode={(int)OrderStatus.RefundingAgain}";
                var Response = await Client.PatchAsync(URL, null);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                return View();
            }
        }
    }
}
