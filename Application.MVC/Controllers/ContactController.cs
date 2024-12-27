using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.MVC.Controllers
{
    public class ContactController : Controller
    {
        HttpClient client;
        public ContactController()
        {
             client = new HttpClient();
        }
        public IActionResult Index()
        {
            string requestURL = $@"https://localhost:7187/api/CustomerSupportMessage";
            var response = client.GetStringAsync(requestURL).Result;

            var data = JsonConvert.DeserializeObject<List<CustomerSupportMessage>>(response);
            var sortedResponse = data.OrderByDescending(p => p.CreatedAt).ToList();
            return View(data);
        }
    }
}
