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

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IProductDetail ProductDetailRepo;
        public ProductDetailsController(IProductDetail ProductDetailRepo)
        {
            this.ProductDetailRepo = ProductDetailRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDetail>>> Get()
        {
            return await ProductDetailRepo.GetProductDetails();
        }

        [HttpGet("getbyId")]
        public async Task<ActionResult<ProductDetail?>> Get(Guid ID)
        {
            return await ProductDetailRepo.GetProductDetailByID(ID);
        }

        [HttpPost("create")]
        public async Task<ActionResult<ProductDetail>> Post([FromBody] ProductDetailDTO NewProductDetail)
        {
            return await ProductDetailRepo.CreateNew(NewProductDetail);
        }

        [HttpPut("update")]
        public async Task<ActionResult<ProductDetail?>> Put(Guid ID, [FromBody] ProductDetailDTO UpdateProductDetail)
        {
            return await ProductDetailRepo.UpdateExisting(ID, UpdateProductDetail);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(Guid ID)
        {
            await ProductDetailRepo.DeleteExisting(ID);
            return Ok();
        }
    }
}
