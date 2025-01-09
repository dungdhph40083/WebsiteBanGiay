using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Application.Data.Repositories
{
    public class ProductDetailRepository : IProductDetail
    {
        private readonly GiayDBContext Context;
        private readonly IMapper Mapper;
        public ProductDetailRepository(GiayDBContext Context, IMapper Mapper)
        {
            this.Context = Context;
            this.Mapper = Mapper;
        }

        public async Task<ProductDetail> CreateNew(ProductDetailDTO NewDetail)
        {
            ProductDetail ProductDetail = new()
            {
                ProductDetailID = Guid.NewGuid(),
                UpdatedAt = DateTime.UtcNow
            };

            ProductDetail = Mapper.Map(NewDetail, ProductDetail);
            await Context.ProductDetails.AddAsync(ProductDetail);
            await Context.SaveChangesAsync();
            return ProductDetail;
        }

        public async Task DeleteExisting(Guid TargetID)
        {
            var Target = await Context.ProductDetails.FindAsync(TargetID);
            if (Target != null)
            {
                Context.ProductDetails.Remove(Target);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<ProductDetail?> GetProductDetailByID(Guid TargetID)
        {
            return await Context.ProductDetails
                    .Include(Prod => Prod.Product)
                        .ThenInclude(ImgP => ImgP != null ? ImgP.Image : null)
                    .Include(Siz => Siz.Size)
                    .Include(Col => Col.Color)
                    .Include(Ctg => Ctg.Category).SingleOrDefaultAsync(x => x.ProductDetailID == TargetID);
        }

        public async Task<List<ProductDetail>> GetProductDetailsByProductID(Guid TargetID)
        {
            return await Context.ProductDetails
                    .Include(Prod => Prod.Product)
                        .ThenInclude(ImgP => ImgP != null ? ImgP.Image : null)
                    .Include(Siz => Siz.Size)
                    .Include(Col => Col.Color)
                    .Include(Ctg => Ctg.Category)
                    .Where(x => x.ProductID == TargetID)
                        .ToListAsync();
        }

        public async Task<List<ProductDetail>> GetProductDetails()
        {
            // trích xuất cả dữ liệu image và product bằng cách .Include
            return await Context.ProductDetails
                .Include(Prod => Prod.Product)
                    .ThenInclude(ImgP => ImgP != null ? ImgP.Image : null)
                .Include(Siz => Siz.Size)
                .Include(Col => Col.Color)
                .Include(Ctg => Ctg.Category).ToListAsync();
        }

        public async Task<ProductDetail?> UpdateExisting(Guid TargetID, ProductDetailDTO UpdatedDetail)
        {
            var Target = await Context.ProductDetails.FindAsync(TargetID);
            if (Target != null)
            {
                Context.ProductDetails.Attach(Target);
                Target.UpdatedAt = DateTime.UtcNow;

                Target = Mapper.Map(UpdatedDetail, Target);
                Context.Update(Target);
                await Context.SaveChangesAsync();
                return Target;
            }
            else return default;
        }

        public async Task<ProductDetail?> UpdateStatusOnly(Guid TargetID, byte Status)
        {
            var Target = await Context.ProductDetails.FindAsync(TargetID);
            if (Target != null)
            {
                // Đánh dấu mục này là đã được sửa đổi
                Context.ProductDetails.Attach(Target);

                // Chỉ cập nhật trường Status mà không thay đổi các trường khác
                Target.Status = Status;
                Target.UpdatedAt = DateTime.UtcNow; // Cập nhật thời gian khi thay đổi trạng thái

                // Lưu thay đổi vào cơ sở dữ liệu
                await Context.SaveChangesAsync();

                return Target;
            }
            else return default; // Trả về null nếu không tìm thấy đối tượng
        }

        public async Task<ProductDetail?> DoAddProductCount(Guid ID, int Count)
        {
            var Target = await GetProductDetailByID(ID);
            if (Target != null)
            {
                Context.ProductDetails.Attach(Target);
                Target.UpdatedAt = DateTime.UtcNow;

                Target.Quantity += Count;

                Context.Update(Target);
                await Context.SaveChangesAsync();
                return Target;
            }
            else return default;
        }

        public async Task<ProductDetailVariationMetadata> CreateNewVariations(ProductDetailMultiDTO VariationDetails)
        {
            // Cái này để thông báo Admin nếu như gặp trường hợp thêm biến thể bị trùng lặp (VariationsNotAdded > 0 = thông báo)
            var Metadata = new ProductDetailVariationMetadata()
            {
                VariationsAdded = 0,
                VariationsNotAdded = 0,
                Variations = []
            };

            var NewVariations = new List<ProductDetail>(); // Danh sách biến thể SP 

            if (VariationDetails.Variations.Count > 0)
            {
                #region FOR DEBUG IN CONSOLE
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nTarget is not null. PID is {Random.Shared.Next(0, 100000)}\n");
                Console.ForegroundColor = ConsoleColor.Gray;
                #endregion

                foreach (var Variation in VariationDetails.Variations)
                {
                    #region FOR DEBUG IN CONSOLE
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nFound variation! PID is {Random.Shared.Next(0, 100000)}\n");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    #endregion

                    var IfThisHasValueThenPerish = await Context.ProductDetails
                        .Where(Qry =>
                               Qry.ProductID == VariationDetails.ProductID && // Nếu trùng ProductID
                               Qry.ColorID == Variation.ColorID &&  // và trùng màu + kích cỡ
                               Qry.SizeID == Variation.SizeID).SingleOrDefaultAsync(); // Kiểm tra trùng lặp

                    if (IfThisHasValueThenPerish == null) // Ko trùng = cho
                    {
                        #region FOR DEBUG IN CONSOLE
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"\nNo variation is found, creating new! PID is {Random.Shared.Next(0, 100000)}\n");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        #endregion

                        if (Variation != null || (Variation?.ColorID != null &&
                                               Variation.SizeID != null &&
                                               Variation.Quantity != null)
                        )
                        {
                            // Tạo Detail mới (biến thể)
                            // & lấy dữ liệu cũ trước, sau đó
                            ProductDetail NewVariation = new()
                            {
                                // Tạo GUID mới
                                ProductDetailID = Guid.NewGuid(),
                                ProductID = VariationDetails.ProductID,
                                CategoryID = VariationDetails.CategoryID,
                                Material = VariationDetails.Material,
                                Brand = VariationDetails.Brand,
                                PlaceOfOrigin = VariationDetails.PlaceOfOrigin,
                                Status = 1,
                                UpdatedAt = DateTime.UtcNow
                            };

                            // Lấy dữ liệu mới vào để làm biến thể mới
                            NewVariation = Mapper.Map(Variation, NewVariation);

                            NewVariations.Add(NewVariation); // Thêm vào danh sách biến thể
                            Metadata.VariationsAdded += 1;
                        }
                        else
                        {
                            #region FOR DEBUG IN CONSOLE
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"\nVariation data isn't available: it could've been from an empty cell - ignoring anyway. PID is {Random.Shared.Next(0, 100000)}\n");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            #endregion
                        }
                    }
                    else
                    {
                        #region FOR DEBUG IN CONSOLE
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\nVariation already exists, ignoring (add 1 to not added). PID is {Random.Shared.Next(0, 100000)}\n");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        #endregion

                        Metadata.VariationsNotAdded += 1;

                        // Có thể là thêm vào thêm số lượng nếu mà gặp phải cái đó
                    }
                };

                // Xong rồi cho DS vào thay vì phải lặp đi lặp lại lệnh .Add
                await Context.ProductDetails.AddRangeAsync(NewVariations);
                await Context.SaveChangesAsync();

                Metadata.Variations = NewVariations;
            }
            else
            {
                #region FOR DEBUG IN CONSOLE
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nSOMETHING BROKE??? PID is {Random.Shared.Next(0, 100000)}\n");
                Console.ForegroundColor = ConsoleColor.Gray;
                #endregion
            }
            return Metadata;
        }

        public async Task DeleteExistingByProductID(Guid TargetID)
        {
            var Targets = await GetProductDetailsByProductID(TargetID);
            if (Targets.Count > 0)
            {
                Context.ProductDetails.RemoveRange(Targets);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<List<ProductDetail>> GetVariationsByProductID(Guid? TargetID)
        {
            var FilteredPies = new List<ProductDetail>();
            List<ProductDetail> Targets;

            if (TargetID != null)
            {
                Targets = await GetProductDetailsByProductID(TargetID.GetValueOrDefault());
            }
            else Targets = await GetProductDetails();

            foreach (var Ball in Targets)
            {
                FilteredPies.Add(new()
                {
                    ProductDetailID = Ball.ProductDetailID,
                    ProductID = Ball.ProductID,
                    ColorID = Ball.ColorID,
                    SizeID = Ball.SizeID,
                    Color = Ball.Color,
                    Size = Ball.Size
                });
            }

            return FilteredPies;
        }
    }
}
