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

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
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
    }
}
