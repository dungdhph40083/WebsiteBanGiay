using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCart ShoppingCartRepo;
        private readonly IVoucher VoucherRepo;
        public ShoppingCartController(IShoppingCart ShoppingCartRepo, IVoucher VoucherRepo)
        {
            this.ShoppingCartRepo = ShoppingCartRepo;
            this.VoucherRepo = VoucherRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<ShoppingCart>>> Get()
        {
            return await ShoppingCartRepo.GetShoppingCarts(); 
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<ShoppingCart?>> Get(Guid ID)
        {
            return await ShoppingCartRepo.GetShoppingCartByID(ID);
        }

        [HttpGet("User/{ID}")]
        public async Task<ActionResult<List<ShoppingCart>>> GetUser(Guid ID)
        {
            return await ShoppingCartRepo.GetShoppingCartsByUserID(ID);
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> Post([FromBody] ShoppingCartDTO NewShoppingCart)
        {
            var Response = await ShoppingCartRepo.CreateNew(NewShoppingCart);
            return CreatedAtAction(nameof(Get), new { ID = Response.CartID }, Response);
        }

        [HttpPut("{ID}")]
        public async Task<ActionResult<ShoppingCart?>> Put(Guid ID, [FromBody] ShoppingCartDTO UpdatedShoppingCart)
        {
            return await ShoppingCartRepo.Update(ID, UpdatedShoppingCart);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult> Delete(Guid ID)
        {
            await ShoppingCartRepo.Delete(ID);
            return NoContent();
        }

        [HttpPut("Add2Cart/{UserID}/{ProductDetailID}")]
        public async Task<ActionResult<ShoppingCart?>> Add2Cart(Guid UserID, Guid ProductDetailID, int? Quantity, bool? AdditionMode)
        {
            var Response = await ShoppingCartRepo.Add2Cart(UserID, ProductDetailID, Quantity, AdditionMode);
            if (Response != null) return await Get(Response.CartID);
            else return NoContent();
        }

        [HttpPatch("ApplyVoucher/{UserID}/{ProductDetailID}")]
        public async Task<ActionResult<ShoppingCart?>> ApplyVoucher(Guid UserID, Guid ProductDetailID, string VoucherCode)
        {
            var Validator = await VoucherRepo.VoucherValidator(VoucherCode);
            if (Validator == ValidateErrorResult.VOUCHER_VALID)
            {
                var Response = await ShoppingCartRepo.ApplyVoucher(UserID, ProductDetailID, VoucherCode);
                if (Response != null) return Response;
                else return NoContent();
            }
            return ValidationProblem(Validator);
        }

        [HttpPatch("UnapplyVoucher/{UserID}/{ProductDetailID}")]
        public async Task<ActionResult<ShoppingCart?>> UnapplyVoucher(Guid UserID, Guid ProductDetailID)
        {
            var Response = await ShoppingCartRepo.UnapplyVoucher(UserID, ProductDetailID);
            if (Response != null) return Response;
            else return NoContent();
        }
    }
}
