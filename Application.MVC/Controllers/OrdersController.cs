//using Application.Data.DTOs;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;

//namespace Application.MVC.Controllers
//{
//    public class OrdersController : Controller
//    {
//        HttpClient client;
//        public OrdersController()
//        {
//            client = new HttpClient();
//        }
//        public ActionResult Index()
//        {
//            string requestURL = "https://localhost:7187/api/Orders";
//            var response = client.GetStringAsync(requestURL).Result;
//            var data = JsonConvert.DeserializeObject<List<OrderDto>>(response);
//            return View(data);
//        }
//        public ActionResult Details(Guid id)
//        {
//            string requestURL = $"https://localhost:7187/api/Orders/{id}";
//            var response = client.GetStringAsync(requestURL).Result;
//            var data = JsonConvert.DeserializeObject<OrderDto>(response);
//            return View(data);
//        }

//        public ActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<ActionResult> Create(OrderDto orderDto)
//        {
//            string requestURL = $"https://localhost:7187/api/Orders";
//            var response = await client.PostAsJsonAsync(requestURL, orderDto);
//            return RedirectToAction("Index");
//        }
//        public IActionResult Edit(Guid id)
//        {
//            string requestURL = $"https://localhost:7187/api/Orders/{id}";
//            var response = client.GetStringAsync(requestURL).Result;
//            var data = JsonConvert.DeserializeObject<OrderDto>(response);
//            return View(data);
//        }
//        [HttpPost]
//        public ActionResult Edit(OrderDto orderDto)
//        {
//            string requestURL = $"https://localhost:7187/api/Orders/{orderDto.OrderID}";
//            var response = client.PutAsJsonAsync(requestURL, orderDto).Result;
//            return RedirectToAction("Index");
//        }

//        public ActionResult Delete(Guid id)
//        {
//            string requestURL = $"https://localhost:7187/api/Orders/{id}";
//            var response = client.DeleteAsync(requestURL).Result;
//            return RedirectToAction("Index");
//        }
//    }
//}
