using Application.API.Service;
using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageDTO>>> GetImages()
        {
            var images = await _imageRepository.GetAllImagesAsync();
            return Ok(images);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ImageDTO>> GetImage(Guid id)
        {
            var image = await _imageRepository.GetImageByIdAsync(id);
            if (image == null) return NotFound();
            return Ok(image);
        }

        [HttpPost]
        public async Task<ActionResult<ImageDTO>> CreateImage([FromForm] ImageDTO imageDto, IFormFile ImageFile)
        {
            switch (ImageUploaderValidator.ValidateImageSizeAndHeader(ImageFile, 4_194_304))
            {
                case ErrorResult.IMAGE_TOO_BIG_ERROR:
                    return BadRequest($"Tệp ảnh không được vượt quá {4_194_304 / 1_048_576}MB!");
                case ErrorResult.IMAGE_IS_BROKEN_ERROR:
                default:
                    return BadRequest("Tệp ảnh không hợp lệ!");
                case SuccessResult.IMAGE_OK:
                    {
                        var CreatedImage = await _imageRepository.CreateImageAsync(imageDto, ImageFile);
                        return CreatedAtAction(nameof(GetImage), new { id = CreatedImage.ImageID }, CreatedImage);
                    }

            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateImage(Guid id, [FromBody] ImageDTO imageDto)
        {
            var result = await _imageRepository.UpdateImageAsync(id, imageDto);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(Guid id)
        {
            var result = await _imageRepository.DeleteImageAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
