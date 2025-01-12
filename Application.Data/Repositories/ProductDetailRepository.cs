using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Drawing.Drawing2D;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

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
            var Item = await Context.ProductDetails
                    .Include(Prod => Prod.Product)
                        .ThenInclude(ImgP => ImgP != null ? ImgP.Image : null)
                    .Include(Siz => Siz.Size)
                    .Include(Col => Col.Color)
                    .Include(Ctg => Ctg.Category).SingleOrDefaultAsync(x => x.ProductDetailID == TargetID);

            await ConstantUpdates(Item);

            return Item;
        }

        public async Task<List<ProductDetail>> GetProductDetailsByProductID(Guid TargetID)
        {
            var LeList = await Context.ProductDetails
                    .Include(Prod => Prod.Product)
                        .ThenInclude(ImgP => ImgP != null ? ImgP.Image : null)
                    .Include(Siz => Siz.Size)
                    .Include(Col => Col.Color)
                    .Include(Ctg => Ctg.Category)
                    .Where(x => x.ProductID == TargetID)
                        .ToListAsync();

            foreach (var Item in LeList)
            {
                await ConstantUpdates(Item);
            }
            return LeList;
        }

        public async Task<List<ProductDetail>> GetProductDetails()
        {
            // trích xuất cả dữ liệu image và product bằng cách .Include
            var LeList = await Context.ProductDetails
                .Include(Prod => Prod.Product)
                    .ThenInclude(ImgP => ImgP != null ? ImgP.Image : null)
                .Include(Siz => Siz.Size)
                .Include(Col => Col.Color)
                .Include(Ctg => Ctg.Category).ToListAsync();

            foreach (var Item in LeList)
            {
                await ConstantUpdates(Item);
            }
            return LeList;
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

        public async Task<ProductDetail?> UpdateSetToZero(Guid TargetID)
        {
            var Target = await Context.ProductDetails.FindAsync(TargetID);
            if (Target != null)
            {
                Context.ProductDetails.Attach(Target);

                Target.Quantity = 0;
                Target.Status = 0;
                Target.UpdatedAt = DateTime.UtcNow;

                Context.Update(Target);
                await Context.SaveChangesAsync();
                return Target;
            }
            else return default;
        }

        public async Task<List<ProductDetail>> UpdateStatusOnly(Guid ProductID)
        {
            var Items = await GetProductDetailsByProductID(ProductID);
            if (Items == null) return [];

            byte Status = (byte)(Items.First().Status == 1 ? 0 : 1);

            // Đánh dấu mục này là đã được sửa đổi
            Context.ProductDetails.AttachRange(Items);
            foreach (var Tem in Items)
            {
                // Chỉ cập nhật trường Status mà không thay đổi các trường khác
                Tem.Status = Status;
                Tem.UpdatedAt = DateTime.UtcNow; // Cập nhật thời gian khi thay đổi trạng thái
            }

            Context.ProductDetails.UpdateRange(Items);
            // Lưu thay đổi vào cơ sở dữ liệu
            await Context.SaveChangesAsync();
            return Items;
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
                                ProductDetailID = Variation.ProductDetailID,
                                ProductID = VariationDetails.ProductID,
                                CategoryID = VariationDetails.CategoryID,
                                Material = VariationDetails.Material,
                                Brand = VariationDetails.Brand,
                                PlaceOfOrigin = VariationDetails.PlaceOfOrigin,
                                Status = VariationDetails.Status,
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

        public async Task<bool?> UpdateVariations(Guid ID, ProductDetailMultiDTO VariationDetails)
        {
            var UpdatedVariations = new List<ProductDetail>();

            var HUGE_THING = await GetProductDetailsByProductID(ID);
            if (HUGE_THING != null)
            {
                foreach (var Item in HUGE_THING)
                {
                    Item.ProductID = VariationDetails.ProductID;
                    Item.CategoryID = VariationDetails.CategoryID;
                    Item.Material = VariationDetails.Material;
                    Item.Brand = VariationDetails.Brand;
                    Item.PlaceOfOrigin = VariationDetails.PlaceOfOrigin;
                    Item.Status = VariationDetails.Status;
                    Item.UpdatedAt = DateTime.UtcNow;

                    // Nevermind
                    Item.Quantity = VariationDetails.Variations
                        .Single(Bruh => Bruh.ProductDetailID == Item.ProductDetailID).Quantity;
                    UpdatedVariations.Add(Item);
                }
                Context.ProductDetails.AttachRange(UpdatedVariations);
                Context.ProductDetails.UpdateRange(UpdatedVariations);
                await Context.SaveChangesAsync();

                return true;
            }
            else return false;
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

        private async Task ConstantUpdates(ProductDetail? Item)
        {
            if (Item?.Quantity == 0)
            {
                Context.ProductDetails.Attach(Item);
                Item.Status = 0;
                Context.Update(Item);
                await Context.SaveChangesAsync();
            }
        }
    }
}
