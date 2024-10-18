using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Application.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly IAllRepositories<Sale> _allrepo;
        GiayDBContext _context = new GiayDBContext();

        public SaleController()
        {
            _allrepo = new AllRepositroies<Sale>(_context, _context.Sales);
        }
        [HttpGet]
        public IEnumerable<Sale> GetAllItem()
        {
            return _allrepo.GetAllItems();
        }
        [HttpGet("{id}")]
        public Sale Get(Guid id)
        {
            return _allrepo.GetAllItems().FirstOrDefault(c => c.SaleID == id);
        }
        [HttpPost("Create")]
        public bool Create(Sale obj)
        {
            Sale Item = new Sale();
            Item.SaleID = Guid.NewGuid();
            Item.Name = obj.Name;
            Item.SaleCode = obj.SaleCode;
            Item.Status = obj.Status;
            Item.CreatedAt = obj.CreatedAt;
            Item.UpdatedAt = obj.UpdatedAt;
            Item.ProductID = obj.ProductID;
            return _allrepo.CreateItem(Item);
        }
        [HttpPut("Update")]
        public bool Update(Sale obj)
        {
            Sale Item = _allrepo.GetAllItems().FirstOrDefault(c => c.SaleID == obj.SaleID);
            Item.Name = obj.Name;
            Item.SaleCode = obj.SaleCode;
            Item.Status = obj.Status;
            Item.CreatedAt = obj.CreatedAt;
            Item.UpdatedAt = obj.UpdatedAt;
            Item.ProductID = obj.ProductID;
            return _allrepo.UpdateItem(Item);
        }
        [HttpDelete("Delete")]
        public bool Delete(Guid id)
        {
            Sale Item = _allrepo.GetAllItems().FirstOrDefault(c => c.SaleID == id);
            return _allrepo.DeleteItem(Item);
        }
    }
}
