﻿using System;
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
            string URL = $@"https://localhost:7187/api/User";
            var Response = await Client.GetFromJsonAsync<List<UserDTO>>(URL);

            if (Response != null && Response.Any())
            {
                var user = Response.First(); 
                return View(user);
            }

            return View(new User()); 
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            string userRequestURL = "https://localhost:7187/api/User";
            var userResponse = await Client.GetStringAsync(userRequestURL);

            if (string.IsNullOrEmpty(userResponse))
            {
                return NotFound("User not found.");
            }

            var users = JsonConvert.DeserializeObject<List<UserDTO>>(userResponse);

            Guid currentUserId = GetCurrentUserId();
            var currentUser = users?.FirstOrDefault(u => u.UserID == currentUserId);

            if (currentUser == null)
            {
                return NotFound("User not found.");
            }

            return View(currentUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserDTO updatedUser, IFormFile? ProfilePicture)
        {
            if (updatedUser == null)
            {
                return BadRequest("Invalid user data.");
            }

            string requestURL = $"https://localhost:7187/api/User/{id}";

            MultipartFormDataContent contents = new()
    {
        { new StringContent(updatedUser.Username ?? string.Empty), nameof(updatedUser.Username) },
        { new StringContent(updatedUser.Email ?? string.Empty), nameof(updatedUser.Email) },
        { new StringContent(updatedUser.PhoneNumber ?? string.Empty), nameof(updatedUser.PhoneNumber) }
    };

            if (ProfilePicture != null)
            {
                var imageStream = new StreamContent(ProfilePicture.OpenReadStream());
                contents.Add(imageStream, nameof(ProfilePicture), ProfilePicture.FileName);
            }

            var response = await Client.PutAsync(requestURL, contents);

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

        private Guid GetCurrentUserId()
        {
            return Guid.Parse("a1411fdb-f4fa-43b6-afc4-4f9c4fc811bb");
        }

    }
}
