using Application.Data.DTOs;
using Application.Data.Models;
using Application.Data.Repositories;
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
        [AllowAnonymous]
        public async Task<ActionResult<List<Color>>> GetAllColors()
        {
            return Ok( await _colorRepository.GetAllColors());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetColorById(Guid id)
        {
            var color = await _colorRepository.GetColorById(id);
            if (color == null) return NotFound();
            return Ok(color);
        }

        [HttpPost]
        public async Task<ActionResult> CreateColor(ColorDTO colorDTO)
        {
            var Check = await _colorRepository.ColorNameAvailibility(colorDTO.ColorName);
            if (Check == false) return Conflict();

            var createdColor = await _colorRepository.CreateColor(colorDTO);
            return CreatedAtAction(nameof(GetColorById), new { id = createdColor.ColorID }, createdColor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateColor(Guid id, ColorDTO colorDTO)
        {
            var OldColor = await _colorRepository.GetColorById(id);
            if (!string.Equals(OldColor?.ColorName, colorDTO.ColorName, StringComparison.OrdinalIgnoreCase))
            {
                var Check = await _colorRepository.ColorNameAvailibility(colorDTO.ColorName);
                if (Check == false) return Conflict();
            }

            await _colorRepository.UpdateColor(id, colorDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColor(Guid id)
        {
            try
            {
                await _colorRepository.DeleteColor(id);
                return NoContent();
            }
            catch (Exception)
            {
                var color = await _colorRepository.GetColorById(id);
                if (color == null) return NoContent();
                else return Conflict();
            }
        }
    }
}
