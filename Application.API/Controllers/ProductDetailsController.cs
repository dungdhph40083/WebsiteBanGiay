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
using Microsoft.AspNetCore.Authorization;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [AllowAnonymous]
        public async Task<ActionResult<List<ProductDetail>>> Get()
        {
            return await ProductDetailRepo.GetProductDetails();
        }

        [HttpGet("{ID}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductDetail?>> Get(Guid ID)
        {
            return await ProductDetailRepo.GetProductDetailByID(ID);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductDetail>> Post([FromForm] ProductDetailDTO NewProductDetail)
        {
            var Response = await ProductDetailRepo.CreateNew(NewProductDetail);
            return CreatedAtAction(nameof(Get), new { Response.ProductDetailID }, Response);
        }

        [HttpPut("{ID}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductDetail?>> Put(Guid ID, [FromForm] ProductDetailDTO UpdatedProductDetail)
        {
            return await ProductDetailRepo.UpdateExisting(ID, UpdatedProductDetail);
        }

        [HttpDelete("{ID}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(Guid ID)
        {
            await ProductDetailRepo.DeleteExisting(ID);
            return NoContent();
        }
        // Phương thức để chuyển trạng thái sản phẩm giữa "mở bán" và "dừng bán"
        [HttpPut("{ID}/toggle-status")]
        public async Task<ActionResult> ToggleStatus(Guid ID)
        {
            var productDetail = await ProductDetailRepo.GetProductDetailByID(ID);
            if (productDetail == null)
            {
                return NotFound("Product detail not found");
            }

            // Đảo trạng thái của productDetail
            byte newStatus = productDetail.Status == 1 ? (byte)0 : (byte)1;

            // Cập nhật trạng thái mà không thay đổi các thuộc tính khác
            await ProductDetailRepo.UpdateStatusOnly(ID, newStatus);

            // Trả về sản phẩm đã được cập nhật
            return Ok(await ProductDetailRepo.GetProductDetailByID(ID));
        }




    }
}
