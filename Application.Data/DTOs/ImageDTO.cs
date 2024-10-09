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
        public byte[]? ImageData { get; set; }
        public byte Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
