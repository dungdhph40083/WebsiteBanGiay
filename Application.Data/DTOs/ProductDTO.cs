﻿namespace Application.Data.DTOs
{
    public class ProductDTO
    {
        public Guid? ImageID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public long? Price { get; set; }
    }
}
