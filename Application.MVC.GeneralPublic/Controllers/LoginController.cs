using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.Models;
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
        public async Task<IActionResult> Login(string usernameOrEmail, string password, bool rememberMe)
        {
            if (string.IsNullOrEmpty(usernameOrEmail) || string.IsNullOrEmpty(password))
            {
                TempData["Error"] = "Tên người dùng hoặc mật khẩu không được để trống.";
                return View();
            }
            var user = await AuthenticateUser(usernameOrEmail, password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.UserID.ToString());
                HttpContext.Session.SetString("Username", user.Username);

                if (rememberMe)
                {
                    Response.Cookies.Append("RememberMe", user.UserID.ToString(), new CookieOptions
                    {
                        Expires = DateTimeOffset.Now.AddDays(30)
                    });
                }
                return RedirectToAction("Index", "Home");
            }

            TempData["Error"] = "Tên người dùng hoặc mật khẩu không đúng.";
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); 
            Response.Cookies.Delete("RememberMe"); 
            return RedirectToAction("Login");
        }

        private async Task<User> AuthenticateUser(string usernameOrEmail, string password)
        {
            string authUrl = $@"https://localhost:7187/api/Auth/Login";
            var payload = new
            {
                UsernameOrEmail = usernameOrEmail,
                Password = password
            };
            var response = await Client.PostAsJsonAsync(authUrl, payload);
            if (response.IsSuccessStatusCode)
            {

                var user = await response.Content.ReadFromJsonAsync<User>();
                return user;
            }
            return null;
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
