using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.MVC.Controllers
{
    public class ReturnsController : Controller
    {
        HttpClient client = new HttpClient();
        public ReturnsController()
        {
            client = new HttpClient();
        }
        public ActionResult Index()
        {
            string requestURL = "https://localhost:7187/api/Returns";
            var response = client.GetStringAsync(requestURL).Result;
            var Returns = JsonConvert.DeserializeObject<List<Return>>(response);
            return View(Returns);
        }

        public ActionResult Details(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Returns/getbyId?ID={id}";
            var response = client.GetStringAsync(requestURL).Result;
            var Returns = JsonConvert.DeserializeObject<Return>(response);
            return View(Returns);
        }
        public async Task<IActionResult> Create()
        {
            try
            {
                var orders = await client.GetFromJsonAsync<List<Order>>("https://localhost:7187/api/Orders");
                ViewBag.Orders = orders ?? new List<Order>();
                Return Return = new Return()
                {
                    ReturnID = Guid.NewGuid(),
                };
                return View(Return);
            }
            catch (Exception ex)
            {
                // Log lỗi nếu có
                Console.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create(Return Return)
        {
            string requestURL = "https://localhost:7187/api/Returns/create";
            var response = await client.PostAsJsonAsync(requestURL, Return);
            return RedirectToAction("Index");
        }
        public ActionResult Update(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Returns/getbyId?ID={id}";
            var response = client.GetStringAsync(requestURL).Result;
            Return Returns = JsonConvert.DeserializeObject<Return>(response);
            return View(Returns);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Guid ID, Return Return)
        {
            string requestURL = $@"https://localhost:7187/api/Returns/update?ID={ID}";
            var response = await client.PutAsJsonAsync(requestURL, Return);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Returns/delete?ID={id}";
            var response = client.DeleteAsync(requestURL).Result;
            return RedirectToAction("Index");
        }
    }
}
