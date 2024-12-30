using Application.Data.DTOs;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Application.MVC.Controllers
{
    public class ProductDetailController : Controller
    {
        HttpClient Client = new HttpClient();

        public async Task<ActionResult> Index(int page = 1, int pageSize = 15)
        {
            string URL = $@"https://localhost:7187/api/ProductDetails";
            var response = await Client.GetFromJsonAsync<List<ProductDetail>>(URL);

            // Sort by UpdatedAt (descending)
            var sortedResponse = response.OrderByDescending(p => p.UpdatedAt).ToList();

            // Total number of items
            int totalItems = sortedResponse.Count();

            // Calculate total pages
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Validate the page number
            page = (page < 1) ? 1 : (page > totalPages) ? totalPages : page;

            // Get the data for the current page
            var productDetailsForPage = sortedResponse
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Pass pagination data to the view
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;

            // Return the paginated data
            return View(productDetailsForPage);
        }


        public async Task<ActionResult> Details(Guid ID)
        {
            string URL = $@"https://localhost:7187/api/ProductDetails/{ID}";
            var Response = await Client.GetFromJsonAsync<ProductDetail>(URL);
            return View(Response);
        }

        public async Task<ActionResult> Create()
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
                await Afvhklsjdfklsjlkjdfklsdjklfjiwrjpofds();
                return View();
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

        private async Task Afvhklsjdfklsjlkjdfklsdjklfjiwrjpofds()
        {
            // Lấy danh sách Products và ProductDetails từ API
            var ProductsList = await Client.GetFromJsonAsync<List<Product>>($@"https://localhost:7187/api/Product");
            var ProductDetailsList = await Client.GetFromJsonAsync<List<ProductDetail>>($@"https://localhost:7187/api/ProductDetails");

            // Lọc các sản phẩm chưa được sử dụng trong ProductDetails
            var AvailableProducts = ProductsList?
                .Where(product => !ProductDetailsList!.Any(detail => detail.ProductID == product.ProductID))
                .ToList();

            var ColorsList = await Client.GetFromJsonAsync<List<Color>>($@"https://localhost:7187/api/Color");
            var SizesList = await Client.GetFromJsonAsync<List<Size>>($@"https://localhost:7187/api/Size");
            var CategoriesList = await Client.GetFromJsonAsync<List<Category>>($@"https://localhost:7187/api/Category");
            var SalesList = await Client.GetFromJsonAsync<List<Sale>>($@"https://localhost:7187/api/Sale");

            // Tạo danh sách SelectListItem cho các dropdown
            var ProdsItems = AvailableProducts?
                .Select(pls => new SelectListItem
                {
                    Text = pls.Name,
                    Value = pls.ProductID.ToString()
                }).ToList();

            var ColrsItems = ColorsList?
                .Select(help => new SelectListItem
                {
                    Text = help.ColorName,
                    Value = help.ColorID.ToString()
                }).ToList();

            var SizesItems = SizesList?
                .Select(im => new SelectListItem
                {
                    Text = im.Name,
                    Value = im.SizeID.ToString()
                }).ToList();

            var CatgsItems = CategoriesList?
                .Select(being => new SelectListItem
                {
                    Text = being.CategoryName,
                    Value = being.CategoryID.ToString()
                }).ToList();

            var SalesItems = SalesList?
                .Select(heldHostage => new SelectListItem
                {
                    Text = heldHostage.Name,
                    Value = heldHostage.SaleID.ToString()
                }).ToList();

            // Nếu API trả về null, khởi tạo danh sách trống
            ViewBag.Prods = ProdsItems;
            ViewBag.Colrs = ColrsItems;
            ViewBag.Sizes = SizesItems;
            ViewBag.Catgs = CatgsItems;
            ViewBag.Sales = SalesItems;
        }


        // POST: ProductDetail/Create
        [HttpPost]
        public async Task<ActionResult> Create(ProductDetailDTO Detail, IFormFile? Image)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            MultipartFormDataContent Contents = new()
            {
                { new StringContent(Detail.ProductID.ToString() ?? ""),  nameof(Detail.ProductID) },
                { new StringContent(Detail.SizeID.ToString() ?? ""),     nameof(Detail.SizeID) },
                { new StringContent(Detail.ColorID.ToString() ?? ""),    nameof(Detail.ColorID) },
                { new StringContent(Detail.CategoryID.ToString() ?? ""), nameof(Detail.CategoryID) },
                { new StringContent(Detail.Material ?? ""),              nameof(Detail.Material) },
                { new StringContent(Detail.Quantity.ToString() ?? ""),   nameof(Detail.Quantity) },
                { new StringContent(Detail.Brand ?? ""),                 nameof(Detail.Brand) },
                { new StringContent(Detail.PlaceOfOrigin ?? ""),         nameof(Detail.PlaceOfOrigin) },
                { new StringContent(Detail.Status.ToString() ?? "1"),    nameof(Detail.Status) }
            };

            if (Image != null)
            {
                var ImageStream = new StreamContent(Image.OpenReadStream());
                Contents.Add(ImageStream, nameof(Image), Image.FileName);
            }

            try
            {
                var response = await Client.PostAsync($@"https://localhost:7187/api/ProductDetails", Contents);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                await Afvhklsjdfklsjlkjdfklsdjklfjiwrjpofds();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {Msg.Message}");
                Console.ForegroundColor = ConsoleColor.Gray;
                return View(Detail);
            }
        }
        private async Task Afvhklsjdfklsjlkjdfklsdjklfjiwrjpofdss()
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
            await Afvhklsjdfklsjlkjdfklsdjklfjiwrjpofdss();

            // Lấy thông tin ProductDetail theo ID
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var productDetail = await Client.GetFromJsonAsync<ProductDetailDTO>($@"https://localhost:7187/api/ProductDetails/{id}");
            return View(productDetail);
        }

        // POST: ProductDetail/Edit/{id}
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, ProductDetailDTO Detail, IFormFile? Image)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            MultipartFormDataContent Contents = new()
            {
                { new StringContent(Detail.ProductID.ToString() ?? ""),  nameof(Detail.ProductID) },
                { new StringContent(Detail.SizeID.ToString() ?? ""),     nameof(Detail.SizeID) },
                { new StringContent(Detail.ColorID.ToString() ?? ""),    nameof(Detail.ColorID) },
                { new StringContent(Detail.CategoryID.ToString() ?? ""), nameof(Detail.CategoryID) },
                { new StringContent(Detail.Material ?? ""),              nameof(Detail.Material) },
                { new StringContent(Detail.Quantity.ToString() ?? ""),   nameof(Detail.Quantity) },
                { new StringContent(Detail.Brand ?? ""),                 nameof(Detail.Brand) },
                { new StringContent(Detail.PlaceOfOrigin ?? ""),         nameof(Detail.PlaceOfOrigin) },
                { new StringContent(Detail.Status.ToString() ?? "1"),    nameof(Detail.Status) }
            };

            if (Image != null)
            {
                var ImageStream = new StreamContent(Image.OpenReadStream());
                Contents.Add(ImageStream, nameof(Image), Image.FileName);
            }

            try
            {
                string URL = $@"https://localhost:7187/api/ProductDetails/{id}";
                var Response = await Client.PutAsync(URL, Contents);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                TempData["Error"] = $"Đã có lỗi xảy ra! Lỗi:\n{Msg.Message} ({Msg.HResult})";
                return View(Detail);
            }
        }
        public async Task<ActionResult> Delete(Guid id)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bạn không có quyền vào trang này");
            }
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string requestURL = $@"https://localhost:7187/api/ProductDetails/{id}";
            var response = await Client.DeleteAsync(requestURL);
            return RedirectToAction(nameof(Index));
        }
        [HttpPut]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            string requestURL = $@"https://localhost:7187/api/ProductDetails/{id}/toggle-status";
            var response = await Client.PutAsync(requestURL, null);

            if (response.IsSuccessStatusCode)
            {
                return Ok(); // Trả về 200 nếu thành công
            }
            else
            {
                return StatusCode((byte)response.StatusCode, "Đổi trạng thái thất bại."); // Trả về lỗi nếu không thành công
            }
        }


    }
}
