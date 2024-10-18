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
    public class ShippingMethodController : ControllerBase
    {
        private readonly IAllRepositories<ShippingMethod> _allrepo;
        GiayDBContext _context = new GiayDBContext();
        public ShippingMethodController()
        {
            _allrepo = new AllRepositroies<ShippingMethod>(_context, _context.ShippingMethods);
        }
        [HttpGet]
        public IEnumerable<ShippingMethod> GetAllItem()
        {
            return _allrepo.GetAllItems();
        }
        [HttpPost("Create")]
        public bool Create(ShippingMethod obj)
        {
            ShippingMethod Item = new ShippingMethod();
            Item.ShippingMethodID = Guid.NewGuid();
            Item.MethodName = obj.MethodName;
            Item.ShippingFee = obj.ShippingFee;
            Item.EstimatedDeliveryTime = DateTime.Now;
            return _allrepo.CreateItem(Item);
        }
        [HttpPut("Update")]
        public bool Update(ShippingMethod obj)
        {
            ShippingMethod Item = _allrepo.GetAllItems().FirstOrDefault(c => c.ShippingMethodID == obj.ShippingMethodID);
            Item.MethodName = obj.MethodName;
            Item.ShippingFee = obj.ShippingFee;
            return _allrepo.UpdateItem(Item);
        }
        [HttpDelete("Delete")]
        public bool Delete(Guid id)
        {
            ShippingMethod Item = _allrepo.GetAllItems().FirstOrDefault(c => c.ShippingMethodID == id);
            return _allrepo.DeleteItem(Item);
        }
    }
}
