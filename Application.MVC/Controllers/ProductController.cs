using Application.Data.DTOs;
using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace Application.MVC.Controllers
{
    public class ProductController : Controller
    {
        HttpClient client;
        public ProductController()
        {
            client = new HttpClient();
        }
        public async Task<ActionResult> Index()
        {
            string requestURL = "https://localhost:7187/api/Product/get-all";
            var response = await client.GetFromJsonAsync<List<Product>>(requestURL);

            if (response == null)
            {
                return View(new List<Product>());
            }

            return View(response);
        }
        public ActionResult Details(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Product/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<Product>(response);
            return View(data);
        }

        public async Task<ActionResult> Create()
        {
            string imageRequestURL = $@"https://localhost:7187/api/Image";
            var images = await client.GetFromJsonAsync<List<Image>>(imageRequestURL);

            // Kiểm tra nếu images null
            if (images == null)
            {
                images = new List<Image>();
            }

            // Tạo SelectList cho dropdown
            ViewBag.Images = new SelectList(images, "ImageID", "ImageFileName");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            if (product.ImageID.HasValue)
            {
                // Lấy thông tin Image từ API
                string imageRequestURL = $"https://localhost:7187/api/Image/{product.ImageID}";
                var image = await client.GetFromJsonAsync<Image>(imageRequestURL);

                // Kiểm tra nếu image tồn tại
                if (image != null)
                {
                    // Gán Image vào Product
                    product.Image = image;
                }
            }


            string requestURL = $"https://localhost:7187/api/Product/create_product";
            var response = await client.PostAsJsonAsync(requestURL, product);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            // Trường hợp tạo thất bại, lấy lại danh sách Image cho dropdown
            string imageListURL = "https://localhost:7187/api/Image";
            var images = await client.GetFromJsonAsync<List<Image>>(imageListURL);
            ViewBag.Images = new SelectList(images, "ImageID", "ImageFileName");

            return View(product);
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            string productRequestURL = $"https://localhost:7187/api/Product/{id}";
            var productResponse = await client.GetStringAsync(productRequestURL);

            if (string.IsNullOrEmpty(productResponse))
            {
                return NotFound();
            }

            var product = JsonConvert.DeserializeObject<Product>(productResponse);

            // Lấy danh sách tất cả Images từ API
            string imagesRequestURL = "https://localhost:7187/api/Image";
            var imagesResponse = await client.GetStringAsync(imagesRequestURL);

            var images = !string.IsNullOrEmpty(imagesResponse)
                ? JsonConvert.DeserializeObject<List<Image>>(imagesResponse)
                : new List<Image>();

            // Đưa dữ liệu hình ảnh vào ViewBag để hiển thị trong combobox
            ViewBag.Images = new SelectList(images, "ImageID", "ImageFileName");

            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid ID, Product product)
        {
            if (product == null)
            {
                return BadRequest("Invalid product data.");
            }

            product.ProductID = ID;

            string requestURL = $"https://localhost:7187/api/Product/update-product/{ID}";

            // Sử dụng PUT để gửi dữ liệu cập nhật sản phẩm
            var response = await client.PutAsJsonAsync(requestURL, product);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); // Chuyển hướng sau khi cập nhật thành công
            }
            else
            {
                // Lấy thông tin lỗi từ API
                var error = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Update failed: {error}");
                return View(product); // Trả về form chỉnh sửa với thông báo lỗi
            }
        }
        public ActionResult Delete(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Product/{id}";
            var response = client.DeleteAsync(requestURL).Result;
            return RedirectToAction("Index");
        }
    }
}
