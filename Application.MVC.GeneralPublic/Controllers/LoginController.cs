using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
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
        private readonly GiayDBContext _giayDBContext;

        HttpClient Client = new HttpClient();
        public LoginController(IHttpClientFactory httpClientFactory, GiayDBContext giayDBContext)
        {
            _httpClientFactory = httpClientFactory;
            _giayDBContext = giayDBContext; 
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
                            if ((bool)Response.IsBanned)
                            {
                                HttpContext.Session.Remove("JwtToken");
                                HttpContext.Session.Remove(nameof(Metadata.Username));
                                HttpContext.Session.Remove("UserAvatar");
                                HttpContext.Response.Cookies.Delete("AuthToken");
                                TempData["ErrorMessage"] = "Tài khoản này đã bị cấm.";
                                return RedirectToAction("index", "Login");
                            }
                            HttpContext.Session.SetString("UserAvatar", Response.Image == null ? string.Empty : Response.Image!.ImageFileName ?? string.Empty);
                            if(Response.Image == null) HttpContext.Session.Remove("UserAvatar");
                        }
                        else
                        {
                            HttpContext.Session.Remove("UserAvatar");
                        }
                        HttpContext.Session.SetString("UserID", ID.ToString());
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

        private async Task<bool> CheckIfPasswordResetRequired(string username)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:7187/api/User/RequiresPasswordReset?username={username}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            return false;
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
                NewUser.IsBanned = DefaultValues.IsBanned;
                var email = await _giayDBContext.Users.FirstOrDefaultAsync(u => u.Email == NewUser.Email);
                if (email != null)
                {
                    TempData["FailureBanner"] = $"Email bạn nhập đã tồn tại";
                    return View(NewUser);
                }
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
                    TempData["FailureBanner"] = $"Tạo tài khoản không thành công!";
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
        public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordDto forgotPasswordDto)
        {

            if (!ModelState.IsValid)
            {
                TempData["FailureBanner"] = "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại.";
                return View(forgotPasswordDto);
            }

            try
            {
                string apiUrl = @"https://localhost:7187/api/User/ForgotPassword";
                var response = await Client.PostAsJsonAsync(apiUrl, forgotPasswordDto);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessBanner"] = "Email đặt lại mật khẩu đã được gửi. Vui lòng kiểm tra hộp thư.";
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    TempData["FailureBanner"] = $"Yêu cầu quên mật khẩu không thành công! {errorMessage}";
                    return View(forgotPasswordDto);
                }
            }
            catch (Exception ex)
            {
                TempData["FailureBanner"] = $"Có lỗi xảy ra: {ex.Message}";
                return View(forgotPasswordDto);
            }
        }

        [HttpGet]
        public IActionResult ResetPassword(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token không hợp lệ.");
            }

            ViewBag.Token = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                TempData["FailureBanner"] = "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại.";
                return View(resetPasswordDto);
            }

            try
            {
                string apiUrl = @"https://localhost:7187/api/User/ResetPassword";
                var response = await Client.PostAsJsonAsync(apiUrl, resetPasswordDto);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessBanner"] = "Mật khẩu của bạn đã được đặt lại thành công!";
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    TempData["FailureBanner"] = $"Đặt lại mật khẩu không thành công! {errorMessage}";
                    return View(resetPasswordDto);
                }
            }
            catch (Exception ex)
            {
                TempData["FailureBanner"] = $"Có lỗi xảy ra: {ex.Message}";
                return View(resetPasswordDto);
            }
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
