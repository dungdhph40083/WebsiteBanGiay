using Application.Data.ModelContexts;
using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class ProductController : Controller
    {
        HttpClient client = new HttpClient();

        public async Task<ActionResult> Index(int page = 1, string priceRange = "all", string sortOrder = "default", Guid? categoryId = null, Guid? colorId = null)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token không tồn tại. Vui lòng đăng nhập lại.");
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); var products = await client.GetFromJsonAsync<List<Product>>("https://localhost:7187/api/Product");
            ViewBag.Orders = products ?? new List<Product>();

            // Lấy danh sách ProductDetails từ API
            var details = await client.GetFromJsonAsync<List<ProductDetail>>("https://localhost:7187/api/ProductDetails");
            ViewBag.Details = details ?? new List<ProductDetail>();

            // Lấy danh sách danh mục (Categories) từ API
            var categories = await client.GetFromJsonAsync<List<Category>>("https://localhost:7187/api/Category");
            //
            var colors = await client.GetFromJsonAsync<List<Color>>("https://localhost:7187/api/Color");
            //
            var Sizes = await client.GetFromJsonAsync<List<Size>>("https://localhost:7187/api/Size");

            // Kết hợp danh mục với số lượng sản phẩm
            var categoryViewData = categories?.Select(category => new
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                ProductCount = details?.Count(d => d.CategoryID == category.CategoryID) ?? 0
            }).ToList();

            ViewBag.Categories = categoryViewData;
            //
            var colorViewData = colors?.Select(color => new
            {
                ColorID = color.ColorID,
                ColorName = color.ColorName,
                ProductCount = details?.Count(d => d.ColorID == color.ColorID) ?? 0
            }).ToList();

            ViewBag.colors = colorViewData;
            //
            decimal minPrice = 0, maxPrice = decimal.MaxValue;
            if (priceRange == "0-1")
            {
                minPrice = 10000;
                maxPrice = 500000;
            }
            else if (priceRange == "1-2")
            {
                minPrice = 500000;
                maxPrice = 1000000;
            }
            else if (priceRange == "2-3")
            {
                minPrice = 1000000;
                maxPrice = 2000000;
            }
            else if (priceRange == "3-4")
            {
                minPrice = 2000000;
                maxPrice = 500000000;
            }

            // Lọc sản phẩm theo khoảng giá
            var filteredProducts = details?
                .Where(p => products!.Any(prod => prod.ProductID == p.ProductID && prod.Price >= minPrice && prod.Price <= maxPrice))
                .ToList();

            // Lọc sản phẩm theo danh mục
            if (categoryId.HasValue)
            {
                filteredProducts = filteredProducts?.Where(p => p.CategoryID == categoryId).ToList();
            }
            if (colorId.HasValue)
            {
                filteredProducts = filteredProducts?.Where(p => p.ColorID == colorId).ToList();
            }

            // Sắp xếp sản phẩm
            switch (sortOrder)
            {
                case "price_asc":
                    filteredProducts = filteredProducts?
                        .OrderBy(p => products?.First(prod => prod.ProductID == p.ProductID).Price)
                        .ToList();
                    break;
                case "price_desc":
                    filteredProducts = filteredProducts?
                        .OrderByDescending(p => products?.First(prod => prod.ProductID == p.ProductID).Price)
                        .ToList();
                    break;
            }

            // Phân trang
            const int pageSize = 9;
            var totalProducts = filteredProducts?.Count;
            var totalPages = (int)Math.Ceiling((double)totalProducts.GetValueOrDefault() / pageSize);

            var productsForCurrentPage = filteredProducts?.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Truyền dữ liệu vào View
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SelectedPrice = priceRange;
            ViewBag.SortOrder = sortOrder;
            ViewBag.SelectedCategory = categoryId;
            ViewBag.SelectedColor = colorId;

            return View(productsForCurrentPage);
        }

        public async Task<ActionResult> Details(Guid ID)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token không tồn tại. Vui lòng đăng nhập lại.");
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var productss = await _client.GetFromJsonAsync<List<Product>>("https://localhost:7187/api/Product");
            ViewBag.Products = productss ?? new List<Product>();

            var sizes = await client.GetFromJsonAsync<List<Size>>("https://localhost:7187/api/Size");
            ViewBag.Sizes = sizes ?? new List<Size>();

            var colors = await client.GetFromJsonAsync<List<Color>>("https://localhost:7187/api/Color");
            ViewBag.Colors = colors ?? new List<Color>();

            string productDetailUrl = $"https://localhost:7187/api/ProductDetails/{ID}";
            var productDetail = await client.GetFromJsonAsync<ProductDetail>(productDetailUrl);


            if (productDetail == null)
            {
                return NotFound();
            }

            return View(productDetail);
        }
    }
}