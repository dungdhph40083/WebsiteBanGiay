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
using Newtonsoft.Json;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IProductDetail ProductDetailRepo;
        private readonly IOrderDetails OrderDetailRepository;
        private readonly IShoppingCart ShoppingCartRepository;
        public ProductDetailsController(IProductDetail ProductDetailRepo, IOrderDetails OrderDetailRepository, IShoppingCart ShoppingCartRepository)
        {
            this.ProductDetailRepo = ProductDetailRepo;
            this.OrderDetailRepository = OrderDetailRepository;
            this.ShoppingCartRepository = ShoppingCartRepository;
        }

        // Để debug
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<ProductDetail>>> Get()
        {
            return await ProductDetailRepo.GetProductDetails();
        }

        [HttpGet("{ID}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductDetail?>> GetByDetailID(Guid ID)
        {
            return await ProductDetailRepo.GetProductDetailByID(ID);
        }

        [HttpGet("ByProduct/{ProductID}")]
        public async Task<ActionResult<List<ProductDetail>>> GetByProductID(Guid ProductID)
        {
            return await ProductDetailRepo.GetProductDetailsByProductID(ProductID);
        }

        [HttpGet("ByProduct/VariationsOnly")]
        public async Task<ActionResult<List<ProductDetail>>> GetVariations(Guid? ProductID)
        {
            return await ProductDetailRepo.GetVariationsByProductID(ProductID.GetValueOrDefault());
        }

        [HttpPost]
        public async Task<ActionResult<ProductDetail>> Post([FromBody] ProductDetailDTO NewProductDetail)
        {
            var Response = await ProductDetailRepo.CreateNew(NewProductDetail);
            return CreatedAtAction(nameof(Get), new { Response.ProductDetailID }, Response);
        }

        [HttpPost("AddVariations")]
        public async Task<ActionResult<ProductDetailVariationMetadata>> PostVariations([FromBody] ProductDetailMultiDTO Variations)
        {
            try
            {
                var Response = await ProductDetailRepo.CreateNewVariations(Variations);
                return Response;
            }
            catch (Exception)
            {
                return Conflict(Response);
            }
        }

        [HttpPut("{ID}")]
        public async Task<ActionResult<ProductDetail?>> Put(Guid ID, [FromBody] ProductDetailDTO UpdatedProductDetail)
        {
            return await ProductDetailRepo.UpdateExisting(ID, UpdatedProductDetail);
        }

        [HttpPut("UpdateVariations/{ID}")]
        public async Task<ActionResult<bool?>> PutVariations(Guid ID, [FromBody] ProductDetailMultiDTO NewVariations)
        {
            return await ProductDetailRepo.UpdateVariations(ID, NewVariations);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult> Delete(Guid ID)
        {
            try
            {
                await ProductDetailRepo.DeleteExisting(ID);
                return NoContent();
            }
            catch (Exception)
            {
                var Target = await ProductDetailRepo.GetProductDetailByID(ID);
                if (Target == null) return NoContent();
                else return Conflict();
            }
        }

        [HttpDelete("ByProduct/{ProductID}")]
        public async Task<ActionResult> DeleteAllByProduct(Guid ProductID)
        {
            bool DetectedDetailsInUse = false;
            var Items = await ProductDetailRepo.GetProductDetailsByProductID(ProductID);
            foreach (var Item in Items)
            {
                await ShoppingCartRepository.DeleteShoppingCartsByDetailID(Item.ProductDetailID);

                if ((await OrderDetailRepository.GetDetailsByProductDetailID(Item.ProductDetailID)).Count > 0)
                {
                    DetectedDetailsInUse = true;
                    await ProductDetailRepo.UpdateSetToZero(Item.ProductDetailID);
                }
                else await ProductDetailRepo.DeleteExisting(Item.ProductDetailID);
            }
            if (DetectedDetailsInUse) { Console.WriteLine("\n409\n"); return Conflict(); }
            else { Console.WriteLine("\n204\n"); return NoContent(); }
        }

        // Phương thức để chuyển trạng thái sản phẩm giữa "mở bán" và "dừng bán"
        [HttpPut("{ProductID}/ToggleStatus")]
        public async Task<ActionResult> ToggleStatus(Guid ProductID)
        {
            return Ok(await ProductDetailRepo.UpdateStatusOnly(ProductID));
        }
    }
}
