using Application.Data.DTOs;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Application.MVC.Controllers
{
    public class ProductDetailController : Controller
    {
        HttpClient Client = new HttpClient();

        public async Task<ActionResult> Index()
        {
            string URL = $@"https://localhost:7187/api/ProductDetails";
            var Response = await Client.GetFromJsonAsync<List<ProductDetail>>(URL);
            return View(Response);
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                await Afvhklsjdfklsjlkjdfklsdjklfjiwrjpofds();
                return View();
            }
            catch (Exception ex)
            {
                // Log lỗi nếu có
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.Gray;
                return View("Error");
            }
        }

        private async Task Afvhklsjdfklsjlkjdfklsdjklfjiwrjpofds()
        {
            // Lấy danh sách Products và Images từ API
            var ProductsList = await Client.GetFromJsonAsync<List<Product>>("https://localhost:7187/api/Products");
            var ColorsList = await Client.GetFromJsonAsync<List<Color>>("https://localhost:7187/api/Color");
            var SizesList = await Client.GetFromJsonAsync<List<Size>>("https://localhost:7187/api/Size");
            var CategoriesList = await Client.GetFromJsonAsync<List<Category>>($@"https://localhost:7187/api/Category");
            var SalesList = await Client.GetFromJsonAsync<List<Sale>>($@"https://localhost:7187/api/Category");

            var ProdsItems = ProductsList?
                .Select(Pls => new SelectListItem
                { Text = Pls.Name, Value = Pls.ProductID.ToString() }).ToList();

            var ColrsItems = ColorsList?
                .Select(Help => new SelectListItem
                { Text = Help.ColorName, Value = Help.ColorID.ToString() }).ToList();

            var SizesItems = SizesList?
                .Select(Im => new SelectListItem
                { Text = Im.Name, Value = Im.SizeID.ToString() }).ToList();

            var CatgsItems = CategoriesList?
                .Select(Being => new SelectListItem
                { Text = Being.CategoryName, Value = Being.CategoryID.ToString() }).ToList();

            var SalesItems = SalesList?
                .Select(HeldHostage => new SelectListItem
                { Text = HeldHostage.Name, Value = HeldHostage.SaleID.ToString() }).ToList();

            // Nếu API trả về null, khởi tạo danh sách trống
            ViewBag.Products = ProdsItems;
            ViewBag.Colors = ColrsItems;
            ViewBag.Sizes = SizesItems;
            ViewBag.Categories = CatgsItems;
            ViewBag.Sales = SalesItems;
        }

        // POST: ProductDetail/Create
        [HttpPost]
        public async Task<ActionResult> Create(ProductDetailDTO Detail, IFormFile? Image)
        {
            MultipartFormDataContent Contents = new()
            {
                { new StringContent(Detail.ProductID.ToString() ?? ""),  nameof(Detail.ProductID) },
                { new StringContent(Detail.SizeID.ToString() ?? ""),     nameof(Detail.SizeID) },
                { new StringContent(Detail.ColorID.ToString() ?? ""),    nameof(Detail.ColorID) },
                { new StringContent(Detail.CategoryID.ToString() ?? ""), nameof(Detail.CategoryID) },
                { new StringContent(Detail.SaleID.ToString() ?? ""),     nameof(Detail.SaleID) },
                { new StringContent(Detail.Material ?? ""),              nameof(Detail.Material) },
                { new StringContent(Detail.Quantity.ToString() ?? ""),   nameof(Detail.Quantity) },
                { new StringContent(Detail.Brand ?? ""),                 nameof(Detail.Brand) },
                { new StringContent(Detail.PlaceOfOrigin ?? ""),         nameof(Detail.PlaceOfOrigin) },
                { new StringContent(Detail.Status.ToString() ?? "1"),    nameof(Detail.Status) }
            };

            // Gửi yêu cầu POST đến API
            try
            {
                var response = await Client.PostAsync("https://localhost:7187/api/ProductDetails", Contents);
                return RedirectToAction(nameof(Index)); // Quay lại trang danh sách nếu thành công
            }
            catch (Exception Msg)
            {
                await Afvhklsjdfklsjlkjdfklsdjklfjiwrjpofds();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {Msg.Message}");
                Console.ForegroundColor = ConsoleColor.Gray;
                return View(Detail);
            }

            // Nếu có lỗi, hiển thị lại form với thông báo lỗi
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            await Afvhklsjdfklsjlkjdfklsdjklfjiwrjpofds();

            // Lấy thông tin ProductDetail theo ID
            var productDetail = await Client.GetFromJsonAsync<ProductDetailDTO>($"https://localhost:7187/api/ProductDetails/{id}");
            return View(productDetail);
        }

        // POST: ProductDetail/Edit/{id}
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ProductDetailDTO updatedDetail)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/ProductDetails/{id}";
                var Response = await Client.PutAsJsonAsync(URL, updatedDetail);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                TempData["Error"] = $"Đã có lỗi xảy ra! Lỗi:\n{Msg.Message} ({Msg.HResult})";
                return View(updatedDetail);
            }
        }
        public ActionResult Delete(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/ProductDetails/{id}";
            var response = Client.DeleteAsync(requestURL).Result;
            return RedirectToAction(nameof(Index));
        }
    }
}
