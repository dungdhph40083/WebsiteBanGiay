using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class MyOrdersController : Controller
    {
        // Fake data; xóa sau khi có Đăng nhập/Đăng ký!!!
        Guid UserID = Guid.Parse("bbd122d1-8961-4363-820e-3ad1a87064e4");

        HttpClient Client = new HttpClient();

        public async Task<ActionResult> Index()
        {
            string URL = $@"https://localhost:7187/api/Orders/User/{UserID}";

            var Response = await Client.GetFromJsonAsync<List<Order>>(URL);

            return View(Response);
        }

        public async Task<ActionResult> Details(Guid ID)
        {
            string URL_OList = $@"https://localhost:7187/api/OrderDetails/Order/{ID}";
            string URL_Order = $@"https://localhost:7187/api/Orders/{ID}";

            var Items = await Client.GetFromJsonAsync<List<OrderDetail>>(URL_OList);
            ViewBag.OrderItems = Items ?? new List<OrderDetail>();

            var Response = await Client.GetFromJsonAsync<Order>(URL_Order);

            return View(Response);
        }

        public async Task<ActionResult> QuickEdit(Guid ID)
        {
            // Gọi API để lấy danh sách người dùng
            var user = await Client.GetFromJsonAsync<User>($@"https://localhost:7187/api/User/{UserID}");

            // Truyền thông tin người dùng vào ViewBag
            ViewBag.DefaultUser = user;

            string URL_Order = $@"https://localhost:7187/api/Orders/{ID}";

            var Response = await Client.GetFromJsonAsync<Order>(URL_Order);

            if (Response == null || Response.HasChangedInfo)
            {
                ViewData["FAILURE"] = "Bạn không thể đổi thông tin đơn nữa do đã đổi thông tin trước đó.";
                return RedirectToAction(nameof(Details), new { ID });
            }
            var TrueResponse = JsonConvert.DeserializeObject<OrderDto>(Response.ToJson());

            return View(TrueResponse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> QuickEdit(Guid ID, OrderDto NewDetail, bool HasExternalInfo)
        {
            var User = await Client.GetFromJsonAsync<User>($@"https://localhost:7187/api/User/{UserID}");
            // fake data 2 - khi tích hợp thì if User exists then proceed, if not then throw into login
            NewDetail.UserID = UserID;

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
            try
            {
                string URL = $@"https://localhost:7187/api/Orders/UpdateStatus/{ID}?StatusCode={(int)OrderStatus.Canceled}";
                var Response = await Client.PatchAsync(URL, null);
                return RedirectToAction(nameof(Details), new { ID });
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
            try
            {
                string URL = $@"https://localhost:7187/api/Orders/UpdateStatus/{ID}?StatusCode={(int)OrderStatus.Received}";
                var Response = await Client.PatchAsync(URL, null);
                return RedirectToAction(nameof(Details), new { ID });
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
            try
            {
                string URL = $@"https://localhost:7187/api/Orders/UpdateStatus/{ID}?StatusCode={(int)OrderStatus.ReceivedAgain}";
                var Response = await Client.PatchAsync(URL, null);
                return RedirectToAction(nameof(Details), new { ID });
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                return View();
            }
        }

        public async Task<ActionResult> RequestRefund(Guid ID)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/Orders/UpdateStatus/{ID}?StatusCode={(int)OrderStatus.Refunding}";
                var Response = await Client.PatchAsync(URL, null);
                return RedirectToAction(nameof(Details), new { ID });
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                return View();
            }
        }

        //public async Task<ActionResult> RequestRefund(Guid ID, ReturnDTO ReturnInfo, string Reason, string? Comments)
        //{
        //    ReturnInfo.OrderID = ID;

        //    switch (Reason)
        //    {
        //        case Reasoning.Generic:
        //            ReturnInfo.Reason = "Hàng không ưng ý";
        //            break;
        //        case Reasoning.UsedProduct:
        //            ReturnInfo.Reason = "Hàng đã sử dụng";
        //            break;
        //        case Reasoning.FakeProduct:
        //            ReturnInfo.Reason = "Hàng giả/hàng nhái";
        //            break;
        //        case Reasoning.BrokenProduct:
        //            ReturnInfo.Reason = "Hàng bị lỗi hoặc gặp hỏng hóc";
        //            break;
        //        case Reasoning.Other:
        //            if (Comments != null)
        //            {
        //                ReturnInfo.Reason = Comments;
        //                break;
        //            }
        //            else
        //            {
        //                ViewData["NoReasoningFound"] = "Đừng quên nhập lý do!";
        //                return View();
        //            }
        //        default:
        //            ViewData["NoReasoningFound"] = "Đã có lỗi xảy ra. Hãy thử lại.";
        //            return View();
        //    }

        //    try
        //    {
        //        string URL_Returns = $@"https://localhost:7187/api/Returns";
        //        var ReturnResponse = await Client.PostAsJsonAsync(URL_Returns, ReturnInfo);

        //        string URL_Orders = $@"https://localhost:7187/api/Orders/UpdateStatus/{ID}?StatusCode={(int)OrderStatus.Refunding}";
        //        var OrderResponse = await Client.PatchAsync(URL_Orders, null);
        //        return RedirectToAction(nameof(Details), new { ID });
        //    }
        //    catch (Exception Msg)
        //    {
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine(Msg.Message);
        //        Console.ForegroundColor = ConsoleColor.Gray;
        //        return View();
        //    }
        //}
    }
}
