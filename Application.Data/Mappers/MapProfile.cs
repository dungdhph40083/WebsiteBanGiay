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

            // khi cần dùng mapper thì phải khai báo mới cái "sơ đồ ghép" cho mỗi thằng
            // cần hợp nhất hoặc trích xuất dữ liệu ra

            CreateMap<ProductDetail, ProductDetailDTO>();
            CreateMap<ProductDetailDTO, ProductDetail>();
            
            CreateMap<ProductWarranty, ProductWarrantyDTO>();
            CreateMap<ProductWarrantyDTO, ProductWarranty>();
            
            CreateMap<Rating, RatingsDTO>();
            CreateMap<RatingsDTO, Rating>();
            
            CreateMap<Return, ReturnDTO>();
            CreateMap<ReturnDTO, Return>();

            // PLEASE use automapper it literally decreases your muscle work by like 83%
        }
    }
}
