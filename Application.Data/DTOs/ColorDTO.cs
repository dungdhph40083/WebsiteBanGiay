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
        public string? ColorName { get; set; }
        public byte Status { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; set; }
    }
}
