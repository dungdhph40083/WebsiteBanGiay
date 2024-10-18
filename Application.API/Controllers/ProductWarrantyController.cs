using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWarrantyController : ControllerBase
    {
        private readonly IAllRepositories<ProductWarranty> _allrepo;
        GiayDBContext _context = new GiayDBContext();

        public ProductWarrantyController()
        {
            _allrepo = new AllRepositroies<ProductWarranty>(_context, _context.ProductWarranties);
        }
        [HttpGet]
        public IEnumerable<ProductWarranty> GetAllItem()
        {
            return _allrepo.GetAllItems();
        }
        //[HttpPost("Create")]
        //public bool Create(ProductWarranty item)
        //{

        //    ProductWarranty Item = new ProductWarranty();
        //    Item.WarrantyID = item.WarrantyID;
        //    Item.WarrantyPeriod = DateTime.Now;
        //    Item.WarrantyConditions = item.WarrantyConditions;
        //    Item.CreatedAt = DateTime.Now;
        //    return _allrepo.CreateItem(Item);
        //}
        //[HttpPut("Update")]
        //public bool Post(ProductWarranty item)
        //{
        //    ProductWarranty Item = _allrepo.GetAllItems().FirstOrDefault(c => c.WarrantyID == item.WarrantyID);
        //    Item.WarrantyPeriod = DateTime.Now;
        //    Item.WarrantyConditions = item.WarrantyConditions;
        //    Item.UpdatedAt = DateTime.Now;

        //    return _allrepo.UpdateItem(Item);
        //    //
        //}

        [HttpDelete("Delete")]
        public bool Delete(Guid id)
        {
            ProductWarranty Item = _allrepo.GetAllItems().FirstOrDefault(c => c.WarrantyID == id);
            return _allrepo.DeleteItem(Item);
        }
    }
}
