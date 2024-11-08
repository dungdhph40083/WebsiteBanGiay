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
    public class Size_ProductDetailController : Controller
    {
        HttpClient Client = new HttpClient();
        // GET: Size_ProductDetailController

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            string URL = $@"https://localhost:7187/api/Size_ProductDetail";
            var Response = await Client.GetFromJsonAsync<List<Size_ProductDetail>>(URL);
            return View(Response);
        }

        // GET: Size_ProductDetailController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(Guid ID)
        {
            string URL = $@"https://localhost:7187/api/Size_ProductDetail/{ID}";
            var Response = await Client.GetFromJsonAsync<Size_ProductDetail>(URL);
            return View(Response);
        }

        // GET: Size_ProductDetailController/Create
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

            string URL_Sizes = $@"https://localhost:7187/api/Size/";
            string URL_PrdDs = $@"https://localhost:7187/api/ProductDetails/";

            var SizesList = await Client.GetFromJsonAsync<List<Size>>         (URL_Sizes);
            var ProdsList = await Client.GetFromJsonAsync<List<ProductDetail>>(URL_PrdDs);

            var SizesItems = SizesList?.Where(Wh => Wh.Status != 0)
                .Select(Cb => new SelectListItem
                   { Text = Cb.Name, Value = Cb.SizeID.ToString() }).ToList();
            var PrdDsItems = ProdsList?.Where(Y => Y.Quantity > 0)
                .Select(Cb => new SelectListItem
                   { Text = Cb.Product!.Name, Value = Cb.ProductDetailID.ToString() }).ToList();

            // Todo: Make slider limit to Quantity you know that yeah...

            ViewBag.Sizes = SizesItems;
            ViewBag.PrdDs = PrdDsItems;
        }

        // POST: Size_ProductDetailController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Size_ProductDetailDTO Input)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/Size_ProductDetail";
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

        // GET: Size_ProductDetailController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(Guid ID)
        {
            await FetchInfoPlsPlsPlsPls();
            string URL = $@"https://localhost:7187/api/Size_ProductDetail/{ID}";
            var Response = await Client.GetFromJsonAsync<Size_ProductDetailDTO>(URL);

            return View(Response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: Size_ProductDetailController/Edit/5
        public async Task<ActionResult> Edit(Guid ID, Size_ProductDetailDTO NewInput)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/Size_ProductDetail/{ID}";
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

        // POST: Size_ProductDetailController/Delete/5
        public async Task<ActionResult> Delete(Guid ID)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/Size_ProductDetail/{ID}";
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
