using Application.Data.DTOs;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCart ShoppingCartRepo;
        public ShoppingCartController(IShoppingCart ShoppingCartRepo)
        {
            this.ShoppingCartRepo = ShoppingCartRepo;
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

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> Post([FromBody] ShoppingCartDTO NewShoppingCart)
        {
            return await ShoppingCartRepo.CreateNew(NewShoppingCart);
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
            return Ok();
        }
    }
}
