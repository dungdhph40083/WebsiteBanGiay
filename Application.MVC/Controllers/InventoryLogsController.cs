using Application.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.MVC.Controllers
{
    public class InventoryLogsController : Controller
    {
        HttpClient client;
        public InventoryLogsController()
        {
            client = new HttpClient();
        }
        public ActionResult Index()
        {
            string requestURL = "https://localhost:7187/api/InventoryLogs";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<List<InventoryLogDto>>(response);
            return View(data);
        }
        public ActionResult Details(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/InventoryLogs/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<InventoryLogDto>(response);
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(InventoryLogDto inventoryLogDto)
        {
            string requestURL = $"https://localhost:7187/api/InventoryLogs";
            var response = await client.PostAsJsonAsync(requestURL, inventoryLogDto);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/InventoryLogs/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<ColorDTO>(response);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(InventoryLogDto inventoryLogDto)
        {
            string requestURL = $"https://localhost:7187/api/InventoryLogs/{inventoryLogDto.LogID}";
            var response = client.PutAsJsonAsync(requestURL, inventoryLogDto).Result;
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/InventoryLogs/{id}";
            var response = client.DeleteAsync(requestURL).Result;
            return RedirectToAction("Index");
        }
    }
}
