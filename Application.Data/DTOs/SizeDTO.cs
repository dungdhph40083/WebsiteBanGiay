﻿using Application.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Data.DTOs
{
    public class SizeDTO
    {
        public Guid SizeID { get; set; }
        public string? Name { get; set; }
        [Required(ErrorMessage = "Invalid.")]
        public byte Status { get; set; }
    }
}
