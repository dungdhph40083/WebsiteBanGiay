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
    public class RatingsController : ControllerBase
    {
        private readonly IAllRepositories<Rating> _allrepo;
        GiayDBContext _context = new GiayDBContext();
        public RatingsController()
        {
            _allrepo = new AllRepositroies<Rating>(_context, _context.Ratings);
        }
        [HttpGet]
        public IEnumerable<Rating> GetAllItem()
        {
            return _allrepo.GetAllItems();
        }

        [HttpGet("{id}")]
        public Rating Get(Guid id)
        {
            return _allrepo.GetAllItems().FirstOrDefault(c => c.RatingID == id);
        }
        //[HttpPost("Create")]
        //public bool Create(Rating obj)
        //{
        //    Rating Item = new Rating();
        //    Item.RatingID = Guid.NewGuid();
        //    Item.UserID = obj.UserID;
        //    Item.ProductID = obj.ProductID;
        //    Item.Stars = obj.Stars;
        //    Item.Comment = obj.Comment;
        //    Item.DateRated = DateTime.Now;
        //    return _allrepo.CreateItem(Item);
        //}
    }
}
