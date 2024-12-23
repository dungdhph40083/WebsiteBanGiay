using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Application.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Data.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RatingsController : ControllerBase
    {
        private readonly IRatings Rating;

        public RatingsController(IRatings Rating)
        {
            this.Rating = Rating;
        }
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<List<Rating>>> Get()
        {
            return await Rating.GetProductRating();
        }
        [HttpGet("{ID}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<Rating?>> Get(Guid ID)
        {
            return await Rating.GetProductRatinglByID(ID);
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<Rating>> Post([FromBody] RatingsDTO NewRating)
        {
            var Response = await Rating.CreateNew(NewRating);
            return CreatedAtAction(nameof(Get), new { Response.RatingID }, Response);
        }

        [HttpPut("{ID}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<Rating?>> Put(Guid ID, [FromBody] RatingsDTO UpdateRating)
        {
            return await Rating.UpdateExisting(ID, UpdateRating);
        }

        [HttpDelete("{ID}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> Delete(Guid ID)
        {
            await Rating.DeleteExisting(ID);
            return NoContent();
        }
    }
}
