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

        public async Task<ProductDetail?> GetProductDetailByProductID(Guid TargetID)
        {
            return await Context.ProductDetails
                    .Include(Prod => Prod.Product)
                        .ThenInclude(ImgP => ImgP != null ? ImgP.Image : null)
                    .Include(Siz => Siz.Size)
                    .Include(Col => Col.Color)
                    .Include(Ctg => Ctg.Category).SingleOrDefaultAsync(x => x.ProductID == TargetID);
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

        public async Task<ProductDetailVariationMetadata> CreateNewVariations(Guid DetailID, List<ProductDetailVariationDTO> VariationDetails)
        {
            // Cái này để thông báo Admin nếu như gặp trường hợp thêm biến thể bị trùng lặp (VariationsNotAdded > 0 = thông báo)
            var Metadata = new ProductDetailVariationMetadata()
            {
                VariationsAdded = 0,
                VariationsNotAdded = 0,
                Variations = []
            };

            var NewVariations = new List<ProductDetail>(); // Danh sách biến thể SP 
            var Target = await GetProductDetailByID(DetailID); // Lấy thông tin cũ

            if (Target != null && VariationDetails.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nTarget is not null. PID is {Random.Shared.Next(0, 100000)}\n");
                Console.ForegroundColor = ConsoleColor.Gray;

                foreach (var Variation in VariationDetails)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nFound variation! PID is {Random.Shared.Next(0, 100000)}\n");
                    Console.ForegroundColor = ConsoleColor.Gray;

                    var IfThisHasValueThenPerish = await Context.ProductDetails
                        .Where(Qry =>
                               Qry.ProductID == Target.ProductID && // Nếu trùng ProductID
                               Qry.ColorID == Variation.ColorID &&  // và trùng màu + kích cỡ
                               Qry.SizeID == Variation.SizeID).SingleOrDefaultAsync(); // Kiểm tra trùng lặp

                    if (IfThisHasValueThenPerish == null) // Ko trùng = cho
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"\nNo variation is found, creating new! PID is {Random.Shared.Next(0, 100000)}\n");
                        Console.ForegroundColor = ConsoleColor.Gray;

                        // Tạo Detail mới (biến thể)
                        // & lấy dữ liệu cũ trước, sau đó
                        ProductDetail NewVariation = new()
                        {
                            // Tạo GUID mới
                            ProductDetailID = Guid.NewGuid(),
                            ProductID = Target.ProductID,
                            CategoryID = Target.CategoryID,
                            Material = Target.Material,
                            Brand = Target.Brand,
                            PlaceOfOrigin = Target.PlaceOfOrigin,
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
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\nVariation already exists, ignoring. PID is {Random.Shared.Next(0, 100000)}\n");
                        Console.ForegroundColor = ConsoleColor.Gray;

                        Metadata.VariationsNotAdded += 1;
                    }
                };

                // Xong rồi cho DS vào thay vì phải lặp đi lặp lại lệnh .Add
                await Context.ProductDetails.AddRangeAsync(NewVariations);
                await Context.SaveChangesAsync();

                Metadata.Variations = NewVariations;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nSOMETHING BROKE??? PID is {Random.Shared.Next(0, 100000)}\n");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            return Metadata;
        }
    }
}
