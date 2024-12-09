//using Application.Data.DTOs;
//using Application.Data.Models;
//using Application.Data.Repositories.IRepository;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;

//namespace Application.MVC.Controllers
//{
//    public class ProductDetailController : Controller
//    {
//        HttpClient client;
//        public ProductDetailController()
//        {
//            client = new HttpClient();
//        }
//        public async Task<ActionResult> Index()
//        {
//            var response = await client.GetAsync("https://localhost:7187/api/ProductDetails");
//            if (!response.IsSuccessStatusCode)
//            {
//                return View("Error");
//        }

//            var productDetails = await response.Content.ReadFromJsonAsync<List<ProductDetail>>();
//            return View(productDetails);
//        }

//        public async Task<IActionResult> Create()
//        {
//            try
//            {
//                await FetchInfo();
//                return View();
//            }
//            catch (Exception ex)
//            {
//                // Log lỗi nếu có
//                Console.ForegroundColor = ConsoleColor.Red;
//                Console.WriteLine($"Error: {ex.Message}");
//                Console.ForegroundColor = ConsoleColor.Gray;
//                return View("Error");
//            }
//        }

//        private async Task FetchInfo()
//        {
//            // Lấy danh sách Products và Images từ API
//            var products = await client.GetFromJsonAsync<List<Product>>("https://localhost:7187/api/Product/get-all");
//            var Colors = await client.GetFromJsonAsync<List<Color>>("https://localhost:7187/api/Color/get-all");
//            var Sizes = await client.GetFromJsonAsync<List<Size>>("https://localhost:7187/api/Size");

//            // Nếu API trả về null, khởi tạo danh sách trống
//            ViewBag.Products = products ?? new List<Product>();
//            ViewBag.Colors = Colors ?? new List<Color>();
//            ViewBag.Sizes = Sizes ?? new List<Size>();
//        }

//        // POST: ProductDetail/Create
//        [HttpPost]
//        public async Task<ActionResult> Create(ProductDetailDTO newProductDetail)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(newProductDetail);
//            }

//            // Gửi yêu cầu POST đến API
//            try
//            {
//                var response = await client.PostAsJsonAsync("https://localhost:7187/api/ProductDetails", newProductDetail);
//                return RedirectToAction("Index"); // Quay lại trang danh sách nếu thành công
//            }
//            catch (Exception Msg)
//            {
//                await FetchInfo();
//                Console.ForegroundColor = ConsoleColor.Red;
//                Console.WriteLine($"Error: {Msg.Message}");
//                Console.ForegroundColor = ConsoleColor.Gray;
//                return View(newProductDetail);
//            }

//            // Nếu có lỗi, hiển thị lại form với thông báo lỗi
//        }

//        public async Task<IActionResult> Edit(Guid id)
//        {
//            await FetchInfo();

//            // Lấy thông tin ProductDetail theo ID
//            var productDetail = await client.GetFromJsonAsync<ProductDetailDTO>($"https://localhost:7187/api/ProductDetails/getbyId?ID={id}");

//            if (productDetail == null)
//        {
//                return NotFound();
//            }

//            return View(productDetail);
//        }

//        // POST: ProductDetail/Edit/{id}
//        [HttpPost]
//        public async Task<IActionResult> Edit(Guid id, ProductDetailDTO updatedDetail)
//        {
//            if (ModelState.IsValid)
//            {
//                var response = await client.PutAsJsonAsync($"https://localhost:7187/api/ProductDetails/{id}", updatedDetail);

//                if (response.IsSuccessStatusCode)
//                {
//                    return RedirectToAction("Index");
//                }   
//                else
//                {
//                    ModelState.AddModelError(string.Empty, "Update failed");
//                }
//            }

//            await FetchInfo();

//            return View(updatedDetail);
//        }
//        public ActionResult Delete(Guid id)
//        {
//            string requestURL = $"https://localhost:7187/api/ProductDetails/{id}";
//            var response = client.DeleteAsync(requestURL).Result;
//            return RedirectToAction("Index");
//        }
//    }
//}
