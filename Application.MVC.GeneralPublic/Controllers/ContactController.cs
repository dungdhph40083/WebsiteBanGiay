using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class ContactController : Controller
    {
        HttpClient client = new HttpClient();
        Guid UserID = Guid.Parse("BBD122D1-8961-4363-820E-3AD1A87064E4");
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
                var user = await client.GetFromJsonAsync<User>($@"https://localhost:7187/api/User/{UserID}");
                ViewBag.DefaultUser = user;
                if (TempData["SuccessMessage"] != null)
                {
                    ViewBag.SuccessMessage = TempData["SuccessMessage"];
                }

                return View();
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
