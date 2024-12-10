using Application.API.Service;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Application.Data.Repositories.IRepository;
using Application.Data.Repositories;
using Application.Data.DTOs;
using Application.Data.Enums;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IProductDetail ProductDetailRepo;
        private readonly IImageRepository ImageRepository;
        public ProductDetailsController(IProductDetail ProductDetailRepo, IImageRepository ImageRepository)
        {
            this.ProductDetailRepo = ProductDetailRepo;
            this.ImageRepository = ImageRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDetail>>> Get()
        {
            return await ProductDetailRepo.GetProductDetails();
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<ProductDetail?>> Get(Guid ID)
        {
            return await ProductDetailRepo.GetProductDetailByID(ID);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDetail>> Post([FromForm] ProductDetailDTO NewProductDetail, IFormFile? Image)
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
                            var CreatedImage = await ImageRepository.CreateImageAsync(Image);
                            NewProductDetail.ImageID = CreatedImage.ImageID;
                        }
                        break;
                }
            }
            var Response = await ProductDetailRepo.CreateNew(NewProductDetail);
            return CreatedAtAction(nameof(Get), new { Response.ProductDetailID }, Response);
        }

        [HttpPut("{ID}")]
        public async Task<ActionResult<ProductDetail?>> Put(Guid ID, [FromForm] ProductDetailDTO UpdatedProductDetail, IFormFile? Image)
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
                            var CreatedImage = await ImageRepository.CreateImageAsync(Image);
                            UpdatedProductDetail.ImageID = CreatedImage.ImageID;
                        }
                        break;
                }
            }
            return await ProductDetailRepo.UpdateExisting(ID, UpdatedProductDetail);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult> Delete(Guid ID)
        {
            await ProductDetailRepo.DeleteExisting(ID);
            return NoContent();
        }
    }
}
