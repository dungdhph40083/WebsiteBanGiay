using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.DTOs
{
    public class ColorDTO
    {
        public Guid ColorID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên màu")]
        [StringLength(50, ErrorMessage = "Tên màu không được dài hơn 50 ký tự.")]
        public string? ColorName { get; set; }
        [Range(0, 1, ErrorMessage = "Vui lòng chọn trạng thái")]
        public byte? Status { get; set; }
    }
}
