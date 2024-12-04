using Application.Data.DTOs;
using Application.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.MVC.Controllers
{
    public class ProductWarrantyController : Controller
    {
        HttpClient client = new HttpClient();
        public ProductWarrantyController()
        {
            client = new HttpClient();
        }
        public ActionResult Index()
        {
            string requestURL = "https://localhost:7187/api/ProductWarranty";
            var response = client.GetStringAsync(requestURL).Result;
            var ProductWarrantys = JsonConvert.DeserializeObject<List<ProductWarranty>>(response);
            return View(ProductWarrantys);
        }

        public ActionResult Details(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/ProductWarranty/getbyId?ID={id}";
            var response = client.GetStringAsync(requestURL).Result;
            var ProductWarrantys = JsonConvert.DeserializeObject<ProductWarranty>(response);
            return View(ProductWarrantys);
        }
        public async Task<IActionResult> Create()
        {
            try
            {
                var products = await client.GetFromJsonAsync<List<Product>>("https://localhost:7187/api/Product/get-all");

                ViewBag.Products = products ?? new List<Product>();
                ProductWarranty ProductWarranty = new ProductWarranty()
                {
                    WarrantyID = Guid.NewGuid(),
                };
                return View(ProductWarranty);
            }
            catch (Exception ex)
            {
                // Log lỗi nếu có
                Console.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create(ProductWarranty ProductWarranty)
        {
            string requestURL = "https://localhost:7187/api/ProductWarranty/create";
            var response = await client.PostAsJsonAsync(requestURL, ProductWarranty);
            return RedirectToAction("Index");
        }
        public ActionResult Update(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/ProductWarranty/getbyId?ID={id}";
            var response = client.GetStringAsync(requestURL).Result;
            ProductWarranty ProductWarrantys = JsonConvert.DeserializeObject<ProductWarranty>(response);
            return View(ProductWarrantys);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Guid ID, ProductWarranty ProductWarranty)
        {
            string requestURL = $@"https://localhost:7187/api/Sale/update/{ID}";
            var response = await client.PutAsJsonAsync(requestURL, ProductWarranty);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/ProductWarranty/delete?ID={id}";
            var response = client.DeleteAsync(requestURL).Result;
            return RedirectToAction("Index");
        }


    }

}

