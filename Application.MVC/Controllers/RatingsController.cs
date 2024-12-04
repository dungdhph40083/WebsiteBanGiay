using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.MVC.Controllers
{
    public class RatingsController : Controller
    {
        HttpClient client = new HttpClient();
        public RatingsController()
        {
            client = new HttpClient();
        }
        public ActionResult Index()
        {
            string requestURL = "https://localhost:7187/api/Ratings";
            var response = client.GetStringAsync(requestURL).Result;
            var Ratings = JsonConvert.DeserializeObject<List<Rating>>(response);
            return View(Ratings);
        }

        public ActionResult Details(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Ratings/getbyId?ID={id}";
            var response = client.GetStringAsync(requestURL).Result;
            var Ratings = JsonConvert.DeserializeObject<Rating>(response);
            return View(Ratings);
        }
        public async Task<IActionResult> Create()
        {
            try
            {
                var products = await client.GetFromJsonAsync<List<Product>>("https://localhost:7187/api/Product/get-all");
                var users = await client.GetFromJsonAsync<List<User>>("https://localhost:7187/api/User");
                ViewBag.Products = products ?? new List<Product>();
                ViewBag.Users = users ?? new List<User>();
                Rating Rating = new Rating()
                {
                    RatingID = Guid.NewGuid(),
                };
                return View(Rating);
            }
            catch (Exception ex)
            {
                // Log lỗi nếu có
                Console.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create(Rating Rating)
        {
            string requestURL = "https://localhost:7187/api/Ratings/create";
            var response = await client.PostAsJsonAsync(requestURL, Rating);
            return RedirectToAction("Index");
        }
        public ActionResult Update(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Ratings/getbyId?ID={id}";
            var response = client.GetStringAsync(requestURL).Result;
            Rating Ratings = JsonConvert.DeserializeObject<Rating>(response);
            return View(Ratings);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Guid ID, Rating Rating)
        {
            string requestURL = $@"https://localhost:7187/api/Ratings/update?ID={ID}";
            var response = await client.PutAsJsonAsync(requestURL, Rating);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Ratings/delete?ID={id}";
            var response = client.DeleteAsync(requestURL).Result;
            return RedirectToAction("Index");
        }
    }
}
