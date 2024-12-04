using Application.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.MVC.Controllers
{
    public class OrderDetailsController : Controller
    {
        HttpClient client;
        public OrderDetailsController()
        {
            client = new HttpClient();
        }
        public ActionResult Index()
        {
            string requestURL = "https://localhost:7187/api/Color/get-all";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<List<OrderDetailDto>>(response);
            return View(data);
        }
        public ActionResult Details(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Color/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<OrderDetailDto>(response);
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(OrderDetailDto orderDetailDto)
        {
            string requestURL = $"https://localhost:7187/api/Color/create-color";
            var response = await client.PostAsJsonAsync(requestURL, orderDetailDto);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Color/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<OrderDetailDto>(response);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(OrderDetailDto orderDetailDto)
        {
            string requestURL = $"https://localhost:7187/api/Color/update-color/{orderDetailDto.OrderDetailID}";
            var response = client.PutAsJsonAsync(requestURL, orderDetailDto).Result;
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Color/{id}";
            var response = client.DeleteAsync(requestURL).Result;
            return RedirectToAction("Index");
        }
    }
}
