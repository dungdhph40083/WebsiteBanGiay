using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.MVC.Controllers
{
    public class RoleController : Controller
    {
        HttpClient client = new HttpClient();
        public RoleController()
        {
            client = new HttpClient();
        }
        public ActionResult Index()
        {
            string requestURL = "https://localhost:7187/api/Role";
            var response = client.GetStringAsync(requestURL).Result;
            var Roles = JsonConvert.DeserializeObject<List<Role>>(response);
            return View(Roles);
        }

        public ActionResult Details(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Role/getbyId?id={id}";
            var response = client.GetStringAsync(requestURL).Result;
            var Roles = JsonConvert.DeserializeObject<Role>(response);
            return View(Roles);
        }
        public ActionResult Create()
        {
            Role Role = new Role()
            {
                RoleID = Guid.NewGuid(),
            };
            return View(Role);
        }
        [HttpPost]
        public async Task<ActionResult> Create(Role Role)
        {
            string requestURL = "https://localhost:7187/api/Role/create";
            var response = await client.PostAsJsonAsync(requestURL, Role);
            return RedirectToAction("Index");
        }
        public ActionResult Update(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Role/getbyId?id={id}";
            var response = client.GetStringAsync(requestURL).Result;
            Role Roles = JsonConvert.DeserializeObject<Role>(response);
            return View(Roles);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Guid ID, Role Role)
        {
            string requestURL = $@"https://localhost:7187/api/Role/update?id={ID}";
            var response = await client.PutAsJsonAsync(requestURL, Role);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Role/delete?id={id}";
            var response = client.DeleteAsync(requestURL).Result;
            return RedirectToAction("Index");
        }
    }
}
