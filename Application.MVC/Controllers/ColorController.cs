using Application.Data.DTOs;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Application.MVC.Controllers
{
    public class ColorController : Controller
    {
        private readonly INotyfService ToastNotifier;
        HttpClient _client = new HttpClient();

        public ColorController(INotyfService ToastNotifier)
        {
            this.ToastNotifier = ToastNotifier;
        }
        public ActionResult Index()
        {
            string requestURL = "https://localhost:7187/api/Color";
            var response = _client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<List<ColorDTO>>(response);
            return View(data);
        }
        public ActionResult Details(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Color/{id}";
            var response = _client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<ColorDTO>(response);
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ColorDTO colorDTO)
        {
            string requestURL = $"https://localhost:7187/api/Color";
            var response = await _client.PostAsJsonAsync(requestURL, colorDTO);

            if (response.IsSuccessStatusCode)
            {
                ToastNotifier.Success("Thêm màu mới thành công!");
            }
            else
            {
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.BadRequest:
                    default:
                        ToastNotifier.Error("Thêm màu mới thất bại.");
                        break;
                    case System.Net.HttpStatusCode.InternalServerError:
                        ToastNotifier.Error("Thêm màu mới thất bại: đã có lỗi máy chủ xảy ra.");
                        break;
                    case System.Net.HttpStatusCode.Unauthorized:
                    case System.Net.HttpStatusCode.Forbidden:
                        ToastNotifier.Error("Bạn không có đủ quyền hạn cần thiết.");
                        break;
                    case System.Net.HttpStatusCode.NotFound:
                        ToastNotifier.Error("Không tìm thấy gì.");
                        break;
                    case System.Net.HttpStatusCode.Conflict:
                        ToastNotifier.Error("Tên của màu không được trùng với các màu khác!");
                        break;
                }
                return View(colorDTO);
            }

            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {  
            string requestURL = $"https://localhost:7187/api/Color/{id}";
            var response = _client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<ColorDTO>(response);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Guid id, ColorDTO colorDTO)
        {
            string requestURL = $"https://localhost:7187/api/Color/{id}";
            var response = _client.PutAsJsonAsync(requestURL, colorDTO).Result;

            if (response.IsSuccessStatusCode)
            {
                ToastNotifier.Success("Sửa màu thành công!");
            }
            else
            {
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.BadRequest:
                    default:
                        ToastNotifier.Error("Sửa màu thất bại.");
                        break;
                    case System.Net.HttpStatusCode.InternalServerError:
                        ToastNotifier.Error("Sửa màu thất bại: đã có lỗi máy chủ xảy ra.");
                        break;
                    case System.Net.HttpStatusCode.Unauthorized:
                    case System.Net.HttpStatusCode.Forbidden:
                        ToastNotifier.Error("Bạn không có đủ quyền hạn cần thiết.");
                        break;
                    case System.Net.HttpStatusCode.NotFound:
                        ToastNotifier.Error("Không tìm thấy gì.");
                        break;
                    case System.Net.HttpStatusCode.Conflict:
                        ToastNotifier.Error("Tên của màu không được trùng với các màu khác!");
                        break;
                }
                return View(colorDTO);
            }

            return RedirectToAction("Index");
        }
        public ActionResult Delete(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Color/{id}";
            var response = _client.DeleteAsync(requestURL).Result;

            if (response.IsSuccessStatusCode)
            {
                ToastNotifier.Success("Xóa màu thành công!");
            }
            else
            {
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.BadRequest:
                    default:
                        ToastNotifier.Error("Xóa màu thất bại.");
                        break;
                    case System.Net.HttpStatusCode.InternalServerError:
                        ToastNotifier.Error("Xóa màu thất bại: đã có lỗi máy chủ xảy ra.");
                        break;
                    case System.Net.HttpStatusCode.Unauthorized:
                    case System.Net.HttpStatusCode.Forbidden:
                        ToastNotifier.Error("Bạn không có đủ quyền hạn cần thiết.");
                        break;
                    case System.Net.HttpStatusCode.NotFound:
                        ToastNotifier.Warning("Không tìm thấy gì.");
                        break;
                    case System.Net.HttpStatusCode.Conflict:
                        ToastNotifier.Error("Xóa màu thất bại: đã có sản phẩm khác sử dụng danh mục này rồi.");
                        break;
                }
                Console.WriteLine(response.StatusCode);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
