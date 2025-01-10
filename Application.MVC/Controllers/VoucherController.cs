using Application.Data.DTOs;
using Application.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Net.Http.Headers;

namespace Application.MVC.Controllers
{
    public class VoucherController : Controller
    {
        HttpClient Client = new HttpClient();
        // GET: VoucherController
        private readonly HttpClient _httpClient;
        public VoucherController()
        {
            _httpClient = new HttpClient();
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            string URL = $@"https://localhost:7187/api/Voucher";
            var Response = await Client.GetFromJsonAsync<List<Voucher>>(URL);
            return View(Response);
        }

        // GET: VoucherController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(Guid ID)
        {
            string URL = $@"https://localhost:7187/api/Voucher/{ID}";
            var Response = await Client.GetFromJsonAsync<Voucher>(URL);
            return View(Response);
        }

        // GET: VoucherController/Create
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

            string URL_Catgs = $@"https://localhost:7187/api/Category/";
            string URL_Prods = $@"https://localhost:7187/api/Product/";

            var CatgsList = await Client.GetFromJsonAsync<List<Category>>(URL_Catgs);
            var ProdsList = await Client.GetFromJsonAsync<List<Product>>(URL_Prods);

            var CatgsItems = CatgsList?
                .Select(Cb => new SelectListItem
                { Text = Cb.CategoryName, Value = Cb.CategoryID.ToString() }).ToList();
            var ProdsItems = ProdsList?
                .Select(Cb => new SelectListItem
                { Text = Cb.Name, Value = Cb.ProductID.ToString() }).ToList();

            ViewBag.Catgs = CatgsItems;
            ViewBag.Prods = ProdsItems;
        }

        // POST: VoucherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VoucherDTO Input)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/Voucher";
                var Response = await Client.PostAsJsonAsync(URL, Input);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                TempData["Error"] = $"Đã có lỗi xảy ra! Lỗi:\n{Msg.Message} ({Msg.HResult})";
                return View(Input);
            }
        }

        // GET: VoucherController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(Guid ID)
        {

            await FetchInfoPlsPlsPlsPls();
            string URL = $@"https://localhost:7187/api/Voucher/{ID}";
            var Response = await Client.GetFromJsonAsync<VoucherDTO>(URL);

            return View(Response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: VoucherController/Edit/5
        public async Task<ActionResult> Edit(Guid ID, VoucherDTO NewInput)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/Voucher/{ID}";
                var Response = await Client.PutAsJsonAsync(URL, NewInput);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                TempData["Error"] = $"Đã có lỗi xảy ra! Lỗi:\n{Msg.Message} ({Msg.HResult})";
                return View(NewInput);
            }
        }

        // POST: VoucherController/Delete/5
        public async Task<ActionResult> Delete(Guid ID)
        {

            try
            {
                string URL = $@"https://localhost:7187/api/Voucher/{ID}";
                var Response = await Client.DeleteAsync(URL);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                TempData["Error"] = $"Đã có lỗi xảy ra! Lỗi:\n{Msg.Message} ({Msg.HResult})";
                return View();
            }
        }
    }
}
