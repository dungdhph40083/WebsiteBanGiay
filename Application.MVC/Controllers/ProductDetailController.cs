using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using Mono.TextTemplating;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Security.Permissions;
using System.Net.Http.Headers;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace Application.MVC.Controllers
{
    public class ProductDetailController : Controller
    {
        HttpClient Client = new HttpClient();
        private readonly INotyfService ToastNotifier;
        public ProductDetailController(INotyfService ToastNotifier)
        {
            this.ToastNotifier = ToastNotifier;
        }

        public async Task<ActionResult> Index(int Page = 1, int PageSize = 10, string SearchQuery = "", string Status = "")
        {
            string URL_Products = $@"https://localhost:7187/api/ProductDetails";

            var ProductList = await Client.GetFromJsonAsync<List<ProductDetail>>(URL_Products);

            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                ProductList = ProductList?.Where
                    (Gfgd => Gfgd.Product != null && Gfgd.Product.Name != null &&
                     Gfgd.Product.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var SortedList =
                ProductList?.OrderByDescending(Sdf => Sdf.UpdatedAt)
                            .GroupBy(Fltr => Fltr.ProductID)
                            .Select(Req => Req.First(Sdf => Sdf.Status == 1 ? Sdf.Status == 1 : Sdf.Status == 0))
                            .ToList();
            switch (Status)
            {
                case "Active":
                    {
                        SortedList = SortedList?.Where(Sdf => Sdf.Status == 1).ToList();
                        break;
                    }
                case "Inactive":
                    {
                        SortedList = SortedList?.Where(Sdf => Sdf.Status == 0).ToList();
                        break;
                    }
                case "All": // mheheheheh "Case" "All" = CaseOh mehjejjehjfkhjkfhn okay i'm stopping
                default:
                    {
                        break;
                    }
            }

            int? ItemCount = SortedList?.Count;
            int TotalPages = (int)Math.Ceiling((double)ItemCount.GetValueOrDefault() / PageSize);

            Page = Page < 1 ? 1 : (Page > TotalPages ? TotalPages : Page);

            var PaginatedList = SortedList?
                .Skip((Page - 1) * PageSize)
                .Take(PageSize).ToList();

            ViewBag.Page = Page;
            ViewBag.PageSize = PageSize;
            ViewBag.TotalPages = TotalPages;
            ViewBag.SearchQuery = SearchQuery;

            return View(PaginatedList);
        }

        public async Task<ActionResult> Details(Guid ID)
        {
            string URL = $@"https://localhost:7187/api/ProductDetails/ByProduct/{ID}";
            var Response = await Client.GetFromJsonAsync<List<ProductDetail>>(URL);
            return View(Response);
        }

        [HttpGet]
        public async Task<ActionResult> Create(Guid? FromID)
        {
            try
            {
                await PopulateDropdowns(FromID);

                if (FromID != null)
                {
                    string URL = $@"https://localhost:7187/api/ProductDetails/ByProduct/{FromID}";
                    var Response = await Client.GetFromJsonAsync<List<ProductDetail>>(URL);

                    if (Response != null)
                    {
                        ViewBag.FromID = FromID;
                        ViewBag.CatgID = Response?.First().CategoryID;

                        var ParsedOutInfo = new ProductDetailMultiDTO();

                        ParsedOutInfo.ProductID = Response?.First().ProductID;
                        ParsedOutInfo.CategoryID = Response?.First().CategoryID;
                        ParsedOutInfo.Brand = Response?.First().Brand;
                        ParsedOutInfo.Material = Response?.First().Material;
                        ParsedOutInfo.PlaceOfOrigin = Response?.First().PlaceOfOrigin;
                        ParsedOutInfo.Status = Response?.First(Sdf => Sdf.Status == 1 ? Sdf.Status == 1 : Sdf.Status == 0).Status;
                        ParsedOutInfo.Variations.Add(new()
                        {
                            ProductDetailID = Guid.NewGuid(),
                            ColorID = null,
                            SizeID = null,
                            Quantity = null
                        });

                        return View(ParsedOutInfo);
                    }
                    else return RedirectToAction(nameof(Index));
                }
                else return View();
            }
            catch (Exception ex)
            {
                // Log lỗi nếu có
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.Gray;
                return RedirectToAction(nameof(Index));
            }
        }

        private async Task PopulateDropdowns(Guid? FromID = null)
        {
            var ProductsList = await Client.GetFromJsonAsync<List<Product>>(@"https://localhost:7187/api/Product");
            var ProductDetailsList = await Client.GetFromJsonAsync<List<ProductDetail>>(@"https://localhost:7187/api/ProductDetails");

            List<Product> AvailableProducts;

            if (FromID == null)
            {
                AvailableProducts = ProductsList?
                    .Where(product => !ProductDetailsList!.Any(detail => detail.ProductID == product.ProductID))
                    .ToList();
            }
            else
            {

                AvailableProducts = ProductsList;
            }


            var ColorsList = (await Client.GetFromJsonAsync<List<Color>>(@"https://localhost:7187/api/Color"))?.Where(No => No.Status == 1).ToList();
            var SizesList = (await Client.GetFromJsonAsync<List<Size>>(@"https://localhost:7187/api/Size"))?.Where(No => No.Status == 1).ToList();
            var CategoriesList = await Client.GetFromJsonAsync<List<Category>>(@"https://localhost:7187/api/Category");
            var SalesList = await Client.GetFromJsonAsync<List<Sale>>(@"https://localhost:7187/api/Sale");

            ViewBag.Prods = AvailableProducts?
                .Select(pls => new SelectListItem
                {
                    Text = pls.Name,
                    Value = pls.ProductID.ToString()
                }).ToList();

            ViewBag.Colrs = ColorsList?
                .Select(help => new SelectListItem
                {
                    Text = help.ColorName,
                    Value = help.ColorID.ToString()
                }).ToList();

            ViewBag.Sizes = SizesList?
                .Select(im => new SelectListItem
                {
                    Text = im.Name,
                    Value = im.SizeID.ToString()
                }).ToList();

            ViewBag.Catgs = CategoriesList?
                .Select(being => new SelectListItem
                {
                    Text = being.CategoryName,
                    Value = being.CategoryID.ToString()
                }).ToList();

            ViewBag.Sales = SalesList?
                .Select(heldHostage => new SelectListItem
                {
                    Text = heldHostage.Name,
                    Value = heldHostage.SaleID.ToString()
                }).ToList();
        }

        // POST: ProductDetail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(bool FromEdit, ProductDetailMultiDTO Details)
        {
            try
            {
                var Response = await Client.PostAsJsonAsync($@"https://localhost:7187/api/ProductDetails/AddVariations", Details);
                if (Response.IsSuccessStatusCode)
                {
                    ToastNotifier.Success("Tạo các biến thể thành công!");
                }
                else
                {
                    switch (Response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.BadRequest:
                        default:
                            ToastNotifier.Error("Tạo các biến thể thất bại.");
                            break;
                        case System.Net.HttpStatusCode.InternalServerError:
                            ToastNotifier.Error("Tạo các biến thể thất bại: đã có lỗi máy chủ xảy ra.");
                            break;
                        case System.Net.HttpStatusCode.Unauthorized:
                        case System.Net.HttpStatusCode.Forbidden:
                            ToastNotifier.Error("Bạn không có đủ quyền hạn cần thiết.");
                            break;
                        case System.Net.HttpStatusCode.NotFound:
                            ToastNotifier.Warning("Không tìm thấy gì.");
                            break;
                        case System.Net.HttpStatusCode.Conflict:
                        {
                            ToastNotifier.Success("Tạo các biến thể thành công!");
                            ToastNotifier.Warning("Một số biến thể mới đã không tạo ra do các biến thể đó đã tồn tại trước đó.");
                            break;
                        }
                    }
                    return View(Details);
                }
                if (FromEdit)
                {
                    return RedirectToAction(nameof(Edit), new { ID = Details.ProductID });
                }
                else return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                await PopulateDropdowns();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {Msg.Message}");
                Console.ForegroundColor = ConsoleColor.Gray;
                return View(Details);
            }
        }

        private async Task PopulateDropdownsOld()
        {
            // Lấy danh sách Products và Images từ API
            var ProductsList = await Client.GetFromJsonAsync<List<Product>>($@"https://localhost:7187/api/Product");
            var ColorsList = await Client.GetFromJsonAsync<List<Color>>($@"https://localhost:7187/api/Color");
            var SizesList = await Client.GetFromJsonAsync<List<Size>>($@"https://localhost:7187/api/Size");
            var CategoriesList = await Client.GetFromJsonAsync<List<Category>>($@"https://localhost:7187/api/Category");
            var SalesList = await Client.GetFromJsonAsync<List<Sale>>($@"https://localhost:7187/api/Sale");

            var ProdsItems = ProductsList?
                .Select(Pls => new SelectListItem
                { Text = Pls.Name, Value = Pls.ProductID.ToString() }).ToList();

            var ColrsItems = ColorsList?
                .Select(Help => new SelectListItem
                { Text = Help.ColorName, Value = Help.ColorID.ToString() }).ToList();

            var SizesItems = SizesList?
                .Select(Im => new SelectListItem
                { Text = Im.Name, Value = Im.SizeID.ToString() }).ToList();

            var CatgsItems = CategoriesList?
                .Select(Being => new SelectListItem
                { Text = Being.CategoryName, Value = Being.CategoryID.ToString() }).ToList();

            var SalesItems = SalesList?
                .Select(HeldHostage => new SelectListItem
                { Text = HeldHostage.Name, Value = HeldHostage.SaleID.ToString() }).ToList();

            // Nếu API trả về null, khởi tạo danh sách trống
            ViewBag.Prods = ProdsItems;
            ViewBag.Colrs = ColrsItems;
            ViewBag.Sizes = SizesItems;
            ViewBag.Catgs = CatgsItems;
            ViewBag.Sales = SalesItems;
        }
        public async Task<ActionResult> Edit(Guid id)
        {
            var CategoriesList = await Client.GetFromJsonAsync<List<Category>>($@"https://localhost:7187/api/Category");

            var CatgsItems = CategoriesList?
                            .Select(Being => new SelectListItem
                            { Text = Being.CategoryName, Value = Being.CategoryID.ToString() }).ToList();

            ViewBag.Catgs = CatgsItems;


            // Lấy thông tin ProductDetail theo ID
            var ProductDetail = await Client.GetFromJsonAsync<List<ProductDetail>>($@"https://localhost:7187/api/ProductDetails/ByProduct/{id}");

            var ProductsList = await Client.GetFromJsonAsync<List<Product>>($@"https://localhost:7187/api/Product");
            var ColorsList = await Client.GetFromJsonAsync<List<Color>>($@"https://localhost:7187/api/Color");
            var SizesList = await Client.GetFromJsonAsync<List<Size>>($@"https://localhost:7187/api/Size");

            ViewBag.Prods = ProductsList;
            ViewBag.Colrs = ColorsList;
            ViewBag.Sizes = SizesList;

            if (ProductDetail != null || ProductDetail?.Count > 0)
            {
                var ParsedOutInfo = new ProductDetailMultiDTO()
                {
                    ProductID = ProductDetail?.First().ProductID,
                    CategoryID = ProductDetail?.First().CategoryID,
                    Brand = ProductDetail?.First().Brand,
                    Material = ProductDetail?.First().Material,
                    PlaceOfOrigin = ProductDetail?.First().PlaceOfOrigin,
                    Status = ProductDetail?.First().Status
                };

                foreach (var Detail in ProductDetail!)
                {
                    ParsedOutInfo.Variations.Add(new()
                    {
                        ProductDetailID = Detail.ProductDetailID,
                        ColorID = Detail.ColorID,
                        SizeID = Detail.SizeID,
                        Quantity = Detail.Quantity
                    });
                }

                return View(ParsedOutInfo);
            }
            else return RedirectToAction(nameof(Index));
        }

        // POST: ProductDetail/Edit/{id}
        [HttpPost]
        public async Task<ActionResult> Edit(Guid ID, ProductDetailMultiDTO Details)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/ProductDetails/UpdateVariations/{ID}";
                var Response = await Client.PutAsJsonAsync(URL, Details);

                if (Response.IsSuccessStatusCode)
                {
                    ToastNotifier.Success("Sửa các biến thể thành công!");
                }
                else
                {
                    switch (Response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.BadRequest:
                        default:
                            ToastNotifier.Error("Sửa các biến thể thất bại.");
                            break;
                        case System.Net.HttpStatusCode.InternalServerError:
                            ToastNotifier.Error("Sửa các biến thể thất bại: đã có lỗi máy chủ xảy ra.");
                            break;
                        case System.Net.HttpStatusCode.Unauthorized:
                        case System.Net.HttpStatusCode.Forbidden:
                            ToastNotifier.Error("Bạn không có đủ quyền hạn cần thiết.");
                            break;
                        case System.Net.HttpStatusCode.NotFound:
                            ToastNotifier.Warning("Không tìm thấy gì.");
                            break;
                        case System.Net.HttpStatusCode.Conflict:
                        {
                            ToastNotifier.Success("Sửa các biến thể thành công!");
                            ToastNotifier.Warning("Một số biến thể mới đã không thể sửa do các biến thể đó đã tồn tại trước đó.");
                            break;
                        }
                        return View(Details);
                    }
                }

                return RedirectToAction(nameof(Edit), new { ID });
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                TempData["Error"] = $"Đã có lỗi xảy ra! Lỗi:\n{Msg.Message} ({Msg.HResult})";
                return View(Details);
            }
        }
        public async Task<ActionResult> Delete(Guid ID, Guid FromID)
        {
            string requestURL = $@"https://localhost:7187/api/ProductDetails/{ID}";
            var response = await Client.DeleteAsync(requestURL);

            if (response.IsSuccessStatusCode)
            {
                ToastNotifier.Success("Xóa biến thể thành công!");
            }
            else
            {
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.BadRequest:
                    default:
                        ToastNotifier.Error("Xóa biến thể thất bại.");
                        break;
                    case System.Net.HttpStatusCode.InternalServerError:
                        ToastNotifier.Error("Xóa biến thể thất bại: đã có lỗi máy chủ xảy ra.");
                        break;
                    case System.Net.HttpStatusCode.Unauthorized:
                    case System.Net.HttpStatusCode.Forbidden:
                        ToastNotifier.Error("Bạn không có đủ quyền hạn cần thiết.");
                        break;
                    case System.Net.HttpStatusCode.NotFound:
                        ToastNotifier.Warning("Không tìm thấy gì.");
                        break;
                    case System.Net.HttpStatusCode.Conflict:
                        ToastNotifier.Error("Xóa biến thể thất bại: sản phẩm đã được mua trước kia rồi.");
                        break;
                }
            }

            return RedirectToAction(nameof(Edit), new { ID = FromID });
        }

        public async Task<ActionResult> TryDeleteWhole(Guid ID, Guid FromID)
        {
            string requestURL = $@"https://localhost:7187/api/ProductDetails/ByProduct/{ID}";
            var response = await Client.DeleteAsync(requestURL);

            Console.WriteLine("\n" + response.StatusCode + "\n");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Deleted whole for some reason");
                ToastNotifier.Success("Xóa các biến thể thành công!");
            }
            else
            {
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.BadRequest:
                    default:
                        ToastNotifier.Error("Xóa các biến thể thất bại.");
                        break;
                    case System.Net.HttpStatusCode.InternalServerError:
                        ToastNotifier.Error("Xóa các biến thể thất bại: đã có lỗi máy chủ xảy ra.");
                        break;
                    case System.Net.HttpStatusCode.Unauthorized:
                    case System.Net.HttpStatusCode.Forbidden:
                        {
                            ToastNotifier.Error("Bạn không có đủ quyền hạn cần thiết.");
                            Unauthorized();
                        }
                        break;
                    case System.Net.HttpStatusCode.NotFound:
                        {
                            ToastNotifier.Warning("Không tìm thấy gì.");
                            NotFound();
                        }
                        break;
                    case System.Net.HttpStatusCode.Conflict:
                        {
                            ToastNotifier.Success("Xóa các biến thể thành công!");
                            ToastNotifier.Warning("Một số biến thể đã không thể xóa do các biến thể đó đã được đặt hàng trước đó.");
                            Conflict();
                            break;
                        }
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPut]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            string requestURL = $@"https://localhost:7187/api/ProductDetails/{id}/ToggleStatus";
            var response = await Client.PutAsync(requestURL, null);

            if (response.IsSuccessStatusCode)
            {
                ToastNotifier.Success("Đổi trạng thái thành công!");
                return Ok(); // Trả về 200 nếu thành công
            }
            else
            {
                ToastNotifier.Error("Đổi trạng thái thất bại.");
                return StatusCode((byte)response.StatusCode, "Đổi trạng thái thất bại."); // Trả về lỗi nếu không thành công
            }
        }
    }
}
