using Application.Data.DTOs;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorRepository _colorRepository;

        public ColorController(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<List<Color>>> GetAllColors()
        {
            return Ok( await _colorRepository.GetAllColors());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> GetColorById(Guid id)
        {
            var color = await _colorRepository.GetColorById(id);
            if (color == null) return NotFound();
            return Ok(color);
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> CreateColor(ColorDTO colorDTO)
        {
            var createdColor = await _colorRepository.CreateColor(colorDTO);
            return CreatedAtAction(nameof(GetColorById), new { id = createdColor.ColorID }, createdColor);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> UpdateColor(Guid id, ColorDTO colorDTO)
        {
            await _colorRepository.UpdateColor(id, colorDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteColor(Guid id)
        {
            await _colorRepository.DeleteColor(id);
            return NoContent();
        }
    }
}
