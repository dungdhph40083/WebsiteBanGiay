using Application.Data.DTOs;
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
            var sortedResponse = data.OrderByDescending(p => p.CreatedAt).ToList(); // Đảm bảo lời nhắn mới nhất lên đầu
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
            string requestURL = $@"https://localhost:7187/api/CustomerSupportMessage/{id}/toggle-status";
            var response = await client.PutAsync(requestURL, null);

            if (response.IsSuccessStatusCode)
            {
                return Ok(); // Trả về 200 nếu thành công
            }
            else
            {
                return StatusCode((byte)response.StatusCode, "Đổi trạng thái thất bại."); // Trả về lỗi nếu không thành công
            }
        }
    }
}
