using Application.Data.DTOs;
using Application.Data.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Mappers
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<ShoppingCartDTO, ShoppingCart>();
            CreateMap<ShoppingCart, ShoppingCartDTO>();

            CreateMap<SizeDTO, Size>();
            CreateMap<Size, SizeDTO>();

            // PLEASE use automapper it literally decreases your muscle work by like 83%
        }
    }
}
