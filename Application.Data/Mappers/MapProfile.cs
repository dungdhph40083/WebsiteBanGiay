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
            // khi cần dùng mapper thì phải khai báo mới cái "sơ đồ ghép" cho mỗi thằng
            // cần hợp nhất hoặc trích xuất dữ liệu ra

            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();

            CreateMap<CustomerSupportMessage, CustomerSupportMessageDTO>();
            CreateMap<CustomerSupportMessageDTO, CustomerSupportMessage>();

            CreateMap<Image, ImageDTO>();
            CreateMap<ImageDTO, Image>();

            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();

            CreateMap<OrderDetail, OrderDetailDto>();
            CreateMap<OrderDetailDto, OrderDetail>();

            CreateMap<OrderTracking, OrderTrackingDTO>();
            CreateMap<OrderTrackingDTO, OrderTracking>();

            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();

            CreateMap<ProductDetail, ProductDetailDTO>();
            CreateMap<ProductDetailDTO, ProductDetail>();

            CreateMap<ProductWarranty, ProductWarrantyDTO>();
            CreateMap<ProductWarrantyDTO, ProductWarranty>();

            CreateMap<PaymentMethod, PaymentMethodDTO>();
            CreateMap<PaymentMethodDTO, PaymentMethod>();

            CreateMap<PaymentMethodDetail, PaymentMethodDetailDTO>();
            CreateMap<PaymentMethodDetailDTO, PaymentMethodDetail>();

            CreateMap<Rating, RatingsDTO>();
            CreateMap<RatingsDTO, Rating>();

            CreateMap<Return, ReturnDTO>();
            CreateMap<ReturnDTO, Return>();

            CreateMap<ShoppingCartDTO, ShoppingCart>();
            CreateMap<ShoppingCart, ShoppingCartDTO>();

            CreateMap<SizeDTO, Size>();
            CreateMap<Size, SizeDTO>();
            
            CreateMap<Sale, SaleDTO>();
            CreateMap<SaleDTO, Sale>();
            
            CreateMap<ShippingMethod, ShippingMethodDTO>();
            CreateMap<ShippingMethodDTO, ShippingMethod>();
            
            CreateMap<Role, RoleDTO>();
            CreateMap<RoleDTO, Role>();

            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<Voucher, VoucherDTO>();
            CreateMap<VoucherDTO, Voucher>();
        }
    }
}
