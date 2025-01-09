//using Application.Data.ModelContexts;
//using Application.Data.Models;
//using Application.Data.Repositories.IRepository;
//using AutoMapper;
//using Microsoft.EntityFrameworkCore;

//namespace Application.Data.Repositories
//{
//    public class Color_ProductRepository : IColor_Product
//    {
//        private readonly IMapper Mapper;
//        private readonly GiayDBContext Context;

//        public Color_ProductRepository(IMapper Mapper, GiayDBContext Context)
//        {
//            this.Mapper = Mapper;
//            this.Context = Context;
//        }

//        public async Task<Color_Product> AddRelation(Color_ProductDTO NewRelation)
//        {
//            Color_Product Relation = new();
//            Relation = Mapper.Map(NewRelation, Relation);

//            await Context.Color_Products.AddAsync(Relation);
//            await Context.SaveChangesAsync();

//            return Relation;
//        }

//        public async Task DeleteRelation(Guid RelationID)
//        {
//            var Target = await GetRelationByID(RelationID);
//            if (Target != null)
//            {
//                Context.Color_Products.Attach(Target);
//                Context.Remove(Target);
//                await Context.SaveChangesAsync();
//            }
//        }

//        public async Task<List<Color_Product>> GetAllRelations()
//        {
//            return await Context.Color_Products
//                    .Include(Fx => Fx.Product)
//                        .ThenInclude(Gx => Gx != null ? Gx.Image : default)
//                    .Include(Fx => Fx.Color)
//                        .ToListAsync();
//        }

//        public async Task<Color_Product?> GetRelationByID(Guid RelationID)
//        {
//            return await Context.Color_Products
//                    .Include(Fx => Fx.Product)
//                        .ThenInclude(Gx => Gx != null ? Gx.Image : default)
//                    .Include(Fx => Fx.Color)
//                        .SingleOrDefaultAsync(Hx => Hx.Color_ProductID == RelationID);
//        }

//        public async Task<List<Color_Product>> GetRelationsByProductID(Guid ProductID)
//        {
//            return await Context.Color_Products
//                .Include(Fx => Fx.Product)
//                    .ThenInclude(Gx => Gx != null ? Gx.Image : default)
//                .Include(Fx => Fx.Color)
//                    .Where(Hx => Hx.ProductID == ProductID)
//                        .ToListAsync();
//        }

//        public async Task<Color_Product?> UpdateRelationStatus(Guid RelationID)
//        {
//            var Target = await GetRelationByID(RelationID);
//            if (Target != null)
//            {
//                Context.Color_Products.Attach(Target);
//                if (Target.Available) Target.Available = false;
//                else Target.Available = true;

//                return Target;
//            }
//            else return default;
//        }
//    }
//}
