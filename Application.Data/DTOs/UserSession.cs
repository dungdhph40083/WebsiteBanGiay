using Application.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Application.Data.Enums;

namespace Application.Data.DTOs
{
    public class UserSession
    {
        public Guid? UserID { get; set; }
        public string Username { get; set; } = null!;
        public string? JWToken { get; set; } // Use later....
    }
}
