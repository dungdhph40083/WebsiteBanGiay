using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Newtonsoft.Json;
using Application.Data.DTOs;
using NuGet.Protocol;
using System.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using System.Security.Policy;


namespace Application.MVC.GeneralPublic.Controllers
{
    public class ProfileController : Controller
    {

        HttpClient Client = new HttpClient();
        public ProfileController()
        {
   
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            Guid ID = GetCurrentUserId();

            string URL = $@"https://localhost:7187/api/User/{ID}";
            var Response = await Client.GetFromJsonAsync<User>(URL);

            if (Response != null)
            {
                return View(Response);
            }

            return View(new User()); 
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            Guid ID = GetCurrentUserId();

            string userRequestURL = $@"https://localhost:7187/api/User/{ID}";
            var userResponse = await Client.GetFromJsonAsync<UserDTO>(userRequestURL);

            if (userResponse == null)
            {
                return NotFound("User not found.");
            }

            return View(userResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Edit( UserDTO updatedUser, [FromForm] IFormFile? ProfilePicture)
        {
            if (updatedUser == null)
            {
                return BadRequest("Invalid user data.");
            }

           Guid id = GetCurrentUserId();

            string requestURL = $"https://localhost:7187/api/User/{id}";

            MultipartFormDataContent contents = new()
    {
        { new StringContent(updatedUser.Username ?? string.Empty), nameof(updatedUser.Username) },
        { new StringContent(updatedUser.Email ?? string.Empty), nameof(updatedUser.Email) }, 
        { new StringContent(updatedUser.PhoneNumber ?? string.Empty), nameof(updatedUser.PhoneNumber) },
        { new StringContent(updatedUser.Address ?? string.Empty), nameof(updatedUser.Address) },
        { new StringContent(updatedUser.LastName ?? string.Empty), nameof(updatedUser.LastName) },
        { new StringContent(updatedUser.FirstName ?? string.Empty), nameof(updatedUser.FirstName) },
    };

            if (ProfilePicture != null)
            {
                var imageStream = new StreamContent(ProfilePicture.OpenReadStream());
                contents.Add(imageStream, "NewProfilePic", ProfilePicture.FileName);
            }

            var response = await Client.PutAsync(requestURL, contents);
      
            var Response = await Client.GetFromJsonAsync<User>(requestURL);
            if (Response != null)
            {
                HttpContext.Session.SetString("UserAvatar", Response.Image?.ImageFileName ?? "default-avatar.png");
            }
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Update failed: {error}");
                return View(updatedUser);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuthentication"); 
            HttpContext.Session.Clear(); 

            return RedirectToAction("Index", "Home");
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
