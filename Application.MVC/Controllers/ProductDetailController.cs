using Application.Data.DTOs;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.MVC.Controllers
{
    public class ProductDetailController : Controller
    {
        HttpClient client;
        public ProductDetailController()
        {
            client = new HttpClient();
        }
        public async Task<ActionResult> Index()
        {
            var response = await client.GetAsync("https://localhost:7187/api/ProductDetails");
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
        }

            var productDetails = await response.Content.ReadFromJsonAsync<List<ProductDetail>>();
            return View(productDetails);
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                // Lấy danh sách Products và Images từ API
                var products = await client.GetFromJsonAsync<List<Product>>("https://localhost:7187/api/Product/get-all");
                var images = await client.GetFromJsonAsync<List<Image>>("https://localhost:7187/api/Image");

                // Nếu API trả về null, khởi tạo danh sách trống
                ViewBag.Products = products ?? new List<Product>();
                ViewBag.Images = images ?? new List<Image>();

                return View();
        }
            catch (Exception ex)
            {
                // Log lỗi nếu có
                Console.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }
        }

        // POST: ProductDetail/Create
        [HttpPost]
        public async Task<ActionResult> Create(ProductDetailDTO newProductDetail)
        {
            if (!ModelState.IsValid)
            {
                return View(newProductDetail);
            }

            // Gửi yêu cầu POST đến API
            var response = await client.PostAsJsonAsync("https://localhost:7187/api/ProductDetails", newProductDetail);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); // Quay lại trang danh sách nếu thành công
            }

            // Nếu có lỗi, hiển thị lại form với thông báo lỗi
            ModelState.AddModelError(string.Empty, "An error occurred while creating the product detail.");
            return View(newProductDetail);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            // Lấy danh sách Products và Images từ API
            var products = await client.GetFromJsonAsync<List<Product>>("https://localhost:7187/api/Product/get-all");
            var images = await client.GetFromJsonAsync<List<Image>>("https://localhost:7187/api/Image");

            // Lấy thông tin ProductDetail theo ID
            var productDetail = await client.GetFromJsonAsync<ProductDetailDTO>($"https://localhost:7187/api/ProductDetails/{id}");

            if (productDetail == null)
        {
                return NotFound();
            }

            // Gán dữ liệu vào ViewBag để sử dụng trong dropdown
            ViewBag.Products = products ?? new List<Product>();
            ViewBag.Images = images ?? new List<Image>();

            return View(productDetail);
        }

        // POST: ProductDetail/Edit/{id}
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ProductDetailDTO updatedDetail)
        {
            if (ModelState.IsValid)
            {
                var response = await client.PutAsJsonAsync($"https://localhost:7187/api/ProductDetails/{id}", updatedDetail);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }   
                else
                {
                    ModelState.AddModelError(string.Empty, "Update failed");
                }
            }

            // Lấy lại danh sách để hiển thị trong trường hợp có lỗi
            var products = await client.GetFromJsonAsync<List<Product>>("https://localhost:7187/api/Product/get-all");
            var images = await client.GetFromJsonAsync<List<Image>>("https://localhost:7187/api/Image");

            ViewBag.Products = products ?? new List<Product>();
            ViewBag.Images = images ?? new List<Image>();

            return View(updatedDetail);
        }
        public ActionResult Delete(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/ProductDetails/{id}";
            var response = client.DeleteAsync(requestURL).Result;
            return RedirectToAction("Index");
        }
    }
}
