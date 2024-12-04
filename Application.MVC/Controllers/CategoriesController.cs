using Application.Data.DTOs;
using Application.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace Application.MVC.Controllers
{
    public class CategoriesController : Controller
    {
       HttpClient client;
        public CategoriesController()
        {
            client = new HttpClient();
        }
        public ActionResult Index()
        {
            string requestURL = $@"https://localhost:7187/api/Category/get-all";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<List<Category>>(response);
            return View(data);
        }
        public ActionResult Details(Guid id)
        {
            string requestURL = $@"https://localhost:7187/api/Category/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<Category>(response);
            return View(data);
        }

        // GET: CategoriesController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public async Task<ActionResult> Create(CategoryDTO categoryDTO)
        { 
            try
            {
            string requestURL = $"https://localhost:7187/api/Category/create-category";
            var response = await client.PostAsJsonAsync(requestURL, categoryDTO);
            return RedirectToAction("Index");
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                return View();
            }
        }

        // GET: CategoriesController/Edit/5
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Category/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<Category>(response);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid ID, CategoryDTO categoryDTO)
        {
            string requestURL = $@"https://localhost:7187/api/Category/update-category/{ID}";
            var response =client.PutAsJsonAsync(requestURL, categoryDTO).Result;    
                return RedirectToAction(nameof(Index));
            }


        [HttpGet]
        public ActionResult Delete(Guid id)
        { try
            {
            string requestURL = $@"https://localhost:7187/api/Category/{id}";
            var response = client.DeleteAsync(requestURL).Result;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                return View();
            }
        }
    }
}
