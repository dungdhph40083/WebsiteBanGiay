using Application.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Data.DTOs
{
    public class ImageDTO
    {
        public string? ImageName { get; set; }
        public string? ImageDescription { get; set; }
        [Required(ErrorMessage = "Required.")]
        public byte Status { get; set; }
    }
}
