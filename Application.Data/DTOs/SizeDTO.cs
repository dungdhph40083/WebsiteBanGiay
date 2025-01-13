using Application.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Data.DTOs
{
    public class SizeDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập Size")]
        public string? Name { get; set; }
        [Range(0, 1, ErrorMessage = "Vui lòng chọn trạng thái")]
        public byte? Status { get; set; }
    }
}
