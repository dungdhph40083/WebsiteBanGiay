using Application.Data.Models;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productRepository;

        public ProductController(IProduct productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(_productRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(Guid id)
        {
            var product = _productRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public ActionResult<Product> PostProduct(Product product)
        {
            _productRepository.Add(product);
            _productRepository.Save();
            return CreatedAtAction("GetProduct", new { id = product.ProductID }, product);
        }

        [HttpPut("{id}")]
        public IActionResult PutProduct(Guid id, Product product)
        {
            if (id != product.ProductID)
            {
                return BadRequest();
            }

            _productRepository.Update(product);
            _productRepository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            _productRepository.Delete(id);
            _productRepository.Save();
            return NoContent();
        }
    }
}
