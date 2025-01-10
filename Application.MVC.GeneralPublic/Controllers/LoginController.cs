using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        HttpClient Client = new HttpClient();
        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string Username, string Password, bool RememberMe)
        {
            var loginPayload = new
            {
                username = Username,
                password = Password
            };

            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var requestContent = new StringContent(JsonSerializer.Serialize(loginPayload), Encoding.UTF8, "application/json");            

                var response = await httpClient.PostAsync("https://localhost:7187/api/Login/login", requestContent);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var token = JsonSerializer.Deserialize<LoginResponse>(responseData, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true})?.Token;

                    if (!string.IsNullOrEmpty(token))
                    {
                        HttpContext.Session.SetString("JwtToken", token);
                        HttpContext.Session.SetString(nameof(Metadata.Username), Username);
                        HttpContext.Response.Cookies.Append("AuthToken", token, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            Expires = RememberMe ? DateTimeOffset.UtcNow.AddDays(7) : DateTimeOffset.UtcNow.AddHours(1)
                        });

                        var handler = new JwtSecurityTokenHandler();
                        var jwtToken = handler.ReadJwtToken(token);
                        var role = jwtToken.Claims.FirstOrDefault(c => c.Type.Contains("role"))?.Value;
                        Guid ID = GetCurrentUserId();
                        string URL = $@"https://localhost:7187/api/User/{ID}";
                        var Response = await Client.GetFromJsonAsync<User>(URL);

                        if (Response != null)
                        {
                            HttpContext.Session.SetString("UserAvatar", Response.Image?.ImageFileName ?? "default-avatar.png");
                        }
                        if (role == "Admin")
                        {
                            return Redirect("https://localhost:7200/#");
                        }
                        else if (role == "User")
                        {
                            return RedirectToAction("index", "Home");
                        }
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Tên người dùng hoặc mật khẩu không đúng.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra trong quá trình đăng nhập.";
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi kết nối: {ex.Message}";
            }
           
            return RedirectToAction("index");
        }


        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("AuthToken");
            return RedirectToAction("Login");
        }

        private class LoginResponse
        {
            public string Token { get; set; }
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        private Guid GetCurrentUserId()
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token không tồn tại. Vui lòng đăng nhập lại.");
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserID");
            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("UserId không tồn tại trong token.");
            }

            return Guid.Parse(userIdClaim.Value);
        }
    }
}
