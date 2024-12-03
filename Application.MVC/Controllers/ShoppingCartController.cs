using Application.Data.DTOs;
using Application.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace Application.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        HttpClient Client = new HttpClient();
        // GET: ShoppingCartController

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            string URL = $@"https://localhost:7187/api/ShoppingCart";
            var Response = await Client.GetFromJsonAsync<List<ShoppingCart>>(URL);
            return View(Response);
        }

        // GET: ShoppingCartController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(Guid ID)
        {
            string URL = $@"https://localhost:7187/api/ShoppingCart/{ID}";
            var Response = await Client.GetFromJsonAsync<ShoppingCart>(URL);
            return View(Response);
        }

        // GET: ShoppingCartController/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await FetchInfoPlsPlsPlsPls();
            return View();
        }

        private async Task FetchInfoPlsPlsPlsPls()
        {
            // A pattern could have improved this but whatever
            // this project is already starting to get confusing

            string URL_Users = $@"https://localhost:7187/api/User/";
            string URL_Color = $@"https://localhost:7187/api/Color/";
            string URL_Sizes = $@"https://localhost:7187/api/Size/";
            string URL_Vouch = $@"https://localhost:7187/api/Voucher/";
            string URL_Prods = $@"https://localhost:7187/api/Product/";

            var UsersList = await Client.GetFromJsonAsync<List<User>>   (URL_Users);
            var ColorList = await Client.GetFromJsonAsync<List<Color>>  (URL_Color);
            var SizesList = await Client.GetFromJsonAsync<List<Size>>   (URL_Sizes);
            var VouchList = await Client.GetFromJsonAsync<List<Voucher>>(URL_Vouch);
            var ProdsList = await Client.GetFromJsonAsync<List<Product>>(URL_Prods);

            var UsersItems = UsersList?.Where(I => I.Status != 0)
                .Select(Cb => new SelectListItem
                   { Text = Cb.Username, Value = Cb.UserID.ToString() }).ToList();
            var ColorItems = ColorList?.Where(Dont => Dont.Status != 0)
                .Select(Cb => new SelectListItem
                   { Text = Cb.ColorName, Value = Cb.ColorID.ToString() }).ToList();
            var SizesItems = SizesList?.Where(Feel => Feel.Status != 0)
                .Select(Cb => new SelectListItem
                   { Text = Cb.Name, Value = Cb.SizeID.ToString() }).ToList();
            var VouchItems = VouchList?.Where(SoGood => SoGood.Status != 0)
                .Select(Cb => new SelectListItem
                   { Text = Cb.VoucherCode, Value = Cb.VoucherID.ToString() }).ToList();
            var ProdsItems = ProdsList?
                .Select(Cb => new SelectListItem
                   { Text = Cb.Name, Value = Cb.ProductID.ToString() }).ToList();

            ViewBag.Users = UsersItems;
            ViewBag.Color = ColorItems;
            ViewBag.Sizes = SizesItems;
            ViewBag.Vouch = VouchItems;
            ViewBag.Prods = ProdsItems;
        }

        // POST: ShoppingCartController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ShoppingCartDTO Input)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/ShoppingCart";
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

        // GET: ShoppingCartController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(Guid ID)
        {
            await FetchInfoPlsPlsPlsPls();
            string URL = $@"https://localhost:7187/api/ShoppingCart/{ID}";
            var Response = await Client.GetFromJsonAsync<ShoppingCartDTO>(URL);

            return View(Response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: ShoppingCartController/Edit/5
        public async Task<ActionResult> Edit(Guid ID, ShoppingCartDTO NewInput)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/ShoppingCart/{ID}";
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

        // POST: ShoppingCartController/Delete/5
        public async Task<ActionResult> Delete(Guid ID)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/ShoppingCart/{ID}";
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
