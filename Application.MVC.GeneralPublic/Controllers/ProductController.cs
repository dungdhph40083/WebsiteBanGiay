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
        //private readonly GiayDBContext _context; dont 
        HttpClient client;

        private readonly HttpClient _client;

        public ProductController()
        {
            _client = new HttpClient();
        }
       
        public async Task<ActionResult> Index(int page = 1, string priceRange = "all")
        {
            var products = await _client.GetFromJsonAsync<List<Product>>("https://localhost:7187/api/Product");
            ViewBag.Orders = products ?? new List<Product>();

            var details = await _client.GetFromJsonAsync<List<ProductDetail>>("https://localhost:7187/api/ProductDetails");
            ViewBag.Details = details ?? new List<ProductDetail>();

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

            var filteredProducts = details
                .Where(p => products.Any(prod => prod.ProductID == p.ProductID && prod.Price >= minPrice && prod.Price <= maxPrice))
                .ToList();

            const int pageSize = 9;
            var totalProducts = filteredProducts.Count;
            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            var productsForCurrentPage = filteredProducts.Skip((page - 1) * pageSize).Take(pageSize).ToList();

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

            var colors = await _client.GetFromJsonAsync<List<Color>>("https://localhost:7187/api/Color");
            ViewBag.Colors = colors ?? new List<Color>();

            string productDetailUrl = $"https://localhost:7187/api/ProductDetails/{ID}";
            var productDetail = await _client.GetFromJsonAsync<ProductDetail>(productDetailUrl);


            if (productDetail == null)
            {
                return NotFound();
            }

            return View(productDetail);
        }
    }
}