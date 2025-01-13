using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
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
                            HttpContext.Session.SetString("UserAvatar", Response.Image?.ImageFileName);
                        }
                        else
                        {
                            HttpContext.Session.Remove("UserAvatar");
                        }
                        HttpContext.Session.SetString("UserRole", role);
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
                    return RedirectToAction("index", "Login");
                }
                else
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra trong quá trình đăng nhập.";
                    return RedirectToAction("index", "Login");

                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi kết nối: {ex.Message}";
            }
           
            return RedirectToAction("index", "Home");
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
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] UserDTO NewUser)
        {
            if (!ModelState.IsValid)
            {
                TempData["FailureBanner"] = "Dữ liệu nhập không hợp lệ. Vui lòng kiểm tra lại.";
                return View(NewUser);
            }
            try
            {
                NewUser.RoleID = Guid.Parse(DefaultValues.UserRoleGUID);
                string apiUrl = $@"https://localhost:7187/api/User/Register";
                var response = await Client.PostAsJsonAsync(apiUrl, NewUser);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessBanner"] = "Tài khoản đã được tạo thành công!";
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    TempData["FailureBanner"] = $"Tạo tài khoản không thành công! {errorMessage}";
                    return View(NewUser);
                }
            }
            catch (Exception ex)
            {
                TempData["FailureBanner"] = $"Có lỗi xảy ra: {ex.Message}";
                return View(NewUser);
            }
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
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
