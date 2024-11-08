using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.DTOs
{
    public class ImageDTO
    {
        public Guid ImageID { get; set; }
        public string? ImageName { get; set; }
        public string? ImageDescription { get; set; }
        public string? ImageFileName { get; set; }
        public byte Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
