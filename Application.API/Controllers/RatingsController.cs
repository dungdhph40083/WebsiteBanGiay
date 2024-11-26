using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Application.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Data.DTOs;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRatings Rating;

        public RatingsController(IRatings Rating)
        {
            this.Rating = Rating;
        }
        [HttpGet]
        public async Task<ActionResult<List<Rating>>> Get()
        {
            return await Rating.GetProductRating();
        }
        [HttpGet("getbyId")]
        public async Task<ActionResult<Rating?>> Get(Guid ID)
        {
            return await Rating.GetProductRatinglByID(ID);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Rating>> Post([FromBody] RatingsDTO NewRating)
        {
            return await Rating.CreateNew(NewRating);
        }

        [HttpPut("update")]
        public async Task<ActionResult<Rating?>> Put(Guid ID, [FromBody] RatingsDTO UpdateRating)
        {
            return await Rating.UpdateExisting(ID, UpdateRating);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(Guid ID)
        {
            await Rating.DeleteExisting(ID);
            return Ok();
        }
    }
}
