using Application.API.Models;
using Application.Data.Models;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoadDataController : ControllerBase
    {
        private readonly IColorRepository _colorRepository;
        private readonly ISize _sizeRepository;
        private readonly IProduct _productRepository;

        public LoadDataController(IColorRepository colorRepository
            , ISize sizeRepository
            , IProduct productRepository)
        {
            _colorRepository = colorRepository;
            _sizeRepository = sizeRepository;
            _productRepository = productRepository;
        }

        //Lấy danh sách màu sắc
        [HttpPost("GetColor")]
        public ActionResult<List<Color>> GetColor(DataSizeColor dataSizeColor)
        {
            if (dataSizeColor == null 
                || dataSizeColor.productId == null 
                || dataSizeColor.productId == ""
                || dataSizeColor.productId == "string")
                return BadRequest();
            Guid productId = Guid.Parse(dataSizeColor.productId.ToString());
            if (_productRepository.GetById(productId) == null)
                return NotFound();
            if(dataSizeColor.data == null || dataSizeColor.data == "" || dataSizeColor.data == "string")
            {
                return Ok(_colorRepository.GetColorsByProductId(productId));
            }
            else
            {
                Guid size = Guid.Parse(dataSizeColor.data.ToString());
                return Ok(_colorRepository.GetColorsByProductIdAndSize(productId, size));
            }
        }

        //Lấy danh sách size
        [HttpPost("GetSize")]
        public ActionResult<List<Size>> GetSize(DataSizeColor dataSizeColor)
        {
            if (dataSizeColor == null
                || dataSizeColor.productId == null
                || dataSizeColor.productId == ""
                || dataSizeColor.productId == "string")
                return BadRequest();
            Guid productId = Guid.Parse(dataSizeColor.productId.ToString());
            if (_productRepository.GetById(productId) == null)
                return NotFound();
            if (dataSizeColor.data == null || dataSizeColor.data == "" || dataSizeColor.data == "string")
            {
                return Ok(_sizeRepository.GetSizesByProductId(productId));
            }
            else
            {
                Guid color = Guid.Parse(dataSizeColor.data.ToString());
                return Ok(_sizeRepository.GetSizesByProductIdAndColor(productId, color));
            }
        }

        //Lấy số lượng sản phẩm
        [HttpPost("GetQuantity")]
        public ActionResult<String> GetQuantity(DataSizeColor dataSizeColor)
        {
            if (dataSizeColor == null
                || dataSizeColor.productId == null
                || dataSizeColor.productId == ""
                || dataSizeColor.productId == "string"
                || dataSizeColor.color == null
                || dataSizeColor.color == ""
                || dataSizeColor.color == "string"
                || dataSizeColor.size == null
                || dataSizeColor.size == ""
                || dataSizeColor.size == "string")
                return BadRequest();
            Guid productId = Guid.Parse(dataSizeColor.productId.ToString());
            if (_productRepository.GetById(productId) == null)
                return NotFound();
            Guid color = Guid.Parse(dataSizeColor.color.ToString());
            Guid size = Guid.Parse(dataSizeColor.size.ToString());
            return Ok(_sizeRepository.getQuantity(productId, color, size).ToString() + " sản phẩm có sẵn");
            if ((dataSizeColor.color == null
                || dataSizeColor.color == ""
                || dataSizeColor.color == "string")
                && (dataSizeColor.size == null
                || dataSizeColor.size == ""
                || dataSizeColor.size == "string"))
            {
                return Ok(_sizeRepository.getQuantity(productId).ToString());
            }
            else
            {
                
            }
        }

        //Check số lượng sản phẩm
        [HttpPost("CheckQuantity")]
        public ActionResult<String> CheckQuantity(DataSizeColor dataSizeColor)
        {
            if (dataSizeColor == null
                || dataSizeColor.productId == null
                || dataSizeColor.productId == ""
                || dataSizeColor.productId == "string")
                return BadRequest();
            Guid productId = Guid.Parse(dataSizeColor.productId.ToString());
            if (_productRepository.GetById(productId) == null)
                return NotFound();
            if ((dataSizeColor.color == null
                || dataSizeColor.color == ""
                || dataSizeColor.color == "string")
                && (dataSizeColor.size == null
                || dataSizeColor.size == ""
                || dataSizeColor.size == "string"))
            {
                return Ok(_sizeRepository.getQuantity(productId).ToString());
            }
            else
            {
                Guid color = Guid.Parse(dataSizeColor.color.ToString());
                Guid size = Guid.Parse(dataSizeColor.size.ToString());
                return Ok(_sizeRepository.getQuantity(productId, color, size).ToString() + " sản phẩm có sẵn");
            }
        }

        //Lấy product detial
        [HttpPost("GetProductDetail")]
        public ActionResult<String> GetProductDetail(DataSizeColor dataSizeColor)
        {
            if (dataSizeColor == null
                || dataSizeColor.productId == null
                || dataSizeColor.productId == ""
                || dataSizeColor.productId == "string")
                return BadRequest();
            Guid productId = Guid.Parse(dataSizeColor.productId.ToString());
            if (_productRepository.GetById(productId) == null)
                return NotFound();
            if ((dataSizeColor.color == null
                || dataSizeColor.color == ""
                || dataSizeColor.color == "string")
                && (dataSizeColor.size == null
                || dataSizeColor.size == ""
                || dataSizeColor.size == "string"))
            {
                return Ok(_sizeRepository.getProductDetail(productId).ToString());
            }
            else
            {
                Guid color = Guid.Parse(dataSizeColor.color.ToString());
                Guid size = Guid.Parse(dataSizeColor.size.ToString());
                return Ok(_sizeRepository.getProductDetail(productId, color, size).ToString());
            }
        }

        //Lấy số lượng sản phẩm trừ đi giỏ hàng
        //[HttpPost("GetQuantityRemoveCart")]
        //public ActionResult<String> GetQuantityRemoveCart(DataSizeColor dataSizeColor)
        //{
        //    if (dataSizeColor == null
        //        || dataSizeColor.productId == null
        //        || dataSizeColor.productId == ""
        //        || dataSizeColor.productId == "string"
        //        || dataSizeColor.color == null
        //        || dataSizeColor.color == ""
        //        || dataSizeColor.color == "string"
        //        || dataSizeColor.size == null
        //        || dataSizeColor.size == ""
        //        || dataSizeColor.size == "string"
        //        || dataSizeColor.userId == null
        //        || dataSizeColor.userId == ""
        //        || dataSizeColor.userId == "string")
        //        return BadRequest();
        //    Guid productId = Guid.Parse(dataSizeColor.productId.ToString());
        //    if (_productRepository.GetById(productId) == null)
        //        return NotFound();
        //    Guid color = Guid.Parse(dataSizeColor.color.ToString());
        //    Guid size = Guid.Parse(dataSizeColor.size.ToString());
        //    Guid userId = Guid.Parse(dataSizeColor.userId.ToString());
        //    return Ok(_sizeRepository.getQuantityRemoveCart(productId, color, size, userId).ToString());
        //}
    }
}
