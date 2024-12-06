using Application.Data.ModelContexts; // Namespace của DbContext
using Application.Data.Models; // Namespace của Product model
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> Index()
        {
            var productss = await client.GetFromJsonAsync<List<Product>>("https://localhost:7187/api/Product/get-all");
            ViewBag.Products = productss ?? new List<Product>();
            var products = _context.Products.ToList();
            return View(products);
        }
    }
}