using Application.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.MVC.Controllers
{
    public class SaleController : Controller
    {
        HttpClient client = new HttpClient();
        public SaleController()
        {
            client = new HttpClient();
        }
        public ActionResult Index()
        {
            string requestURL = "https://localhost:7187/api/Sale";
            var response = client.GetStringAsync(requestURL).Result;
            var Sales = JsonConvert.DeserializeObject<List<Sale>>(response);
            return View(Sales);
        }

        public ActionResult Details(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Sale/getbyId?id={id}";
            var response = client.GetStringAsync(requestURL).Result;
            var Sales = JsonConvert.DeserializeObject<Sale>(response);
            return View(Sales);
        }
        public async Task<IActionResult> Create()
        {
            try
            {
                var products = await client.GetFromJsonAsync<List<Product>>("https://localhost:7187/api/Product/get-all");

                ViewBag.Products = products ?? new List<Product>();
                Sale Sale = new Sale()
                {
                    SaleID = Guid.NewGuid(),
                };
                return View(Sale);
            }
            catch (Exception ex)
            {
                // Log lỗi nếu có
                Console.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }
            
        }
        [HttpPost]
        public async Task<ActionResult> Create(Sale Sale)
        {
            string requestURL = "https://localhost:7187/api/Sale/create";
            var response = await client.PostAsJsonAsync(requestURL, Sale);
            return RedirectToAction("Index");
        }
        public ActionResult Update(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Sale/getbyId?id={id}";
            var response = client.GetStringAsync(requestURL).Result;
            Sale Sales = JsonConvert.DeserializeObject<Sale>(response);
            return View(Sales);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Guid ID, Sale Sale)
        {
            string requestURL = $@"https://localhost:7187/api/Sale/update?id={ID}";
            var response = await client.PutAsJsonAsync(requestURL, Sale);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Sale/delete?id={id}";
            var response = client.DeleteAsync(requestURL).Result;
            return RedirectToAction("Index");
        }
    }
}
