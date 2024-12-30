using Application.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Application.MVC.Controllers
{
    public class ColorController : Controller
    {
        HttpClient client;
        private readonly HttpClient _client;
        public ColorController()
        {
            _client = new HttpClient();
        }
        public ActionResult Index()
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string requestURL = "https://localhost:7187/api/Color";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<List<ColorDTO>>(response);
            return View(data);
        }
        public ActionResult Details(Guid id)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string requestURL = $"https://localhost:7187/api/Color/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<ColorDTO>(response);
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ColorDTO colorDTO)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string requestURL = $"https://localhost:7187/api/Color";
            var response = await client.PostAsJsonAsync(requestURL, colorDTO);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string requestURL = $"https://localhost:7187/api/Color/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<ColorDTO>(response);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Guid id, ColorDTO colorDTO)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string requestURL = $"https://localhost:7187/api/Color/{id}";
            var response = client.PutAsJsonAsync(requestURL, colorDTO).Result;
            return RedirectToAction("Index");
        }
        public ActionResult Delete(Guid id)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string requestURL = $"https://localhost:7187/api/Color/{id}";
            var response = client.DeleteAsync(requestURL).Result;
            return RedirectToAction("Index");
        }
    }
}
