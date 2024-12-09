//using Application.Data.DTOs;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;

//namespace Application.MVC.Controllers
//{
//    public class PaymentMethodDetailsController : Controller
//    {
//        HttpClient client;
//        public PaymentMethodDetailsController()
//        {
//            client = new HttpClient();
//        }
//        public ActionResult Index()
//        {
//            string requestURL = "https://localhost:7187/api/PaymentMethodDetail";
//            var response = client.GetStringAsync(requestURL).Result;
//            var data = JsonConvert.DeserializeObject<List<PaymentMethodDetailDTO>>(response);
//            return View(data);
//        }
//        public ActionResult Details(Guid id)
//        {
//            string requestURL = $"https://localhost:7187/api/PaymentMethodDetail/{id}";
//            var response = client.GetStringAsync(requestURL).Result;
//            var data = JsonConvert.DeserializeObject<PaymentMethodDetailDTO>(response);
//            return View(data);
//        }

//        public ActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<ActionResult> Create(PaymentMethodDetailDTO paymentMethodDetailDTO)
//        {
//            string requestURL = $"https://localhost:7187/api/PaymentMethodDetail";
//            var response = await client.PostAsJsonAsync(requestURL, paymentMethodDetailDTO);
//            return RedirectToAction("Index");
//        }
//        public IActionResult Edit(Guid id)
//        {
//            string requestURL = $"https://localhost:7187/api/PaymentMethodDetail/{id}";
//            var response = client.GetStringAsync(requestURL).Result;
//            var data = JsonConvert.DeserializeObject<PaymentMethodDetailDTO>(response);
//            return View(data);
//        }
//        [HttpPost]
//        public ActionResult Edit(PaymentMethodDetailDTO paymentMethodDetailDTO)
//        {
//            string requestURL = $"https://localhost:7187/api/PaymentMethodDetail{paymentMethodDetailDTO.PaymentMethodDetails}";
//            var response = client.PutAsJsonAsync(requestURL, paymentMethodDetailDTO).Result;
//            return RedirectToAction("Index");
//        }

//        public ActionResult Delete(Guid id)
//        {
//            string requestURL = $"https://localhost:7187/api/PaymentMethodDetail/{id}";
//            var response = client.DeleteAsync(requestURL).Result;
//            return RedirectToAction("Index");
//        }
//    }
//}
