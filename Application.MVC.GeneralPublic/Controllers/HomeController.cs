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
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var userId = HttpContext.Session.GetString("UserID");
            if (userId != null)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserID == Guid.Parse(userId));
                return View(user);
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
                .OrderByDescending(Req => Req.Product?.CreatedAt).Take(10).ToList() ?? new List<ProductDetail>();
        }
    }
}
