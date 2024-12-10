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

        public ProductController(GiayDBContext context)
        {
            _context = context;
            client = new HttpClient();
        }

        public async Task<IActionResult> Index(string priceRange = "all", int page = 1)
        {
            var pageSize = 9;
            var products = new List<Product>();

            try
            {
                // Fetch products from the API
                products = await client.GetFromJsonAsync<List<Product>>("https://localhost:7187/api/Product");
            }
            catch (Exception ex)
            {
                // Handle potential errors like API call failure
                // You could log the error or return an appropriate message
                Console.WriteLine($"Error fetching products: {ex.Message}");
                products = new List<Product>();
            }

            // Lọc theo giá
            if (!string.IsNullOrEmpty(priceRange) && priceRange != "all")
            {
                var priceRanges = new Dictionary<string, (decimal min, decimal max)>
        {
            { "0-1", (99000, 500000) },
            { "1-2", (500000, 1000000) },
            { "2-3", (1000000, 2000000) },
            { "3-4", (2000000, 5000000) }
        };

                if (priceRanges.ContainsKey(priceRange))
                {
                    var range = priceRanges[priceRange];
                    products = products.Where(p => p.Price >= range.min && p.Price <= range.max).ToList();
                }
            }

            // Phân trang
            var totalProducts = products.Count();
            var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);
            var pagedProducts = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Trả về View
            ViewBag.Products = pagedProducts;
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.SelectedPrice = priceRange;

            return View();
        }


    }
}