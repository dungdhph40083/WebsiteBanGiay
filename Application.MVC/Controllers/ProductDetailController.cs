using Application.Data.DTOs;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.MVC.Controllers
{
    public class ProductDetailController : Controller
    {
        HttpClient client = new HttpClient();
        public ProductDetailController()
        {
            client = new HttpClient();
        }
        public ActionResult Index()
        {
            string requestURL = "https://localhost:7187/api/ProductDetails";
            var response = client.GetStringAsync(requestURL).Result;
            var ProductDetails = JsonConvert.DeserializeObject<List<ProductDetail>>(response);
            return View(ProductDetails);
        }

        public ActionResult Details(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/ProductDetails/getbyId?ID={id}";
            var response = client.GetStringAsync(requestURL).Result;
            var ProductDetails = JsonConvert.DeserializeObject<ProductDetail>(response);
            return View(ProductDetails);
        }
        public ActionResult Create()
        {
            ProductDetail ProductDetail = new ProductDetail()
            {
                ProductDetailID = Guid.NewGuid(),
            };
            return View(ProductDetail);
        }
        [HttpPost]
        public async Task<ActionResult> Create(ProductDetail ProductDetail)
        {
            string requestURL = "https://localhost:7187/api/ProductDetails/create";
            var response = await client.PostAsJsonAsync(requestURL, ProductDetail);
            return RedirectToAction("Index");
        }
        public ActionResult Update(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/ProductDetails/getbyId?ID={id}";
            var response = client.GetStringAsync(requestURL).Result;
            ProductDetail ProductDetail = JsonConvert.DeserializeObject<ProductDetail>(response);
            return View(ProductDetail);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Guid ID, ProductDetail ProductDetail)
        {
            string requestURL = $@"https://localhost:7187/api/ProductDetails/update?ID={ID}";
            var response = await client.PutAsJsonAsync(requestURL, ProductDetail);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/ProductDetails/delete?ID={id}";
            var response = client.DeleteAsync(requestURL).Result;
            return RedirectToAction("Index");
        }
    }
}
