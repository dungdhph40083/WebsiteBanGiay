using Application.Data.Models;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productRepository;
        private readonly IImageRepository _imageRepository;

        public ProductController(IProduct productRepository, IImageRepository imageRepository)
        {
            _productRepository = productRepository;
            _imageRepository = imageRepository;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = _productRepository.GetAll();

            // Dùng async-await để lấy thông tin Image cho mỗi sản phẩm
            foreach (var product in products)
            {
                if (product.ImageID.HasValue)
                {
                    var image = await _imageRepository.GetImageByIdAsync(product.ImageID.Value);
                    product.Image = image;  // Gán thông tin Image cho sản phẩm
                }
            }

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = _productRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            // Lấy thông tin Image nếu có ImageID
            if (product.ImageID.HasValue)
            {
                product.Image = await _imageRepository.GetImageByIdAsync(product.ImageID.Value);
            }

            return Ok(product);
        }

        [HttpPost("create_product")]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            // Kiểm tra nếu có ImageID
            if (product.ImageID.HasValue)
            {
                // Lấy thông tin Image từ ImageID thông qua ImageRepository
                var image = await _imageRepository.GetImageByIdAsync(product.ImageID.Value);

                if (image != null)
                {
                    // Liên kết thông tin Image vào Product
                    product.Image = image;
                }
            }

            // Thêm sản phẩm vào cơ sở dữ liệu
             _productRepository.Add(product);
             _productRepository.Save();

            // Trả về kết quả với thông tin ImageFileName
            var response = new
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Price = product.Price,
                ImageFileName = product.Image?.ImageFileName  
            };

            return CreatedAtAction("GetProduct", new { id = product.ProductID }, response);
        }

        [HttpPut("update-product/{id}")]
        public IActionResult PutProduct(Guid id, [FromBody] Product product)
        {

            if (product == null || id != product.ProductID)
            {
                return BadRequest("Product ID mismatch or product is null.");
            }

            try
            {
                // Update product information in the database
                 _productRepository.Update(id,product);
                 _productRepository.Save();

                return Ok(product); // Return the updated product
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            _productRepository.Delete(id);
            _productRepository.Save();
            return NoContent();
        }
    }
}
