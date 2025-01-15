using Application.Data.DTOs;
using Application.Data.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Net.Http.Headers;

namespace Application.MVC.Controllers
{
    public class SizeController : Controller
    {
        private readonly INotyfService ToastNotifier;
        HttpClient Client = new HttpClient();

        public SizeController(INotyfService ToastNotifier)
        {
            this.ToastNotifier = ToastNotifier;
        }
        // GET: SizeController

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            string URL = $@"https://localhost:7187/api/Size";
            var Response = await Client.GetFromJsonAsync<List<Size>>(URL);
            return View(Response);
        }

        // GET: SizeController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(Guid ID)
        {

            string URL = $@"https://localhost:7187/api/Size/{ID}";
            var Response = await Client.GetFromJsonAsync<Size>(URL);
            return View(Response);
        }

        // GET: SizeController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: SizeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SizeDTO Input)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/Size";
                var Response = await Client.PostAsJsonAsync(URL, Input);

                if (Response.IsSuccessStatusCode)
                {
                    ToastNotifier.Success("Thêm kích cỡ mới thành công!");
                }
                else
                {
                    switch (Response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.BadRequest:
                        default:
                            ToastNotifier.Error("Thêm kích cỡ mới thất bại.");
                            break;
                        case System.Net.HttpStatusCode.InternalServerError:
                            ToastNotifier.Error("Thêm kích cỡ mới thất bại: đã có lỗi máy chủ xảy ra.");
                            break;
                        case System.Net.HttpStatusCode.Unauthorized:
                        case System.Net.HttpStatusCode.Forbidden:
                            ToastNotifier.Error("Bạn không có đủ quyền hạn cần thiết.");
                            break;
                        case System.Net.HttpStatusCode.NotFound:
                            ToastNotifier.Warning("Không tìm thấy gì.");
                            break;
                        case System.Net.HttpStatusCode.Conflict:
                            ToastNotifier.Error("Thêm kích cỡ mới thất bại: đã có sản phẩm khác sử dụng danh mục này rồi.");
                            break;
                    }
                    return View(Input);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                return View();
            }
        }

        // GET: SizeController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(Guid ID)
        {
            string URL = $@"https://localhost:7187/api/Size/{ID}";
            var Response = await Client.GetFromJsonAsync<SizeDTO>(URL);

            return View(Response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: SizeController/Edit/5
        public async Task<ActionResult> Edit(Guid ID, SizeDTO NewInput)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/Size/{ID}";
                var Response = await Client.PutAsJsonAsync(URL, NewInput);

                if (Response.IsSuccessStatusCode)
                {
                    ToastNotifier.Success("Sửa kích cỡ thành công!");
                }
                else
                {
                    switch (Response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.BadRequest:
                        default:
                            ToastNotifier.Error("Sửa kích cỡ thất bại.");
                            break;
                        case System.Net.HttpStatusCode.InternalServerError:
                            ToastNotifier.Error("Sửa kích cỡ thất bại: đã có lỗi máy chủ xảy ra.");
                            break;
                        case System.Net.HttpStatusCode.Unauthorized:
                        case System.Net.HttpStatusCode.Forbidden:
                            ToastNotifier.Error("Bạn không có đủ quyền hạn cần thiết.");
                            break;
                        case System.Net.HttpStatusCode.NotFound:
                            ToastNotifier.Warning("Không tìm thấy gì.");
                            break;
                        case System.Net.HttpStatusCode.Conflict:
                            ToastNotifier.Error("Sửa kích cỡ thất bại: đã có sản phẩm khác sử dụng danh mục này rồi.");
                            break;
                    }
                    return View(NewInput);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                return View();
            }
        }

        // POST: SizeController/Delete/5
        public async Task<ActionResult> Delete(Guid ID)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/Size/{ID}";
                var Response = await Client.DeleteAsync(URL);

                if (Response.IsSuccessStatusCode)
                {
                    ToastNotifier.Success("Xóa kích cỡ thành công!");
                }
                else
                {
                    switch (Response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.BadRequest:
                        default:
                            ToastNotifier.Error("Xóa kích cỡ thất bại.");
                            break;
                        case System.Net.HttpStatusCode.InternalServerError:
                            ToastNotifier.Error("Xóa kích cỡ thất bại: đã có lỗi máy chủ xảy ra.");
                            break;
                        case System.Net.HttpStatusCode.Unauthorized:
                        case System.Net.HttpStatusCode.Forbidden:
                            ToastNotifier.Error("Bạn không có đủ quyền hạn cần thiết.");
                            break;
                        case System.Net.HttpStatusCode.NotFound:
                            ToastNotifier.Warning("Không tìm thấy gì.");
                            break;
                        case System.Net.HttpStatusCode.Conflict:
                            ToastNotifier.Error("Xóa kích cỡ thất bại: đã có sản phẩm khác sử dụng danh mục này rồi.");
                            break;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                return View();
            }
        }
    }
}
