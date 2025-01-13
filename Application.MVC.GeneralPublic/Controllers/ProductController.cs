using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Net.Http.Headers;
namespace Application.MVC.GeneralPublic.Controllers
{
    public class ProductController : Controller
    {
        HttpClient client = new HttpClient();
        public async Task<ActionResult> Index(int Page = 1, string PriceRange = "All", string SortBy = "Default", Guid? CategoryQuery = null, Guid? ColorQuery = null, Guid? SizeQuery = null)
        {
            long MinPrice = 0, MaxPrice = long.MaxValue;
            int PageSize = 9, TotalProducts = 0, TotalPages = (int)Math.Ceiling((double)(TotalProducts / PageSize));

            // Lấy DS sản phẩm & chi tiết của chúng
            var Products = await client.GetFromJsonAsync<List<Product>>("https://localhost:7187/api/Product");
            ViewBag.Orders = Products ?? new List<Product>();

            var Details = await client.GetFromJsonAsync<List<ProductDetail>>("https://localhost:7187/api/ProductDetails");
            Details = Details?.OrderByDescending(Req => Req.Product?.CreatedAt)
                .GroupBy(Sdf => Sdf.ProductID).Select(Req => Req.First()).ToList();
            ViewBag.Details = Details ?? new List<ProductDetail>();

            if (Products != null && Details != null)
            {
                // Lấy các DS: danh mục, màu và kích cỡ
                var Categories = await client.GetFromJsonAsync<List<Category>>("https://localhost:7187/api/Category");
                var Colors = await client.GetFromJsonAsync<List<Color>>("https://localhost:7187/api/Color");
                var Sizes = await client.GetFromJsonAsync<List<Size>>("https://localhost:7187/api/Size");

                // Sắp các cái lọc
                var CategoriesView = Categories?.Select(Targ => new
                {
                    Targ.CategoryID,
                    Targ.CategoryName,
                    ProductCount = Details?.Where(Sdf => Sdf.CategoryID == Sdf.CategoryID).Count() ?? 0
                }).ToList();

                var ColoresView = Colors?.Select(Targ => new
                {
                    Targ.ColorID,
                    Targ.ColorName,
                    ProductCount = Details?.Where(Sdf => Sdf.ColorID == Sdf.ColorID).Count() ?? 0
                }).ToList();

                var SizesView = Sizes?.Select(Targ => new
                {
                    Targ.SizeID,
                    Targ.Name,
                    ProductCount = Details?.Where(Sdf => Sdf.SizeID == Sdf.SizeID).Count() ?? 0
                }).ToList();

                // Lọc giá
                switch (PriceRange)
                {
                    case "Low":
                        {
                            MaxPrice = 100_000;
                        }
                        break;
                    case "Mid":
                        {
                            MinPrice = 100_000;
                            MaxPrice = 500_000;
                        }
                        break;
                    case "High":
                        {
                            MinPrice = 500_000;
                            MaxPrice = 1_000_000;
                        }
                        break;
                    case "Ultra":
                        {
                            MinPrice = 1_000_000;
                            MaxPrice = 2_500_000;
                        }
                        break;
                    case "ProMax":
                        {
                            MinPrice = 2_500_000;
                        }
                        break;
                    case "All":
                    default:
                        break;
                }

                // Lọc giá (REAL)
                var FilteredDetails = Details
                    .Where(Flt => Products.Any
                          (Cri => Cri.ProductID == Flt.ProductID &&
                                  Cri.Price.GetValueOrDefault() >= MinPrice &&
                                  Cri.Price.GetValueOrDefault() <= MaxPrice)).ToList() ?? [];

                // Lọc sản phẩm theo danh mục
                if (CategoryQuery != null)
                {
                    FilteredDetails = FilteredDetails?.Where
                        (Sdf => Sdf.CategoryID == CategoryQuery).ToList();
                }
                if (ColorQuery != null)
                {
                    FilteredDetails = FilteredDetails?.Where
                        (Sdf => Sdf.ColorID == ColorQuery).ToList();
                }
                if (SizeQuery != null)
                {
                    FilteredDetails = FilteredDetails?.Where
                        (Sdf => Sdf.SizeID == SizeQuery).ToList();
                }

                // Sắp xếp
                switch (SortBy)
                {
                    case "Ascending":
                        FilteredDetails = FilteredDetails?.OrderBy(Req => Req.Product?.Name).ToList();
                        break;
                    case "Descending":
                        FilteredDetails = FilteredDetails?.OrderByDescending(Req => Req.Product?.Name).ToList();
                        break;
                    case "Default":
                    default:
                        break;
                }

                // Xong chưa?
                TotalProducts = FilteredDetails!.Count;
                var CurrentPageProducts = FilteredDetails!.Skip((Page - 1) * PageSize).Take(PageSize).ToList();

                // Truyền các thứ vào Viu
                ViewBag.CurrentPage = Page;
                ViewBag.TotalPages = TotalPages;
                ViewBag.PriceRange = PriceRange;
                ViewBag.SortOrder = SortBy;
                ViewBag.SelectedCategory = CategoryQuery;
                ViewBag.SelectedColor = ColorQuery;
                ViewBag.SelectedSize = SizeQuery;

                ViewBag.Colors = Colors;
                ViewBag.Sizes = Sizes;
                ViewBag.Categories = Categories;

                return View(CurrentPageProducts);
            }
            else return View();
        }

        public async Task<ActionResult> Details(Guid ID)
        {
            var productss = await client.GetFromJsonAsync<List<Product>>("https://localhost:7187/api/Product");
            ViewBag.Products = productss ?? new List<Product>();

            var sizes = await client.GetFromJsonAsync<List<Size>>("https://localhost:7187/api/Size");
            ViewBag.Sizes = sizes ?? new List<Size>();

            var colors = await client.GetFromJsonAsync<List<Color>>("https://localhost:7187/api/Color");
            ViewBag.Colors = colors ?? new List<Color>();

            string productDetailUrl = $"https://localhost:7187/api/ProductDetails/{ID}";
            var productDetail = await client.GetFromJsonAsync<ProductDetail>(productDetailUrl);


            if (productDetail == null)
            {
                return NotFound();
            }

            return View(productDetail);
        }
    }
}