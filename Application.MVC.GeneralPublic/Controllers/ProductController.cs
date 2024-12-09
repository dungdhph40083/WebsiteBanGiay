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

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 9;
            var products = await client.GetFromJsonAsync<List<Product>>("https://localhost:7187/api/Product");

            if (products != null)
            {
                int totalProducts = products.Count();
                int totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

                var pagedProducts = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                ViewBag.Products = pagedProducts;
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
            }

            return View();
        }

    }
}