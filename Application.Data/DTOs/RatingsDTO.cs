using Application.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.DTOs
{
    public class RatingsDTO
    {
        public Guid? UserID { get; set; }
        public Guid? ProductID { get; set; }
        public decimal? Stars { get; set; }
        public string? Comment { get; set; }
        public DateTime? DateRated { get; set; }
    }
}
