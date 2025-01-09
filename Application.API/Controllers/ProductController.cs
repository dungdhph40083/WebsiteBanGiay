using Application.API.Service;
using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.Models;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productRepository;
        private readonly IProductDetail ProductDetailRepo;
        private readonly IImageRepository _imageRepository;

        public ProductController(IProduct productRepository, IImageRepository imageRepository, IProductDetail ProductDetailRepo)
        {
            _productRepository = productRepository;
            _imageRepository = imageRepository;
            this.ProductDetailRepo = ProductDetailRepo;
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<IEnumerable<Product?>>> GetProducts()
        {
            return await _productRepository.GetAll();
        }
        
        [HttpGet("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<Product?>> GetProduct(Guid id)
        {
            return await _productRepository.GetById(id);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Product>> PostProduct([FromForm] ProductDTO product, IFormFile? Image)
        {
            // Kiểm tra và xử lý ảnh nếu có
            if (Image != null)
            {
                switch (ImageUploaderValidator.ValidateImageSizeAndHeader(Image, 4_194_304))  // Kiểm tra kích thước và định dạng ảnh
                {
                    case ErrorResult.IMAGE_TOO_BIG_ERROR:
                        return BadRequest($"Tệp ảnh không được vượt quá {4_194_304 / 1_048_576}MB!"); // Nếu ảnh quá lớn
                    case ErrorResult.IMAGE_IS_BROKEN_ERROR:
                    default:
                        return BadRequest("Tệp ảnh không hợp lệ!"); // Nếu ảnh bị lỗi
                    case SuccessResult.IMAGE_OK:
                        // Nếu ảnh hợp lệ, lưu ảnh vào hệ thống
                        var createdImage = await _imageRepository.CreateImageAsync(Image);
                        product.ImageID = createdImage.ImageID; // Gán ID ảnh vào sản phẩm
                        break;
                }
            }

            // Kiểm tra tên sản phẩm xem đã tồn tại trong cơ sở dữ liệu chưa
            var existingProduct = await _productRepository.CheckProductNameAsync(product.Name);
            if (existingProduct != null)
            {
                return Conflict("Tên sản phẩm đã tồn tại!");  // Nếu tên sản phẩm đã có, trả về lỗi
            }

            // Thêm sản phẩm vào cơ sở dữ liệu
            var response = await _productRepository.Add(product);

            // Trả về phản hồi sau khi thêm sản phẩm thành công
            return CreatedAtAction(nameof(GetProduct), new { id = response.ProductID }, response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Product?>> PutProduct(Guid id, [FromForm] ProductDTO product, IFormFile? Image)
        {
            // Kiểm tra xem sản phẩm có tồn tại không
            var existingProduct = await _productRepository.GetById(id);
            if (existingProduct == null)
            {
                return NotFound("Sản phẩm không tồn tại.");
            }

            // Nếu tên sản phẩm thay đổi, kiểm tra xem tên mới có bị trùng không
            if (!string.Equals(existingProduct.Name, product.Name, StringComparison.OrdinalIgnoreCase))
            {
                var checkProductName = await _productRepository.CheckProductNameAsync(product.Name);
                if (checkProductName != null)
                {
                    return Conflict("Tên sản phẩm đã tồn tại.");
                }
            }

            // Nếu không có ảnh mới, giữ nguyên ảnh cũ
            if (Image == null)
            {
                product.ImageID = existingProduct.ImageID; // Giữ nguyên ảnh cũ
            }
            else
            {
                // Kiểm tra và xử lý ảnh nếu có
                switch (ImageUploaderValidator.ValidateImageSizeAndHeader(Image, 4_194_304))
                {
                    case ErrorResult.IMAGE_TOO_BIG_ERROR:
                        return BadRequest($"Tệp ảnh không được vượt quá {4_194_304 / 1_048_576}MB!");
                    case ErrorResult.IMAGE_IS_BROKEN_ERROR:
                    default:
                        return BadRequest("Tệp ảnh không hợp lệ!");
                    case SuccessResult.IMAGE_OK:
                        var createdImage = await _imageRepository.CreateImageAsync(Image);
                        product.ImageID = createdImage.ImageID; // Gán ID ảnh vào sản phẩm
                        break;
                }
            }

            // Cập nhật sản phẩm
            var updatedProduct = await _productRepository.Update(id, product);

            if (updatedProduct == null)
            {
                return NotFound("Không thể cập nhật sản phẩm.");
            }

            return Ok(updatedProduct); // Trả về sản phẩm đã cập nhật
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult>? DeleteProduct(Guid id)
        {
            var CheckForRelations = await ProductDetailRepo.GetProductDetailByProductID(id);
            if (CheckForRelations != null)
            {
                await ProductDetailRepo.DeleteExisting(CheckForRelations.ProductDetailID);
            }
            await _productRepository.Delete(id);
            return NoContent();
        }
        [HttpGet("check-name/{name}")]
        public async Task<IActionResult> CheckProductName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Tên sản phẩm không được để trống.");
            }

            var existingProduct = await _productRepository.CheckProductNameAsync(name);
            if (existingProduct != null)
            {
                return Conflict("Tên sản phẩm đã tồn tại.");
            }

            return Ok(); // Nếu không có sản phẩm trùng tên
        }
    }
}
