using Application.Data.DTOs;
using Application.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class LoginController : Controller
    {
        HttpClient Client = new HttpClient();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserDTO Input)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/User";

                MultipartFormDataContent Contents = new()
                {
                    { new StringContent(Input.Username!),                              nameof(Input.Username) },
                    { new StringContent(Input.Password!),                              nameof(Input.Password) },
                    { new StringContent(Input.FirstName ?? ""),                        nameof(Input.FirstName) },
                    { new StringContent(Input.LastName ?? ""),                         nameof(Input.LastName) },
                    { new StringContent(Input.Email ?? ""),                            nameof(Input.Email) },
                    { new StringContent(Input.Address ?? ""),                          nameof(Input.Address) },
                    { new StringContent(Input.PhoneNumber ?? ""),                      nameof(Input.PhoneNumber) },
                    { new StringContent(((int)VisibilityStatus.Available).ToString()), nameof(Input.Status) }
                };

                var Response = await Client.PostAsync(URL, Contents);

                if (Response.StatusCode == HttpStatusCode.OK || Response.StatusCode == HttpStatusCode.Created)
                {
                    TempData["SuccessBanner"] = "SUCCESS";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                TempData["FailureBanner"] = $"{Msg.Message} ({Msg.HResult})";
                return View();
            }
        }
    }
}
