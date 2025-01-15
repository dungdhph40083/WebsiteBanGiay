using Application.Data.DTOs;
using Application.Data.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace Application.MVC.Controllers
{
    public class ContactController : Controller
    {
        HttpClient client = new HttpClient();
        private readonly INotyfService ToastNotifier;
        public ContactController(INotyfService ToastNotifier)
        {
            this.ToastNotifier = ToastNotifier;
        }

        public IActionResult Index()
        {
            string requestURL = $@"https://localhost:7187/api/CustomerSupportMessage";
            var response = client.GetStringAsync(requestURL).Result;

            var data = JsonConvert.DeserializeObject<List<CustomerSupportMessage>>(response);
            var sortedResponse = data?.OrderByDescending(p => p.CreatedAt).ToList(); // Đảm bảo lời nhắn mới nhất lên đầu
            return View(sortedResponse); // Sử dụng sortedResponse để hiển thị
        }

        public ActionResult Details(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/CustomerSupportMessage/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<CustomerSupportMessage>(response);
            return View(data);
        }
        [HttpPut]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            string requestURL = $@"https://localhost:7187/api/CustomerSupportMessage/{id}/ToggleStatus";
            var Response = await client.PutAsync(requestURL, null);

            if (Response.IsSuccessStatusCode)
            {
                ToastNotifier.Success("Đổi trạng thái thành công!");
            }
            else
            {
                ToastNotifier.Error("Đổi trạng thái thất bại.");
            }
            return View();
        }
    }
}
