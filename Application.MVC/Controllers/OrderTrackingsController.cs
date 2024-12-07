using Application.Data.DTOs;
using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.MVC.Controllers
{
    public class OrderTrackingsController : Controller
    {
        HttpClient client;
        public OrderTrackingsController()
        {
            client = new HttpClient();
        }
        public ActionResult Index()
        {
            string requestURL = "https://localhost:7187/api/OrderTracking";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<List<OrderTrackingDTO>>(response);
            return View(data);
        }
        public ActionResult Details(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/OrderTracking/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<OrderTrackingDTO>(response);
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(OrderTrackingDTO orderTrackingDTO)
        {
            string requestURL = $"https://localhost:7187/api/OrderTracking";
            var response = await client.PostAsJsonAsync(requestURL, orderTrackingDTO);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/OrderTracking/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<OrderTrackingDTO>(response);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(OrderTrackingDTO orderTrackingDTO)
        {
            string requestURL = $"https://localhost:7187/api/OrderTracking/{orderTrackingDTO.TrackingID}";
            var response = client.PutAsJsonAsync(requestURL, orderTrackingDTO).Result;
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/OrderTracking/{id}";
            var response = client.DeleteAsync(requestURL).Result;
            return RedirectToAction("Index");
        }
    }
}
