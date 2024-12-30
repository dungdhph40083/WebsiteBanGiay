using Application.Data.DTOs;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;

namespace Application.MVC.Controllers
{
    public class ProductWarrantyController : Controller
    {
        HttpClient client = new HttpClient();
        private readonly HttpClient _httpClient;
        public ProductWarrantyController()
        {
            _httpClient = new HttpClient();
        }
        public ActionResult Index()
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string requestURL = "https://localhost:7187/api/ProductWarranty";
            var response = client.GetStringAsync(requestURL).Result;
            var ProductWarrantys = JsonConvert.DeserializeObject<List<ProductWarranty>>(response);
            return View(ProductWarrantys);
        }

        public ActionResult Details(Guid id)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string requestURL = $"https://localhost:7187/api/ProductWarranty/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var ProductWarrantys = JsonConvert.DeserializeObject<ProductWarranty>(response);
            return View(ProductWarrantys);
        }
        public async Task<IActionResult> Create()
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                // Lấy danh sách sản phẩm từ API
                var products = await client.GetFromJsonAsync<List<Product>>("https://localhost:7187/api/Product");

                ViewBag.Products = products ?? new List<Product>();

                // Tạo đối tượng ProductWarranty với giá trị mặc định
                ProductWarranty productWarranty = new ProductWarranty
                {
                    WarrantyID = Guid.NewGuid(),
                    CreatedAt = DateTime.Now, // Gán giá trị mặc định cho CreatedAt
                    UpdatedAt = DateTime.Now  // Gán giá trị mặc định cho UpdatedAt
                };
                return View(productWarranty);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi và hiển thị trang lỗi
                Console.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductWarranty productWarranty)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                // Gán lại giá trị CreatedAt và UpdatedAt để đảm bảo ngày giờ chính xác
                productWarranty.CreatedAt = DateTime.Now;
                productWarranty.UpdatedAt = DateTime.Now;

                // Gửi yêu cầu POST tới API
                string requestURL = "https://localhost:7187/api/ProductWarranty";
                var response = await client.PostAsJsonAsync(requestURL, productWarranty);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    // Ghi log lỗi nếu API trả về lỗi
                    string error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Error: {error}");
                    ViewBag.ErrorMessage = "Không thể tạo bảo hành. Vui lòng thử lại.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                ViewBag.ErrorMessage = "Đã xảy ra lỗi trong quá trình xử lý.";
            }
            var products = await client.GetFromJsonAsync<List<Product>>("https://localhost:7187/api/Product");
            ViewBag.Products = products ?? new List<Product>();

            return View(productWarranty);
        }

        public async Task<IActionResult> Update(Guid id)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                string requestURL = $"https://localhost:7187/api/ProductWarranty/{id}";
                var response = await client.GetStringAsync(requestURL);
                var ProductWarrantys = JsonConvert.DeserializeObject<ProductWarranty>(response);

                // Lấy danh sách sản phẩm từ API
                var products = await client.GetFromJsonAsync<List<Product>>("https://localhost:7187/api/Product");
                ViewBag.Products = products ?? new List<Product>();

                return View(ProductWarrantys);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                // Nếu xảy ra lỗi, vẫn cần gán danh sách sản phẩm để hiển thị
                var products = await client.GetFromJsonAsync<List<Product>>("https://localhost:7187/api/Product");
                ViewBag.Products = products ?? new List<Product>();

                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Guid ID, ProductWarranty ProductWarranty)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                // Giữ nguyên CreatedAt, chỉ cập nhật UpdatedAt
                var existingRequestURL = $"https://localhost:7187/api/ProductWarranty/{ID}";
                var existingResponse = await client.GetStringAsync(existingRequestURL);
                var existingWarranty = JsonConvert.DeserializeObject<ProductWarranty>(existingResponse);

                if (existingWarranty != null)
                {
                    ProductWarranty.CreatedAt = existingWarranty.CreatedAt; // Giữ nguyên CreatedAt
                    ProductWarranty.UpdatedAt = DateTime.Now; // Cập nhật UpdatedAt

                    string requestURL = $"https://localhost:7187/api/ProductWarranty/{ID}";
                    var response = await client.PutAsJsonAsync(requestURL, ProductWarranty);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        string error = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"API Error: {error}");
                        ViewBag.ErrorMessage = "Không thể cập nhật bảo hành. Vui lòng thử lại.";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                ViewBag.ErrorMessage = "Đã xảy ra lỗi trong quá trình xử lý.";
            }

            // Lấy lại danh sách sản phẩm để hiển thị trong View
            var products = await client.GetFromJsonAsync<List<Product>>("https://localhost:7187/api/Product");
            ViewBag.Products = products ?? new List<Product>();

            return View(ProductWarranty);
        }
        public ActionResult Delete(Guid id)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string requestURL = $"https://localhost:7187/api/ProductWarranty/{id}";
            var response = client.DeleteAsync(requestURL).Result;
            return RedirectToAction("Index");
        }


    }

}

