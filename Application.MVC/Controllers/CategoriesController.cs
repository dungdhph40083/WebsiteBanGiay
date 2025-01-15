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
    public class CategoriesController : Controller
    {
        private readonly INotyfService ToastNotifier;
        HttpClient client = new HttpClient();

        public CategoriesController(INotyfService ToastNotifier)
        {
            this.ToastNotifier = ToastNotifier;
        }
        
        public ActionResult Index()
        {
            string requestURL = $@"https://localhost:7187/api/Category";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<List<Category>>(response);

            return View(data);
        }
        public ActionResult Details(Guid id)
        {
            string requestURL = $@"https://localhost:7187/api/Category/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<Category>(response);
            return View(data);
        }

        // GET: CategoriesController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public async Task<ActionResult> Create(CategoryDTO categoryDTO)
        {
  
            try
            {
                string requestURL = $"https://localhost:7187/api/Category";
                var response = await client.PostAsJsonAsync(requestURL, categoryDTO);

                if (response.IsSuccessStatusCode)
                {
                    ToastNotifier.Success("Thêm danh mục mới thành công!");
                }
                else
                {
                    switch (response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.BadRequest:
                        default:
                            ToastNotifier.Error("Thêm danh mục mới thất bại.");
                            break;
                        case System.Net.HttpStatusCode.InternalServerError:
                            ToastNotifier.Error("Thêm danh mục mới thất bại: đã có lỗi máy chủ xảy ra.");
                            break;
                        case System.Net.HttpStatusCode.Unauthorized:
                        case System.Net.HttpStatusCode.Forbidden:
                            ToastNotifier.Error("Bạn không có đủ quyền hạn cần thiết.");
                            break;
                        case System.Net.HttpStatusCode.NotFound:
                            ToastNotifier.Error("Không tìm thấy gì.");
                            break;
                        case System.Net.HttpStatusCode.Conflict:
                            ToastNotifier.Error("Tên của danh mục không được trùng với các danh mục khác!");
                            break;
                    }
                    return View(categoryDTO);
                }

            return RedirectToAction("Index");
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                return View();
            }
        }

        // GET: CategoriesController/Edit/5
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            string requestURL = $"https://localhost:7187/api/Category/{id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<CategoryDTO>(response);
            return View(data);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, CategoryDTO categoryDTO)
        {
            string requestURL = $@"https://localhost:7187/api/Category/{id}";
            var response = await client.PutAsJsonAsync(requestURL, categoryDTO);

            if (response.IsSuccessStatusCode)
            {
                ToastNotifier.Success("Sửa danh mục thành công!");
            }
            else
            {
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.BadRequest:
                    default:
                        ToastNotifier.Error("Sửa danh mục thất bại.");
                        break;
                    case System.Net.HttpStatusCode.InternalServerError:
                        ToastNotifier.Error("Sửa danh mục thất bại: đã có lỗi máy chủ xảy ra.");
                        break;
                    case System.Net.HttpStatusCode.Unauthorized:
                    case System.Net.HttpStatusCode.Forbidden:
                        ToastNotifier.Error("Bạn không có đủ quyền hạn cần thiết.");
                        break;
                    case System.Net.HttpStatusCode.NotFound:
                        ToastNotifier.Error("Không tìm thấy gì.");
                        break;
                    case System.Net.HttpStatusCode.Conflict:
                        ToastNotifier.Error("Tên của danh mục không được trùng với các danh mục khác!");
                        break;
                }
                return View(categoryDTO);
            }

            return RedirectToAction(nameof(Index));
        }


        public ActionResult Delete(Guid id)
        {
            try
            {
            string requestURL = $@"https://localhost:7187/api/Category/{id}";
            var response = client.DeleteAsync(requestURL).Result;

                if (response.IsSuccessStatusCode)
                {
                    ToastNotifier.Success("Xóa danh mục thành công!");
                }
                else
                {
                    switch (response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.BadRequest:
                        default:
                            ToastNotifier.Error("Xóa danh mục thất bại.");
                            break;
                        case System.Net.HttpStatusCode.InternalServerError:
                            ToastNotifier.Error("Xóa danh mục thất bại: đã có lỗi máy chủ xảy ra.");
                            break;
                        case System.Net.HttpStatusCode.Unauthorized:
                        case System.Net.HttpStatusCode.Forbidden:
                            ToastNotifier.Error("Bạn không có đủ quyền hạn cần thiết.");
                            break;
                        case System.Net.HttpStatusCode.NotFound:
                            ToastNotifier.Warning("Không tìm thấy gì.");
                            break;
                        case System.Net.HttpStatusCode.Conflict:
                            ToastNotifier.Error("Xóa danh mục thất bại: đã có sản phẩm khác sử dụng danh mục này rồi.");
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
