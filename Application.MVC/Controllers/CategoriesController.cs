﻿using Application.Data.DTOs;
using Application.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Net.Http.Headers;

namespace Application.MVC.Controllers
{
    public class CategoriesController : Controller
    {
       HttpClient client;
        private readonly HttpClient _client;
        public CategoriesController()
        {
            client = new HttpClient();
        }
        public ActionResult Index()
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string requestURL = $@"https://localhost:7187/api/Category";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<List<Category>>(response);
            return View(data);
        }
        public ActionResult Details(Guid id)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
            string requestURL = $"https://localhost:7187/api/Category";
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
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string requestURL = $"https://localhost:7187/api/Category/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<Category>(response);
            return View(data);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, CategoryDTO categoryDTO)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string requestURL = $@"https://localhost:7187/api/Category/{id}";
            var response = await client.PutAsJsonAsync(requestURL, categoryDTO);    
                return RedirectToAction(nameof(Index));
        }


        public ActionResult Delete(Guid id)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
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
