﻿using Application.API.Service;
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
                            ImageDTO AutoGeneratedImageInfo = new() { Status = 1 };
                            if (!string.IsNullOrWhiteSpace(product.Name))
                            {
                                AutoGeneratedImageInfo.ImageName = $"Ảnh hàng {product.Name}";
                                AutoGeneratedImageInfo.ImageDescription = $"{product.Description}";
                            }
                            else
                            {
                                AutoGeneratedImageInfo.ImageName = $"Ảnh hàng.";
                                AutoGeneratedImageInfo.ImageDescription = $"Ảnh hàng.";
                            }

                            var CreatedImage = await _imageRepository.CreateImageAsync(AutoGeneratedImageInfo, Image);

                            product.ImageID = CreatedImage.ImageID;
                        }
                        break;
                }
            }
            // Thêm sản phẩm vào cơ sở dữ liệu
            var Response = _productRepository.Add(product);
            return CreatedAtAction("GetProduct", new { id = Response.ProductID }, Response);
        }

        [HttpPut("update-product/{id}")]
        public async Task<IActionResult> PutProduct(Guid id, [FromForm] ProductDTO product, IFormFile? Image)
        {

            if (product == null)
            {
                return BadRequest("Product ID mismatch or product is null.");
            }

            try
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
                                ImageDTO AutoGeneratedImageInfo = new() { Status = 1 };
                                if (!string.IsNullOrWhiteSpace(product.Name))
                                {
                                    AutoGeneratedImageInfo.ImageName = $"Ảnh hàng {product.Name}";
                                    AutoGeneratedImageInfo.ImageDescription = $"{product.Description}";
                                }
                                else
                                {
                                    AutoGeneratedImageInfo.ImageName = $"Ảnh hàng.";
                                    AutoGeneratedImageInfo.ImageDescription = $"Ảnh hàng.";
                                }

                                var CreatedImage = await _imageRepository.CreateImageAsync(AutoGeneratedImageInfo, Image);

                                product.ImageID = CreatedImage.ImageID;
                            }
                            break;
                    }
                }

                var Response = _productRepository.Update(id, product);
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
            return NoContent();
        }
    }
}
