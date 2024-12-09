using Application.API.Service;
using Application.Data.DTOs;
using Application.Data.Enums;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _productRepository.GetAll();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Product?>> GetProduct(Guid id)
        {
            return await _productRepository.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromForm] ProductDTO product, IFormFile? Image)
        {
            if (Image != null)
            {
                switch (ImageUploaderValidator.ValidateImageSizeAndHeader(Image, 4_194_304))
                {
                    case ErrorResult.IMAGE_TOO_BIG_ERROR:
                        return BadRequest($"Tệp ảnh không được vượt quá {4_194_304 / 1_048_576}MB!");
                    case ErrorResult.IMAGE_IS_BROKEN_ERROR:
                    default:
                        return BadRequest("Tệp ảnh không hợp lệ!");
                    case SuccessResult.IMAGE_OK:
                        {
                            var CreatedImage = await _imageRepository.CreateImageAsync(Image);
                            product.ImageID = CreatedImage.ImageID;
                        }
                        break;
                }
            }
            // Thêm sản phẩm vào cơ sở dữ liệu
            var Response = await _productRepository.Add(product);
            return CreatedAtAction(nameof(GetProduct), new { id = Response.ProductID }, Response);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Product?>> PutProduct(Guid id, [FromForm] ProductDTO product, IFormFile? Image)
        {
            // Update product information in the database

            if (Image != null)
            {
                switch (ImageUploaderValidator.ValidateImageSizeAndHeader(Image, 4_194_304))
                {
                    case ErrorResult.IMAGE_TOO_BIG_ERROR:
                        return BadRequest($"Tệp ảnh không được vượt quá {4_194_304 / 1_048_576}MB!");
                    case ErrorResult.IMAGE_IS_BROKEN_ERROR:
                    default:
                        return BadRequest("Tệp ảnh không hợp lệ!");
                    case SuccessResult.IMAGE_OK:
                        {
                            var CreatedImage = await _imageRepository.CreateImageAsync(Image);
                            product.ImageID = CreatedImage.ImageID;
                        }
                        break;
                }
            }

            return await _productRepository.Update(id, product);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            _productRepository.Delete(id);
            return NoContent();
        }
    }
}
