using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Application.MVC.Controllers
{
    public class OrderController : Controller
    {
        HttpClient Client = new HttpClient();
        
        public async Task<ActionResult> Index()
        {
            string URL = "https://localhost:7187/api/Orders";

            var Response = await Client.GetFromJsonAsync<List<Order>>(URL);
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
