using Application.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.MVC.Controllers
{
    public class CustomSupportTicketController : Controller
    {
        HttpClient client;
        public CustomSupportTicketController()
        {
            client = new HttpClient();
        }
        public ActionResult Index()
        {
            string requestURL = "https://localhost:7187/api/Color/get-all";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<List<CustomerSupportTicketDTO>>(response);
            return View(data);
        }
        public ActionResult Details(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Color/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<CustomerSupportTicketDTO>(response);
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CustomerSupportTicketDTO customerDTO)
        {
            string requestURL = $"https://localhost:7187/api/Color/create-color";
            var response = await client.PostAsJsonAsync(requestURL, customerDTO);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Color/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<CustomerSupportTicketDTO>(response);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(CustomerSupportTicketDTO customerDTO)
        {
            string requestURL = $"https://localhost:7187/api/Color/update-color/{customerDTO.TicketID}";
            var response = client.PutAsJsonAsync(requestURL, customerDTO).Result;
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
