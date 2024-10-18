using Application.API.Service;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IAllRepositories<Role> _allrepo;
        GiayDBContext _context= new GiayDBContext();
        public RoleController()
        {
            _allrepo = new AllRepositroies<Role>(_context, _context.Roles);
        }
        [HttpGet]
        public IEnumerable<Role> GetAllItem()
        {
            return _allrepo.GetAllItems();
        }

        [HttpGet("{id}")]
        public Role Get(Guid id)
        {
            return _allrepo.GetAllItems().FirstOrDefault(c => c.RoleID == id);
        }
        [HttpPost("CreateRole")]
        public bool Create(Role obj)
        {
            Role Item = new Role();
            Item.RoleID = Guid.NewGuid();
            Item.RoleCode = obj.RoleCode;
            Item.RoleName = obj.RoleName;
            return _allrepo.CreateItem(Item);
        }
        [HttpPut("UpdateRole")]
        public bool UpdateRole(Role obj)
        {
            Role Item = _allrepo.GetAllItems().FirstOrDefault(c => c.RoleID == obj.RoleID);
            Item.RoleCode = obj.RoleCode;
            Item.RoleName = obj.RoleName;
            return _allrepo.UpdateItem(Item);
        }
        [HttpDelete("DeleteRole")]
        public bool Delete(Guid id)
        {
            Role Item = _allrepo.GetAllItems().FirstOrDefault(c => c.RoleID == id);
            return _allrepo.DeleteItem(Item);
        }
    }
}
