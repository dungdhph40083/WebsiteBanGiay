using Application.Data.ModelContexts;
using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class ProductController : Controller
    {
        private readonly GiayDBContext _context;
        HttpClient client;

        private readonly HttpClient _client;

        public ProductController()
        {
            _client = new HttpClient();
        }


        public async Task<ActionResult> Index(int page = 1, string priceRange = "all")
        {
            // Fetch all products and product details
            var products = await _client.GetFromJsonAsync<List<Product>>("https://localhost:7187/api/Product");
            ViewBag.Orders = products ?? new List<Product>();

            var details = await _client.GetFromJsonAsync<List<ProductDetail>>("https://localhost:7187/api/ProductDetails");
            ViewBag.Details = details ?? new List<ProductDetail>();

            // Price range filtering
            decimal minPrice = 0, maxPrice = decimal.MaxValue;

            if (priceRange == "0-1")
            {
                minPrice = 99000;
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
                maxPrice = 5000000;
            }

            // Filter products by price range (using price from Product)
            var filteredProducts = details
                .Where(p => products.Any(prod => prod.ProductID == p.ProductID && prod.Price >= minPrice && prod.Price <= maxPrice))
                .ToList();

            // Pagination logic
            const int pageSize = 9; // Number of products per page
            var totalProducts = filteredProducts.Count;
            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            // Get products for the current page
            var productsForCurrentPage = filteredProducts.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Store pagination data and selected price range in ViewBag for use in the view
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SelectedPrice = priceRange;

            return View(productsForCurrentPage);
        }




        public async Task<ActionResult> Details(Guid ID)
        {
            var productss = await _client.GetFromJsonAsync<List<Product>>("https://localhost:7187/api/Product");
            ViewBag.Products = productss ?? new List<Product>();

            var sizes = await _client.GetFromJsonAsync<List<Size>>("https://localhost:7187/api/Size");
            ViewBag.Sizes = sizes ?? new List<Size>();

            // Fetch the list of colors
            var colors = await _client.GetFromJsonAsync<List<Color>>("https://localhost:7187/api/Color");
            ViewBag.Colors = colors ?? new List<Color>();

            // Fetch product details using the provided ID
            string productDetailUrl = $"https://localhost:7187/api/ProductDetails/{ID}";
            var productDetail = await _client.GetFromJsonAsync<ProductDetail>(productDetailUrl);

            // Check if product detail exists
            if (productDetail == null)
            {
                return NotFound();
            }

            return View(productDetail);
        }


    }
}