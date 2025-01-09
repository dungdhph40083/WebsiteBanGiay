//using Application.Data.ModelContexts;
//using Application.Data.Models;
//using Application.Data.Repositories.IRepository;
//using AutoMapper;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace Application.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class Color_ProductsController : ControllerBase
//    {
//        private readonly IColor_Product SizeProductRepo;

//        public Color_ProductsController(IColor_Product SizeProductRepo)
//        {
//            this.SizeProductRepo = SizeProductRepo;
//        }

//        [HttpGet]
//        public async Task<ActionResult<List<Color_Product>>> GetAllRelations()
//        {
//            return await SizeProductRepo.GetAllRelations();
//        }

//        [HttpGet("{RelationID}")]
//        public async Task<ActionResult<Color_Product?>> GetRelationByID(Guid RelationID)
//        {
//            var Target = await SizeProductRepo.GetRelationByID(RelationID);
//            if (Target == null) return NoContent();
//            return Target;
//        }

//        [HttpGet]
//        public async Task<ActionResult<List<Color_Product>>> GetRelationsByProductID(Guid ProductID)
//        {
//            return await SizeProductRepo.GetRelationsByProductID(ProductID);
//        }

//        [HttpPost]
//        public async Task<ActionResult<Color_Product>> AddRelation(Color_ProductDTO NewRelation)
//        {
//            return await SizeProductRepo.AddRelation(NewRelation);
//        }

//        [HttpPatch]
//        public async Task<ActionResult<Color_Product>> UpdateRelationStatus(Guid RelationID)
//        {
//            var Target = await SizeProductRepo.UpdateRelationStatus(RelationID);
//            if (Target == null) return NotFound();
//            else return Target;
//        }

//        [HttpDelete]
//        public async Task<ActionResult> DeleteRelation(Guid RelationID)
//        {
//            await SizeProductRepo.DeleteRelation(RelationID);
//            return NoContent();
//        }
//    }
//}
