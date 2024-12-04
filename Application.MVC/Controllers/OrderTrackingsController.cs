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
            var data = JsonConvert.DeserializeObject<List<OderTrackingDTO>>(response);
            return View(data);
        }
        public ActionResult Details(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/OrderTracking/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<OderTrackingDTO>(response);
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(OderTrackingDTO oderTrackingDTO)
        {
            string requestURL = $"https://localhost:7187/api/OrderTracking";
            var response = await client.PostAsJsonAsync(requestURL, oderTrackingDTO);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/OrderTracking/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<OderTrackingDTO>(response);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(OderTrackingDTO oderTrackingDTO)
        {
            string requestURL = $"https://localhost:7187/api/OrderTracking/{oderTrackingDTO.TrackingID}";
            var response = client.PutAsJsonAsync(requestURL, oderTrackingDTO).Result;
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
