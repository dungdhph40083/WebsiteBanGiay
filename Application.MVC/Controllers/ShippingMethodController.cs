using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.MVC.Controllers
{
    public class ShippingMethodController : Controller
    {
        HttpClient client = new HttpClient();
        public ShippingMethodController()
        {
            client = new HttpClient();
        }
        public ActionResult Index()
        {
            string requestURL = "https://localhost:7187/api/ShippingMethod";
            var response = client.GetStringAsync(requestURL).Result;
            var ShippingMethods = JsonConvert.DeserializeObject<List<ShippingMethod>>(response);
            return View(ShippingMethods);
        }

        public ActionResult Details(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/ShippingMethod/getbyId?id={id}";
            var response = client.GetStringAsync(requestURL).Result;
            var ShippingMethods = JsonConvert.DeserializeObject<ShippingMethod>(response);
            return View(ShippingMethods);
        }
        public ActionResult Create()
        {
            ShippingMethod ShippingMethod = new ShippingMethod()
            {
                ShippingMethodID = Guid.NewGuid(),
            };
            return View(ShippingMethod);
        }
        [HttpPost]
        public async Task<ActionResult> Create(ShippingMethod ShippingMethod)
        {
            string requestURL = "https://localhost:7187/api/ShippingMethod/create";
            var response = await client.PostAsJsonAsync(requestURL, ShippingMethod);
            return RedirectToAction("Index");
        }
        public ActionResult Update(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/ShippingMethod/getbyId?id={id}";
            var response = client.GetStringAsync(requestURL).Result;
            ShippingMethod ShippingMethods = JsonConvert.DeserializeObject<ShippingMethod>(response);
            return View(ShippingMethods);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Guid ID, ShippingMethod ShippingMethod)
        {
            string requestURL = $@"https://localhost:7187/api/ShippingMethod/update?id={ID}";
            var response = await client.PutAsJsonAsync(requestURL, ShippingMethod);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/ShippingMethod/delete?id={id}";
            var response = client.DeleteAsync(requestURL).Result;
            return RedirectToAction("Index");
        }
    }
}
