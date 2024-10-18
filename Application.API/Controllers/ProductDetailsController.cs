using Application.API.Service;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Application.Data.Repositories.IRepository;
using Application.Data.Repositories;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IAllRepositories<ProductDetail> _allrepo;
        GiayDBContext _context = new GiayDBContext();
        public ProductDetailsController()
        {
            _allrepo = new AllRepositroies<ProductDetail>(_context, _context.ProductDetails);
        }
        [HttpGet]
        public IEnumerable<ProductDetail> GetAllItem()
        {
            return _allrepo.GetAllItems();
        }

        [HttpGet("{id}")]
        public ProductDetail Get(Guid id)
        {
            return _allrepo.GetAllItems().FirstOrDefault(c => c.ProductDetailID == id);
        }
        //[HttpPost("Create")]
        //public bool Create(ProductDetail obj)
        //{
        //    return _allrepo.CreateItem(obj);
        //}
        //[Route("Update")]
        //[HttpPut]
        //public bool Update(ProductDetail obj)
        //{
        //    ProductDetail item = _allrepo.GetAllItems().FirstOrDefault(c => c.ProductDetailID == obj.ProductDetailID);
        //    item.ProductID = obj.ProductID;
        //    item.Material = obj.Material;
        //    item.Quantity = obj.Quantity;
        //    item.Price = obj.Price;
        //    item.Brand = obj.Brand;
        //    item.PlaceOfOrigin = obj.PlaceOfOrigin;
        //    item.Type = obj.Type;
        //    item.WarrantyPeriod = obj.WarrantyPeriod;
        //    item.Instructions = obj.Instructions;
        //    item.Features = obj.Features;
        //    //item.ImageID = obj.Image;
        //    return _allrepo.UpdateItem(item);
        //}
        [HttpDelete("Delete")]
        public bool Delete(Guid id)
        {
            ProductDetail Item = _allrepo.GetAllItems().FirstOrDefault(c => c.ProductDetailID == id);
            return _allrepo.DeleteItem(Item);
        }
    }
}
