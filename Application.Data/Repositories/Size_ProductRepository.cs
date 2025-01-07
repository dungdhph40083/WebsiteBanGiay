//using Application.Data.ModelContexts;
//using Application.Data.Models;
//using Application.Data.Repositories.IRepository;
//using AutoMapper;
//using Microsoft.EntityFrameworkCore;

//namespace Application.Data.Repositories
//{
//    public class Size_ProductRepository : ISize_Product
//    {
//        private readonly IMapper Mapper;
//        private readonly GiayDBContext Context;

//        public Size_ProductRepository(IMapper Mapper, GiayDBContext Context)
//        {
//            this.Mapper = Mapper;
//            this.Context = Context;
//        }

//        public async Task<Size_Product> AddRelation(Size_ProductDTO NewRelation)
//        {
//            Size_Product Relation = new();
//            Relation = Mapper.Map(NewRelation, Relation);

//            await Context.Size_Products.AddAsync(Relation);
//            await Context.SaveChangesAsync();

//            return Relation;
//        }

//        public async Task DeleteRelation(Guid RelationID)
//        {
//            var Target = await GetRelationByID(RelationID);
//            if (Target != null)
//            {
//                Context.Size_Products.Attach(Target);
//                Context.Remove(Target);
//                await Context.SaveChangesAsync();
//            }
//        }

//        public async Task<List<Size_Product>> GetAllRelations()
//        {
//            return await Context.Size_Products
//                    .Include(Fx => Fx.Product)
//                        .ThenInclude(Gx => Gx != null ? Gx.Image : default)
//                    .Include(Fx => Fx.Size)
//                        .ToListAsync();
//        }

//        public async Task<Size_Product?> GetRelationByID(Guid RelationID)
//        {
//            return await Context.Size_Products
//                    .Include(Fx => Fx.Product)
//                        .ThenInclude(Gx => Gx != null ? Gx.Image : default)
//                    .Include(Fx => Fx.Size)
//                        .SingleOrDefaultAsync(Hx => Hx.Size_ProductID == RelationID);
//        }

//        public async Task<List<Size_Product>> GetRelationsByProductID(Guid ProductID)
//        {
//            return await Context.Size_Products
//                    .Include(Fx => Fx.Product)
//                        .ThenInclude(Gx => Gx != null ? Gx.Image : default)
//                    .Include(Fx => Fx.Size)
//                        .Where(Hx => Hx.ProductID == ProductID)
//                            .ToListAsync();
//        }

//        public async Task<Size_Product?> UpdateRelationStatus(Guid RelationID)
//        {
//            var Target = await GetRelationByID(RelationID);
//            if (Target != null)
//            {
//                Context.Size_Products.Attach(Target);
//                if (Target.Available) Target.Available = false;
//                else Target.Available = true;

//                return Target;
//            }
//            else return default;
//        }
//    }
//}
