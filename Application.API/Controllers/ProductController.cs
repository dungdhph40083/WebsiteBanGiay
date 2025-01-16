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
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Product?>>> GetProducts()
        {
            return await _productRepository.GetAll();
        }
        
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Product?>> GetProduct(Guid id)
        {
            return await _productRepository.GetById(id);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Product>> PostProduct([FromForm] ProductDTO product, List<IFormFile>? Image)
        {
            // Kiểm tra tên sản phẩm xem đã tồn tại trong cơ sở dữ liệu chưa
            var existingProduct = await _productRepository.CheckProductNameAsync(product.Name);
            if (existingProduct != null)
            {
                return Conflict("Tên sản phẩm đã tồn tại!");  // Nếu tên sản phẩm đã có, trả về lỗi
            }

            // Thêm sản phẩm vào cơ sở dữ liệu
            var response = await _productRepository.Add(product);

            // Kiểm tra và xử lý ảnh nếu có
            if (Image != null && Image.Count != 0)
            {
                int FailedUploadsBczTooBig = 0;
                int FailedUploadsBczBroken = 0;

                foreach (var ImgItem in Image)
                {
                    switch (ImageUploaderValidator.ValidateImageSizeAndHeader(ImgItem, 4_194_304))  // Kiểm tra kích thước và định dạng ảnh
                    {
                        case ErrorResult.IMAGE_TOO_BIG_ERROR:
                            {
                                FailedUploadsBczTooBig++;
                                Image.Remove(ImgItem);
                                break;
                            }
                        case ErrorResult.IMAGE_IS_BROKEN_ERROR:
                        default:
                            {
                                FailedUploadsBczBroken++;
                                Image.Remove(ImgItem);
                                break;
                            }
                        case SuccessResult.IMAGE_OK:
                            // ko làm gì cả
                            break;
                    }
                }
                await _imageRepository.CreateShittonOfImagesWithProductIDAsync(response.ProductID, Image);
            }

            // Trả về phản hồi sau khi thêm sản phẩm thành công
            return CreatedAtAction(nameof(GetProduct), new { id = response.ProductID }, response);
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Product?>> PutProduct(Guid id, [FromForm] ProductDTO product, List<IFormFile>? Image)
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

            // Cập nhật sản phẩm
            var updatedProduct = await _productRepository.Update(id, product);

            if (updatedProduct == null)
            {
                return NotFound("Không thể cập nhật sản phẩm.");
            }

            // Kiểm tra và xử lý ảnh nếu có
            if (Image != null && Image.Count != 0)
            {
                int FailedUploadsBczTooBig = 0;
                int FailedUploadsBczBroken = 0;

                foreach (var ImgItem in Image)
                {
                    switch (ImageUploaderValidator.ValidateImageSizeAndHeader(ImgItem, 4_194_304))  // Kiểm tra kích thước và định dạng ảnh
                    {
                        case ErrorResult.IMAGE_TOO_BIG_ERROR:
                            {
                                FailedUploadsBczTooBig++;
                                Image.Remove(ImgItem);
                                break;
                            }
                        case ErrorResult.IMAGE_IS_BROKEN_ERROR:
                        default:
                            {
                                FailedUploadsBczBroken++;
                                Image.Remove(ImgItem);
                                break;
                            }
                        case SuccessResult.IMAGE_OK:
                            // ko làm gì cả
                            break;
                    }
                }
                await _imageRepository.CreateShittonOfImagesWithProductIDAsync(updatedProduct.ProductID, Image);
            }

            return Ok(updatedProduct); // Trả về sản phẩm đã cập nhật
        }


        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult>? DeleteProduct(Guid id)
        {
            try
            {
                await ProductDetailRepo.DeleteExistingByProductID(id);
                var Imgs = await _imageRepository.GetImagesByProductID(id);
                foreach (var Img in Imgs)
                {
                    await _imageRepository.DeleteImageAsync(Img.ImageID);
                }
                await _productRepository.Delete(id);
                return NoContent();
            }
            catch (Exception)
            {
                if (await _productRepository.GetById(id) != null)
                {
                    return Conflict();
                }
                else return NoContent();
            }
        }

        [HttpGet("CheckProductName/{name}")]
        [AllowAnonymous]
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
