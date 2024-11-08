using Application.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.MVC.Controllers
{
    public class SizeController : Controller
    {
        HttpClient client;
        public SizeController()
        {
            client = new HttpClient();
        }
        public ActionResult Index()
        {
            string requestURL = "https://localhost:7187/api/Size/get-all";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<List<SizeDTO>>(response);
            return View(data);
        }
        public ActionResult Details(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Size/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<SizeDTO>(response);
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(SizeDTO sizeDTO)
        {
            string requestURL = $"https://localhost:7187/api/Size/create-size";
            var response = await client.PostAsJsonAsync(requestURL, sizeDTO);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Size/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<SizeDTO>(response);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(SizeDTO sizeDTO)
        {
            string requestURL = $"https://localhost:7187/api/Size/update-size/{sizeDTO.SizeID}";
            var response = client.PutAsJsonAsync(requestURL, sizeDTO).Result;
            return RedirectToAction("Index");
        }
        public ActionResult Delete(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Size/{id}";
            var response = client.DeleteAsync(requestURL).Result;
            return RedirectToAction("Index");
        }

    }
}
