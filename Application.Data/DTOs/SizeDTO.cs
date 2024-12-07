using Application.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Data.DTOs
{
    public class SizeDTO
    {
        public string? Name { get; set; }
        public byte? Status { get; set; }
    }
}
