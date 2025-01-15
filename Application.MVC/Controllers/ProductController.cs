using Application.Data.DTOs;
using Application.Data.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace Application.MVC.Controllers
{
    public class ProductController : Controller
    {
        HttpClient client = new HttpClient();
        private readonly INotyfService ToastNotifier;

        public ProductController(INotyfService ToastNotifier)
        {
            this.ToastNotifier = ToastNotifier;
        }
        [ResponseCache(NoStore = true, Duration = 0)]
        public async Task<ActionResult> Index(string searchTerm, int page = 1, int pageSize = 15)
        {
            string requestURL = "https://localhost:7187/api/Product";
            var response = await client.GetFromJsonAsync<List<Product>>(requestURL);

            // Lọc sản phẩm theo tên nếu có searchTerm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                response = response.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Sắp xếp sản phẩm theo ngày tạo (mới nhất trước)
            var sortedResponse = response.OrderByDescending(p => p.CreatedAt).ToList();

            // Tổng số sản phẩm
            int totalItems = sortedResponse.Count;

            // Tính toán phân trang
            var pagedData = sortedResponse
                .Skip((page - 1) * pageSize) // Bỏ qua sản phẩm của các trang trước
                .Take(pageSize) // Lấy số sản phẩm của trang hiện tại
                .ToList();

            // Truyền thông tin phân trang và searchTerm sang View qua ViewBag
            ViewBag.SearchTerm = searchTerm; // Lưu giá trị searchTerm để hiển thị lại trong input
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return View(pagedData);
        }



        public ActionResult Details(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Product/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<Product>(response);
            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductDTO product, IFormFile? Image)
        {
            // Kiểm tra xem tên sản phẩm đã tồn tại trong cơ sở dữ liệu chưa
            string checkProductNameURL = $"https://localhost:7187/api/Product/CheckProductName/{product.Name}";

            var checkResponse = await client.GetAsync(checkProductNameURL);

            if (checkResponse.IsSuccessStatusCode)
            {
                // Kiểm tra nếu không chọn ảnh thì yêu cầu người dùng chọn ảnh
                if (Image == null)
                {
                    ModelState.AddModelError(string.Empty, "Vui lòng chọn ảnh sản phẩm.");
                    return View(product); // Trả về form với thông báo lỗi
                }

                // Nếu tên sản phẩm không trùng, tiếp tục xử lý tạo sản phẩm
                string requestURL = $"https://localhost:7187/api/Product";

                MultipartFormDataContent Contents = new()
                   {
                { new StringContent(product.Name!),                                nameof(product.Name) },
                { new StringContent(product.Description!),                         nameof(product.Description) },
                { new StringContent(product.Price.GetValueOrDefault().ToString()), nameof(product.Price) }
                  };

                if (Image != null)
                {
                    var ImageStream = new StreamContent(Image.OpenReadStream());
                    Contents.Add(ImageStream, nameof(Image), Image.FileName);
                }

                var response = await client.PostAsync(requestURL, Contents);

                if (response.IsSuccessStatusCode)
                {
                    ToastNotifier.Success("Thêm sản phẩm mới thành công!");
                }
                else
                {
                    switch (response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.BadRequest:
                        default:
                            ToastNotifier.Error("Thêm sản phẩm mới thất bại.");
                            break;
                        case System.Net.HttpStatusCode.InternalServerError:
                            ToastNotifier.Error("Thêm sản phẩm mới thất bại: đã có lỗi máy chủ xảy ra.");
                            break;
                        case System.Net.HttpStatusCode.Unauthorized:
                        case System.Net.HttpStatusCode.Forbidden:
                            ToastNotifier.Error("Bạn không có đủ quyền hạn cần thiết.");
                            break;
                        case System.Net.HttpStatusCode.NotFound:
                            ToastNotifier.Error("Không tìm thấy gì.");
                            break;
                        case System.Net.HttpStatusCode.Conflict:
                            ToastNotifier.Error("Tên của sản phẩm không được trùng với các sản phẩm khác!");
                            break;
                    }
                    return View(product);
                }
                return RedirectToAction("Index");
            }
            else
            {
                ToastNotifier.Error("Tên của sản phẩm không được trùng với các sản phẩm khác!");
                return View(product);
            }
        }
        public async Task<ActionResult> Edit(Guid id)
        {

            string productRequestURL = $"https://localhost:7187/api/Product/{id}";
            var productResponse = await client.GetStringAsync(productRequestURL);

            if (string.IsNullOrEmpty(productResponse))
            {
                return NotFound();
            }

            var product = JsonConvert.DeserializeObject<ProductDTO>(productResponse);

            // Đưa dữ liệu hình ảnh vào ViewBag để hiển thị trong combobox

            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid ID, ProductDTO product, IFormFile? Image)
        {

            if (product == null)
            {
                ToastNotifier.Error("Không tìm thấy gì.");
                return RedirectToAction(nameof(Index));
            }

            string requestURL = $"https://localhost:7187/api/Product/{ID}";

            MultipartFormDataContent Contents = new()
        {
            { new StringContent(product.Name!),        nameof(product.Name) },
            { new StringContent(product.Description!), nameof(product.Description) },
            { new StringContent(product.Price.GetValueOrDefault().ToString()), nameof(product.Price) }
        };

            if (Image != null)
            {
                var ImageStream = new StreamContent(Image.OpenReadStream());
                Contents.Add(ImageStream, nameof(Image), Image.FileName);
            }

            var response = await client.PutAsync(requestURL, Contents);

            if (response.IsSuccessStatusCode)
            {
                ToastNotifier.Success("Sửa sản phẩm thành công!");
                return RedirectToAction("Index");
            }
            else
            {
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.BadRequest:
                    default:
                        ToastNotifier.Error("Sửa sản phẩm thất bại.");
                        break;
                    case System.Net.HttpStatusCode.InternalServerError:
                        ToastNotifier.Error("Sửa sản phẩm thất bại: đã có lỗi máy chủ xảy ra.");
                        break;
                    case System.Net.HttpStatusCode.Unauthorized:
                    case System.Net.HttpStatusCode.Forbidden:
                        ToastNotifier.Error("Bạn không có đủ quyền hạn cần thiết.");
                        break;
                    case System.Net.HttpStatusCode.NotFound:
                        ToastNotifier.Error("Không tìm thấy gì.");
                        break;
                    case System.Net.HttpStatusCode.Conflict:
                        ToastNotifier.Error("Tên của sản phẩm không được trùng với các sản phẩm khác!");
                        break;
                }
                return View(product);
            }
        }
            
        public async Task<ActionResult> Delete(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Product/{id}";
            var response = await client.DeleteAsync(requestURL);

            if (response.IsSuccessStatusCode)
            {
                ToastNotifier.Success("Xóa sản phẩm thành công!");
            }
            else
            {
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.BadRequest:
                    default:
                        ToastNotifier.Error("Xóa sản phẩm thất bại.");
                        break;
                    case System.Net.HttpStatusCode.InternalServerError:
                        ToastNotifier.Error("Xóa sản phẩm thất bại: đã có lỗi máy chủ xảy ra.");
                        break;
                    case System.Net.HttpStatusCode.Unauthorized:
                    case System.Net.HttpStatusCode.Forbidden:
                        ToastNotifier.Error("Bạn không có đủ quyền hạn cần thiết.");
                        break;
                    case System.Net.HttpStatusCode.NotFound:
                        ToastNotifier.Warning("Không tìm thấy gì.");
                        break;
                    case System.Net.HttpStatusCode.Conflict:
                        ToastNotifier.Error("Xóa sản phẩm thất bại: sản phẩm đã được mua trước kia rồi.");
                        break;
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
