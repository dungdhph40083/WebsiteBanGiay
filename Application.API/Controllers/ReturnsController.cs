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
    public class ReturnsController : ControllerBase
    {
        private readonly IAllRepositories<Return> _allrepo;
        GiayDBContext _context = new GiayDBContext();
        public ReturnsController()
        {
            _allrepo = new AllRepositroies<Return>(_context, _context.Returns);
        }
        [HttpGet]
        public IEnumerable<Return> GetAllItem()
        {
            return _allrepo.GetAllItems();
        }

        [HttpGet("{id}")]
        public Return Get(Guid id)
        {
            return _allrepo.GetAllItems().FirstOrDefault(c => c.ReturnID == id);
        }
    }
}
