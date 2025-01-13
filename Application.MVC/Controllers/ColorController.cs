﻿using Application.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Application.MVC.Controllers
{
    public class ColorController : Controller
    {
     
        private readonly HttpClient _client;
        public ColorController()
        {
            _client = new HttpClient();
        }
        public ActionResult Index()
        {
            string requestURL = "https://localhost:7187/api/Color";
            var response = _client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<List<ColorDTO>>(response);
            return View(data);
        }
        public ActionResult Details(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Color/{id}";
            var response = _client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<ColorDTO>(response);
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ColorDTO colorDTO)
        {
            string requestURL = $"https://localhost:7187/api/Color";
            var response = await _client.PostAsJsonAsync(requestURL, colorDTO);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {  
            string requestURL = $"https://localhost:7187/api/Color/{id}";
            var response = _client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<ColorDTO>(response);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Guid id, ColorDTO colorDTO)
        {
            string requestURL = $"https://localhost:7187/api/Color/{id}";
            var response = _client.PutAsJsonAsync(requestURL, colorDTO).Result;
            return RedirectToAction("Index");
        }
        public ActionResult Delete(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Color/{id}";
            var response = _client.DeleteAsync(requestURL).Result;
            return RedirectToAction("Index");
        }
    }
}
