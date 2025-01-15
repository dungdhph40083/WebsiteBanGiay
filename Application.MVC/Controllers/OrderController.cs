using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net;

namespace Application.MVC.Controllers
{
    public class OrderController : Controller
    {
        HttpClient Client = new HttpClient();
        private readonly INotyfService ToastNotifier;
        public OrderController(INotyfService ToastNotifier)
        {
            this.ToastNotifier = ToastNotifier;
        }
        [ResponseCache(NoStore = true, Duration = 0)]
        public async Task<ActionResult> Index(string? Filter)
        {
            string URL;
            string URL_Badge = "https://localhost:7187/api/Orders/Count";
            if (Filter == null) URL = "https://localhost:7187/api/Orders";
            else URL = $@"https://localhost:7187/api/Orders?Filter={Filter}";

            var Response = await Client.GetFromJsonAsync<List<Order>>(URL);
            var BadgeData = await Client.GetFromJsonAsync<CategorizedOrdersCountModel>(URL_Badge);

            ViewData["FilterValue"] = Filter;
            ViewBag.BadgeData = BadgeData ?? new();
            //

            return View(Response);
        }

        public async Task<ActionResult> Details(Guid ID)
        {
            string URL_Order = $@"https://localhost:7187/api/Orders/{ID}";
            string URL_OList = $@"https://localhost:7187/api/OrderDetails/Order/{ID}";

            var Infos = await Client.GetFromJsonAsync<Order>(URL_Order);
            var Items = await Client.GetFromJsonAsync<List<OrderDetail>>(URL_OList);

            ViewBag.AlwaysTired = Infos ?? new Order();
            ViewBag.Grrrrrrrrrr = Items ?? new List<OrderDetail>();

            return View();
        }

        public async Task<ActionResult> Invoice(Guid ID)
        {
            string URL_Order = $@"https://localhost:7187/api/Orders/{ID}";
            string URL_OList = $@"https://localhost:7187/api/OrderDetails/Order/{ID}";

            var Infos = await Client.GetFromJsonAsync<Order>(URL_Order);
            var Items = await Client.GetFromJsonAsync<List<OrderDetail>>(URL_OList);

            ViewBag.AlwaysTired = Infos ?? new Order();
            ViewBag.Grrrrrrrrrr = Items ?? new List<OrderDetail>();

            return View();
        }

        public async Task FetchInfoPlsPlsPlsPls()
        {
            string URL_Users = "https://localhost:7187/api/Users";
            string URL_PmMts = "https://localhost:7187/api/PaymentMethods";

            var UsersList = await Client.GetFromJsonAsync<List<User>>(URL_Users);
            var PmMtsList = await Client.GetFromJsonAsync<List<PaymentMethod>>(URL_PmMts);

            var UsersItems = UsersList?.Where(I => I.Status != 0)
                .Select(Cb => new SelectListItem
                { Text = Cb.Username, Value = Cb.UserID.ToString() }).ToList();
            var PmMtsItems = PmMtsList?.Where(I => I.Status != 0)
                .Select(Cb => new SelectListItem
                { Text = Cb.MethodName, Value = Cb.PaymentMethodID.ToString() }).ToList();

            ViewBag.Users = UsersItems;
            ViewBag.PmMts = PmMtsItems;
        }

        // POST: OrdersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrderDto Input)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/Orders";
                var Response = await Client.PostAsJsonAsync(URL, Input);

                if (Response.IsSuccessStatusCode)
                {
                    ToastNotifier.Success("Tạo mới đơn hàng thành công!");
                }
                else
                {
                    switch (Response.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:
                        default:
                            ToastNotifier.Error("Tạo mới đơn hàng thất bại.");
                            break;
                        case HttpStatusCode.InternalServerError:
                            ToastNotifier.Error("Tạo mới đơn hàng thất bại: đã có lỗi máy chủ xảy ra.");
                            break;
                        case HttpStatusCode.Unauthorized:
                        case HttpStatusCode.Forbidden:
                            ToastNotifier.Error("Bạn không có đủ quyền hạn cần thiết.");
                            break;
                        case HttpStatusCode.NotFound:
                            ToastNotifier.Warning("Không tìm thấy gì.");
                            break;
                    }
                }

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
        public async Task<ActionResult> UpdateOrderStatus(Guid ID, byte StatusCode)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/Orders/UpdateStatus/{ID}?StatusCode={StatusCode}";
                var Response = await Client.PatchAsync(URL, null);

