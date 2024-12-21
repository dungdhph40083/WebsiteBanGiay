using Application.Data.DTOs;
using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Application.MVC.Controllers
{
    public class OrderDetailController : Controller
    {
        HttpClient Client = new HttpClient();

        public async Task<ActionResult> Index()
        {
            string URL = "https://localhost:7187/api/OrderDetails";

            var Response = await Client.GetFromJsonAsync<List<OrderDetail>>(URL);
            return View(Response);
        }

        public async Task<ActionResult> Details(Guid ID)
        {
            string URL = $@"https://localhost:7187/api/OrderDetails/{ID}";

            var Response = await Client.GetFromJsonAsync<OrderDetail>(URL);
            return View(Response);
        }

        public async Task FetchInfoPlsPlsPlsPls()
        {
            string URL_Sales = "https://localhost:7187/api/Sales";
            string URL_Vchrs = "https://localhost:7187/api/Vouchers";
            string URL_Ordrs = "https://localhost:7187/api/Orders";
            string URL_Prods = "https://localhost:7187/api/Products";
            string URL_Ships = "https://localhost:7187/api/ShippingMethods";

            var SalesList = await Client.GetFromJsonAsync<List<Sale>>(URL_Sales);
            var VchrsList = await Client.GetFromJsonAsync<List<Voucher>>(URL_Vchrs);
            var OrdrsList = await Client.GetFromJsonAsync<List<Order>>(URL_Ordrs);
            var ProdsList = await Client.GetFromJsonAsync<List<Product>>(URL_Prods);
            var ShipsList = await Client.GetFromJsonAsync<List<ShippingMethod>>(URL_Ships);

            var SalesItems = SalesList?.Where(I => I.Status != 0)
                .Select(Cb => new SelectListItem
                { Text = Cb.Name, Value = Cb.SaleID.ToString() }).ToList();
            var VchrsItems = VchrsList?.Where(I => I.Status != 0)
                .Select(Cb => new SelectListItem
                { Text = Cb.VoucherCode, Value = Cb.VoucherID.ToString() }).ToList();
            var OrdrsItems = OrdrsList?.Where(I => I.Status != 0)
                .Select(Cb => new SelectListItem
                { Text = ("Đơn hàng " + Cb.OrderID.ToString().Take(6)), Value = Cb.OrderID.ToString() }).ToList();
            var ProdsItems = ProdsList?
                .Select(Cb => new SelectListItem
                { Text = Cb.Name, Value = Cb.ProductID.ToString() }).ToList();
            var ShipsItems = ShipsList?
                .Select(Cb => new SelectListItem
                { Text = Cb.MethodName, Value = Cb.ShippingMethodID.ToString() }).ToList();

            ViewBag.Sales = SalesItems;
            ViewBag.Vchrs = VchrsItems;
            ViewBag.Ordrs = OrdrsItems;
            ViewBag.Prods = ProdsItems;
            ViewBag.Ships = ShipsItems;
        }

        // POST: OrderDetailsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrderDetailDto Input)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/OrderDetails";
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

        // GET: OrderDetailsController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(Guid ID)
        {
            await FetchInfoPlsPlsPlsPls();
            string URL = $@"https://localhost:7187/api/OrderDetails/{ID}";
            var Response = await Client.GetFromJsonAsync<OrderDetailDto>(URL);

            return View(Response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: OrderDetailsController/Edit/5
        public async Task<ActionResult> Edit(Guid ID, OrderDetailDto NewInput)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/OrderDetails/{ID}";
                var Response = await Client.PutAsJsonAsync(URL, NewInput);
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

        // POST: OrderDetailsController/Delete/5
        public async Task<ActionResult> Delete(Guid ID)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/OrderDetails/{ID}";
                var Response = await Client.DeleteAsync(URL);
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
