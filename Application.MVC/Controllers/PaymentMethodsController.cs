using Application.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.MVC.Controllers
{
    public class PaymentMethodsController : Controller
    {
        HttpClient client;
        public PaymentMethodsController()
        {
            client = new HttpClient();
        }
        public ActionResult Index()
        {
            string requestURL = "https://localhost:7187/api/PaymentMethod";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<List<PaymentMethodDTO>>(response);
            return View(data);
        }
        public ActionResult Details(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/PaymentMethod/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<PaymentMethodDTO>(response);
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(PaymentMethodDTO paymentMethodDTO)
        {
            string requestURL = $"https://localhost:7187/api/PaymentMethod";
            var response = await client.PostAsJsonAsync(requestURL, paymentMethodDTO);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/PaymentMethod/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<PaymentMethodDTO>(response);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(PaymentMethodDTO paymentMethodDTO)
        {
            string requestURL = $"https://localhost:7187/api/PaymentMethod/{paymentMethodDTO.PaymentMethodID}";
            var response = client.PutAsJsonAsync(requestURL, paymentMethodDTO).Result;
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/PaymentMethodDetail/{id}";
            var response = client.DeleteAsync(requestURL).Result;
            return RedirectToAction("Index");
        }
    }
}
