using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Application.Data.ModelContexts;
using Application.Data.Models;


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
            var Response = await Client.GetFromJsonAsync<List<User>>(URL);

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

            string url = "https://localhost:7187/api/User";
            var users = await Client.GetFromJsonAsync<List<User>>(url);


            Guid currentUserId = GetCurrentUserId(); 

            var currentUser = users?.FirstOrDefault(u => u.UserID == currentUserId);

            if (currentUser == null)
            {
                return NotFound("User not found.");
            }

            return View(currentUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedUser);
            }
            string url = "https://localhost:7187/api/User";
            var response = await Client.PutAsJsonAsync(url, updatedUser);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); 
            }
            else
            {
                ModelState.AddModelError("", "Failed to update user profile.");
                return View(updatedUser);
            }
        }

        private Guid GetCurrentUserId()
        {
            return Guid.Parse("a1411fdb-f4fa-43b6-afc4-4f9c4fc811bb");
        }

    }
}
