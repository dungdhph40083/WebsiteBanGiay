using Application.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.MVC.Controllers
{
    public class CategoryController : Controller
    {
       HttpClient client;
        public CategoryController()
        {
            client = new HttpClient();
        }
        public ActionResult Index()
        {
            string requestURL = $@"https://localhost:7187/api/Category/get-all";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<List<CategoryDTO>>(response);
            return View(data);
        }
        public ActionResult Details(Guid id)
        {
            string requestURL = $@"https://localhost:7187/api/Category/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<CategoryDTO>(response);
            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public async Task<ActionResult> Create(CategoryDTO categoryDTO)
        {
            string requestURL = $"https://localhost:7187/api/Category/create-category";
            var response = await client.PostAsJsonAsync(requestURL, categoryDTO);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Category/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<CategoryDTO>(response);
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryDTO categoryDTO)
        {
            string requestURL = $@"https://localhost:7187/api/Category/update-category/{categoryDTO.CategoryID}";
            var response =client.PutAsJsonAsync(requestURL, categoryDTO).Result;    
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            string requestURL = $@"https://localhost:7187/api/Category/{id}";
            var response = client.DeleteAsync(requestURL).Result;
            return RedirectToAction(nameof(Index));
        }
    }
}
