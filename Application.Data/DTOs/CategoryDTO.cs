using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Data.DTOs
{
    public class CategoryDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
        [StringLength(100, ErrorMessage = "Tên danh mục không được dài quá 100 ký tự.")]
        public string? CategoryName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        [StringLength(500, ErrorMessage = "Mô tả không được dài quá 500 ký tự.")]
        public string? Description { get; set; }
    }
}
