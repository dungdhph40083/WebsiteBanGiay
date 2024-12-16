using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class ContactController : Controller
    {
        HttpClient client = new HttpClient();

        public ContactController()
        {
            client = new HttpClient();
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Create()
        {
            try
            {
                var users = await client.GetFromJsonAsync<List<User>>("https://localhost:7187/api/User");
                ViewBag.Users = users ?? new List<User>();
                CustomerSupportMessage customerSupportMessage = new CustomerSupportMessage()
                {
                    MessageID = Guid.NewGuid(),
                    CreatedAt = DateTime.Now
                };


                if (TempData["SuccessMessage"] != null)
                {
                    ViewBag.SuccessMessage = TempData["SuccessMessage"];
                }

                return View(customerSupportMessage);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }
        }


        [HttpPost]
        public async Task<ActionResult> Create(CustomerSupportMessage customerSupportMessage)
        {
            string requestURL = "https://localhost:7187/api/CustomerSupportMessage";
            var response = await client.PostAsJsonAsync(requestURL, customerSupportMessage);

            if (response.IsSuccessStatusCode)
            {

                TempData["SuccessMessage"] = "Gửi tin nhắn thành công";
            }
            else
            {

                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi gửi tin nhắn.";
            }

            return RedirectToAction("Create");
        }
    }
}
