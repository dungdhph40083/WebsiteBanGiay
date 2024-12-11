using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly HttpClient _client;
        //private readonly GiayDBContext _context;
        public ProductDetailController(/*GiayDBContext context*/)
        {
            _client = new();
            //_context = context;
        }
        public async Task<IActionResult> Index(Guid id)
        {
            string apiUrl = "https://localhost:7187/api/ProductDetails";
            List<ProductDetail> productDetails = new();

            try
            {
                var response = await _client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    productDetails = await response.Content.ReadFromJsonAsync<List<ProductDetail>>();
                }
                else
                {
                    ViewBag.ErrorMessage = "Không thể lấy dữ liệu từ server.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Lỗi xảy ra: {ex.Message}";
            }

            
            //var productDetail = productDetails.FirstOrDefault(p => p.ProductDetailID == id);

            //if (productDetail == null)
            //{
            //    return NotFound("Sản phẩm không tồn tại.");
            //}

            
            //ViewBag.Size = new List<Size> { productDetail.Size };
            //ViewBag.Color = new List<Color> { productDetail.Color };

            
            return View(productDetails);
        }
    }
}
