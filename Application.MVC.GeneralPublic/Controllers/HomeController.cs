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
            if (userId != null)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserID == Guid.Parse(userId));
                if(user.IsBanned == true)
                {
                    return View("Banned");
                }
                return View(user);
            }
            await FetchInfo();
            return View(new User());
        }

        private async Task FetchInfo()
        {
            string URL_Prods = "https://localhost:7187/api/ProductDetails";
            string URL_Imgds = "https://localhost:7187/api/Image";

            var Details = await Client.GetFromJsonAsync<List<ProductDetail>>(URL_Prods);
            var Images  = await Client.GetFromJsonAsync<List<Image>>(URL_Imgds);

            // Lấy 8 con từ đầu danh sách sau khi được sắp xếp theo ngày từ cuối lên đầu
            // Sau đó cho vào ViewBag
            ViewBag.Top8Products = Details?
                .OrderByDescending(Req => Req.Product?.CreatedAt)
                .GroupBy(Sdf => Sdf.ProductID).Select(Req => Req.First())
                .Take(20).ToList() ?? new List<ProductDetail>();

            ViewBag.Images = Images;
        }
    }
}
