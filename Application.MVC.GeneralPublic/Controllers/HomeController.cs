using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using Application.Data.Repositories.IRepository;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class HomeController : Controller
    {
        HttpClient Client = new HttpClient();
        private readonly HttpClient _httpClient;
        private readonly GiayDBContext _context;
        public HomeController(HttpClient httpClient, GiayDBContext giayDBContext)
        {
            _httpClient = httpClient;
            _context = giayDBContext;
        }
        public async Task<ActionResult> Index()
        {
            var userId = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("index", "Login");
            }
            Guid parsedUserId;
            if (!Guid.TryParse(userId, out parsedUserId))
            {
                return RedirectToAction("index", "Login");
            }
            var user = _context.Users.FirstOrDefault(u => u.UserID == parsedUserId);
            if (user != null && user.IsBanned != false)
            {
                return View("Banned");
            }
            await FetchInfo();
            return View(new User());
        }

        private async Task FetchInfo()
        {
            string URL_Prods = $@"https://localhost:7187/api/ProductDetails";

            var Details = await Client.GetFromJsonAsync<List<ProductDetail>>(URL_Prods);
            // Lấy 8 con từ đầu danh sách sau khi được sắp xếp theo ngày từ cuối lên đầu

            // Sau đó cho vào ViewBag
            ViewBag.Top8Products = Details?
                .OrderByDescending(Req => Req.Product?.CreatedAt)
                .GroupBy(Sdf => Sdf.ProductID).Select(Req => Req.First())
                .Take(20).ToList() ?? new List<ProductDetail>();
        }
    }
}