                if (Response.IsSuccessStatusCode /*&& Response.Content != null*/)
                {
                    // Screw this.
                    ToastNotifier.Success("Cập nhật trạng thái đơn thành công!");

                    //var Response2JSON = JsonConvert.DeserializeObject<Order>(await Response.Content.ReadAsStringAsync());

                    //switch ((OrderStatus)Response2JSON!.Status)
                    //{
                    //    case OrderStatus.RefundProcessed:
                    //        ToastNotifier.Warning("Đã chấp nhận hoàn trả đơn!");
                    //        break;
                    //    case OrderStatus.RefundDelivered:
                    //        ToastNotifier.Information("Đã bắt đầu tiến hành hoàn trả đơn.");
                    //        break;
                    //    case OrderStatus.RefundReceived:
                    //        ToastNotifier.Information("Đã xác nhận lấy hàng trả.");
                    //        break;
                    //    case OrderStatus.Refunded:
                    //        ToastNotifier.Warning("Đã xác nhận hoàn trả thành công!");
                    //        break;
                    //    case OrderStatus.DeliveryFailure:
                    //        {
                    //            if (Response2JSON?.AttemptsLeft != 0)
                    //            {
                    //                ToastNotifier.Warning("Đã xác nhận giao hàng không thành công.");
                    //            }
                    //            else
                    //            {
                    //                ToastNotifier.Warning("Đã xác nhận giao hàng thất bại.");
                    //            }
                    //        }
                    //        break;
                    //    case OrderStatus.DeliveryIsDead:
                    //        ToastNotifier.Warning("Đã xác nhận giao hàng thất bại.");
                    //        break;
                    //    case OrderStatus.Processed:
                    //        ToastNotifier.Success("Xác nhận đơn thành công!");
                    //        break;
                    //    case OrderStatus.Delivered:
                    //        ToastNotifier.Information("Đã xác nhận giao đơn.");
                    //        break;
                    //    case OrderStatus.Arrived:
                    //        ToastNotifier.Information("Đã xác nhận giao hàng thành công!");
                    //        break;
                    //    case OrderStatus.Received:
                    //    case OrderStatus.ReceivedAgain:
                    //        ToastNotifier.Success("Đã xác nhận khách đã nhận hàng thành công!");
                    //        break;
                    //    case OrderStatus.ReceivedCompleted:
                    //        ToastNotifier.Information("Đã từ chối hoàn trả thành công.");
                    //        break;
                    //    case OrderStatus.ReceivedRefundFail:
                    //        ToastNotifier.Warning("Đã xác nhận hoàn trả thất bại.");
                    //        break;
                    //    default:
                    //        break;
                    //}
                }
                else
                {
                    switch (Response.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:
                        default:
                            ToastNotifier.Error("Cập nhật trạng thái đơn thất bại.");
                            break;
                        case HttpStatusCode.InternalServerError:
                            ToastNotifier.Error("Cập nhật trạng thái đơn thất bại: đã có lỗi máy chủ xảy ra.");
                            break;
                        case HttpStatusCode.Unauthorized:
                        case HttpStatusCode.Forbidden:
                            ToastNotifier.Error("Bạn không có đủ quyền hạn cần thiết.");
                            break;
                        case HttpStatusCode.NotFound:
                            ToastNotifier.Warning("Không tìm thấy gì.");
                            break;
                    }
                }

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

        //public async Task<ActionResult> UpdateOrderHasPaid(Guid ID, bool Toggle)
        //{
        //    try
        //    {
        //        string URL = $@"https://localhost:7187/api/Orders/UpdateStatusPaid/{ID}?Toggle={Toggle}";
        //        var Response = await Client.PatchAsync(URL, null);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception Msg)
        //    {
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine(Msg.Message);
        //        Console.ForegroundColor = ConsoleColor.Gray;
        //        return View();
        //    }
        //}

        //// GET: OrdersController/Edit/5
        //[HttpGet]
        //public async Task<ActionResult> Edit(Guid ID)
        //{
        //    await FetchInfoPlsPlsPlsPls();
        //    string URL = $@"https://localhost:7187/api/Orders/{ID}";
        //    var Response = await Client.GetFromJsonAsync<OrderDto>(URL);

        //    return View(Response);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //// POST: OrdersController/Edit/5
        //public async Task<ActionResult> Edit(Guid ID, OrderDto NewInput)
        //{
        //    try
        //    {
        //        string URL = $@"https://localhost:7187/api/Orders/{ID}";
        //        var Response = await Client.PutAsJsonAsync(URL, NewInput);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception Msg)
        //    {
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine(Msg.Message);
        //        Console.ForegroundColor = ConsoleColor.Gray;
        //        return View();
        //    }
        //}

        //// POST: OrdersController/Delete/5
        //public async Task<ActionResult> Delete(Guid ID)
        //{
        //    try
        //    {
        //        string URL = $@"https://localhost:7187/api/Orders/{ID}";
        //        var Response = await Client.DeleteAsync(URL);
        //        return RedirectToAction(nameof(Index));
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
