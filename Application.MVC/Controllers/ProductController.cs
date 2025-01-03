﻿using Application.Data.DTOs;
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
        public async Task<ActionResult> Index(int page = 1, int pageSize = 15)
        {
            string requestURL = "https://localhost:7187/api/Product";
            var response = await client.GetFromJsonAsync<List<Product>>(requestURL);

            if (response == null)
            {
                return View(new List<Product>());
            }

            // Sắp xếp sản phẩm theo ngày tạo (mới nhất trước)
            var sortedResponse = response.OrderByDescending(p => p.CreatedAt).ToList();

            // Tổng số sản phẩm
            int totalItems = sortedResponse.Count;

            // Tính toán phân trang
            var pagedData = sortedResponse
                .Skip((page - 1) * pageSize) // Bỏ qua sản phẩm của các trang trước
                .Take(pageSize) // Lấy số sản phẩm của trang hiện tại
                .ToList();

            // Truyền thông tin phân trang sang View qua ViewBag
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return View(pagedData);
        }


        public ActionResult Details(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Product/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<Product>(response);
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductDTO product, IFormFile? Image)
        {
            string requestURL = $"https://localhost:7187/api/Product";

            MultipartFormDataContent Contents = new()
            {
                { new StringContent(product.Name!),                                nameof(product.Name) },
                { new StringContent(product.Description!),                         nameof(product.Description) },
                { new StringContent(product.Price.GetValueOrDefault().ToString()), nameof(product.Price) }
            };

            if (Image != null)
            {
                var ImageStream = new StreamContent(Image.OpenReadStream());
                Contents.Add(ImageStream, nameof(Image), Image.FileName);
            }

            var response = await client.PostAsync(requestURL, Contents);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Create failed: {error}");
                return View(product); // Trả về form chỉnh sửa với thông báo lỗi
            }
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            string productRequestURL = $"https://localhost:7187/api/Product/{id}";
            var productResponse = await client.GetStringAsync(productRequestURL);

            if (string.IsNullOrEmpty(productResponse))
            {
                return NotFound();
            }

            var product = JsonConvert.DeserializeObject<ProductDTO>(productResponse);

            // Đưa dữ liệu hình ảnh vào ViewBag để hiển thị trong combobox

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid ID, ProductDTO product, IFormFile? Image)
        {
            if (product == null)
            {
                return BadRequest("Invalid product data.");
            }

            string requestURL = $"https://localhost:7187/api/Product/{ID}";

            MultipartFormDataContent Contents = new()
            {
                { new StringContent(product.Name!),        nameof(product.Name) },
                { new StringContent(product.Description!), nameof(product.Description) },
                { new StringContent(product.Price.GetValueOrDefault().ToString()), nameof(product.Price) }
            };

            if (Image != null)
            {
                var ImageStream = new StreamContent(Image.OpenReadStream());
                Contents.Add(ImageStream, nameof(Image), Image.FileName);
            }

            var response = await client.PutAsync(requestURL, Contents);

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

        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                string requestURL = $"https://localhost:7187/api/Product/{id}";
                var response = await client.DeleteAsync(requestURL);

                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                ViewBag.Error = "Không thể xóa do sản phẩm vẫn còn quan hệ ở các bảng khác!\n(Sự kiện, voucher, hóa đơn chi tiết)";
                Console.WriteLine((ViewBag.Error as string).ToJson());
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
