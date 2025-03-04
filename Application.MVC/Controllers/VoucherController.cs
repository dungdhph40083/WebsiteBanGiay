﻿using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.OutputCaching;
using Newtonsoft.Json;
using NuGet.Protocol;
using System;
using System.Net.Http.Headers;

namespace Application.MVC.Controllers
{
    public class VoucherController : Controller
    {
        HttpClient Client = new HttpClient();
        private readonly INotyfService ToastNotifier;
        public VoucherController(INotyfService ToastNotifier)
        {
            this.ToastNotifier = ToastNotifier;
        }
        [ResponseCache(NoStore = true, Duration = 0)]
        // GET: VoucherController
        [HttpGet]
        public async Task<ActionResult> Index(string SortByTime, string SortByLook)
        {
            string URL = $@"https://localhost:7187/api/Voucher";
            var Response = await Client.GetFromJsonAsync<List<Voucher>>(URL);

            switch (SortByTime)
            {
                case VoucherFilters.VOUCHER_COMING_SOON:
                    Response = Response?.Where(Flt => Flt.StartingAt.GetValueOrDefault() > DateTime.UtcNow).ToList();
                    break;
                case VoucherFilters.VOUCHER_ONGOING:
                    Response = Response?.Where
                        (Flt => Flt.StartingAt.GetValueOrDefault() <= DateTime.UtcNow && DateTime.UtcNow <= Flt.EndingAt.GetValueOrDefault())
                        .ToList();
                    break;
                case VoucherFilters.VOUCHER_DIED:
                    Response = Response?.Where(Flt => Flt.EndingAt.GetValueOrDefault() < DateTime.UtcNow).ToList();
                    break;
                default:
                    break;
            }

            switch (SortByLook)
            {
                case VoucherFilters.VOUCHER_PUBLIC:
                    Response = Response?.Where(Flt => Flt.Status == (byte)VoucherStatus.Disabled || Flt.Status == (byte)VoucherStatus.Active).ToList();
                    break;
                case VoucherFilters.VOUCHER_PRIVATE:
                    Response = Response?.Where(Flt => Flt.Status == (byte)VoucherStatus.DisabledPrivate || Flt.Status == (byte)VoucherStatus.ActivePrivate).ToList();
                    break;
                default:
                    break;
            }

            ViewData["FilterValueByTime"] = SortByTime;
            ViewData["FilterValueByLook"] = SortByLook;

            return View(Response);
        }

        // GET: VoucherController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(Guid ID)
        {
            string URL = $@"https://localhost:7187/api/Voucher/{ID}";
            var Response = await Client.GetFromJsonAsync<Voucher>(URL);
            return View(Response);
        }

        // GET: VoucherController/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await FetchInfoPlsPlsPlsPls();
            return View();
        }

        private async Task FetchInfoPlsPlsPlsPls()
        {
            // A pattern could have improved this but whatever
            // this project is already starting to get confusing

            string URL_Catgs = $@"https://localhost:7187/api/Category/";
            string URL_Prods = $@"https://localhost:7187/api/Product/";

            var CatgsList = await Client.GetFromJsonAsync<List<Category>>(URL_Catgs);
            var ProdsList = await Client.GetFromJsonAsync<List<Product>>(URL_Prods);

            var CatgsItems = CatgsList?
                .Select(Cb => new SelectListItem
                { Text = Cb.CategoryName, Value = Cb.CategoryID.ToString() }).ToList();
            var ProdsItems = ProdsList?
                .Select(Cb => new SelectListItem
                { Text = Cb.Name, Value = Cb.ProductID.ToString() }).ToList();

            ViewBag.Catgs = CatgsItems;
            ViewBag.Prods = ProdsItems;
        }

        // POST: VoucherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VoucherDTO Input)
        {
            await FetchInfoPlsPlsPlsPls();
            try
            {
                string URL = $@"https://localhost:7187/api/Voucher";
                var Response = await Client.PostAsJsonAsync(URL, Input);
                if (Response.IsSuccessStatusCode)
                {
                    ToastNotifier.Success("Tạo Voucher mới thành công!");
                }
                else
                {
                    switch (Response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.BadRequest:
                        default:
                            ToastNotifier.Error("Tạo Voucher mới thất bại.");
                            break;
                        case System.Net.HttpStatusCode.InternalServerError:
                            ToastNotifier.Error("Tạo Voucher mới thất bại: đã có lỗi máy chủ xảy ra.");
                            break;
                        case System.Net.HttpStatusCode.Unauthorized:
                        case System.Net.HttpStatusCode.Forbidden:
                            ToastNotifier.Error("Bạn không có đủ quyền hạn cần thiết.");
                            break;
                        case System.Net.HttpStatusCode.NotFound:
                            ToastNotifier.Warning("Không tìm thấy gì.");
                            break;
                        case System.Net.HttpStatusCode.Conflict:
                            ToastNotifier.Error("Tạo Voucher mới thất bại: đã có Voucher khác sử dụng mã này rồi.");
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
                TempData["Error"] = $"Đã có lỗi xảy ra! Lỗi:\n{Msg.Message} ({Msg.HResult})";
                return View(Input);
            }
        }

        // GET: VoucherController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(Guid ID)
        {

            await FetchInfoPlsPlsPlsPls();
            string URL = $@"https://localhost:7187/api/Voucher/{ID}";
            var Response = await Client.GetFromJsonAsync<VoucherDTO>(URL);

            return View(Response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: VoucherController/Edit/5
        public async Task<ActionResult> Edit(Guid ID, VoucherDTO NewInput)
        {
            await FetchInfoPlsPlsPlsPls();
            try
            {
                string URL = $@"https://localhost:7187/api/Voucher/{ID}";
                var Response = await Client.PutAsJsonAsync(URL, NewInput);

                if (Response.IsSuccessStatusCode)
                {
                    ToastNotifier.Success("Sửa Voucher thành công!");
                }
                else
                {
                    switch (Response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.BadRequest:
                        default:
                            ToastNotifier.Error("Sửa Voucher thất bại.");
                            break;
                        case System.Net.HttpStatusCode.InternalServerError:
                            ToastNotifier.Error("Sửa Voucher thất bại: đã có lỗi máy chủ xảy ra.");
                            break;
                        case System.Net.HttpStatusCode.Unauthorized:
                        case System.Net.HttpStatusCode.Forbidden:
                            ToastNotifier.Error("Bạn không có đủ quyền hạn cần thiết.");
                            break;
                        case System.Net.HttpStatusCode.NotFound:
                            ToastNotifier.Warning("Không tìm thấy gì.");
                            break;
                        case System.Net.HttpStatusCode.Conflict:
                            ToastNotifier.Error("Sửa Voucher thất bại: đã có Voucher khác sử dụng mã Voucher này rồi.");
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
                TempData["Error"] = $"Đã có lỗi xảy ra! Lỗi:\n{Msg.Message} ({Msg.HResult})";
                return View(NewInput);
            }
        }

        // POST: VoucherController/Delete/5
        public async Task<ActionResult> Delete(Guid ID)
        {

            try
            {
                string URL = $@"https://localhost:7187/api/Voucher/{ID}";
                var Response = await Client.DeleteAsync(URL);

                if (Response.IsSuccessStatusCode)
                {
                    ToastNotifier.Success("Xóa Voucher thành công!");
                }
                else
                {
                    switch (Response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.BadRequest:
                        default:
                            ToastNotifier.Error("Xóa Voucher thất bại.");
                            break;
                        case System.Net.HttpStatusCode.InternalServerError:
                            ToastNotifier.Error("Xóa Voucher thất bại: đã có lỗi máy chủ xảy ra.");
                            break;
                        case System.Net.HttpStatusCode.Unauthorized:
                        case System.Net.HttpStatusCode.Forbidden:
                            ToastNotifier.Error("Bạn không có đủ quyền hạn cần thiết.");
                            break;
                        case System.Net.HttpStatusCode.NotFound:
                            ToastNotifier.Warning("Không tìm thấy gì.");
                            break;
                        case System.Net.HttpStatusCode.Conflict:
                            ToastNotifier.Error("Xóa Voucher thất bại: Voucher này đã được sử dụng ít nhất một lần trong sự kiện trước đó.");
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
                TempData["Error"] = $"Đã có lỗi xảy ra! Lỗi:\n{Msg.Message} ({Msg.HResult})";
                return View();
            }
        }


        public async Task<ActionResult> ToggleStatus(Guid ID)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/Voucher/ToggleStatus/{ID}";
                var Response = await Client.PatchAsync(URL, null);

                if (Response.IsSuccessStatusCode)
                {
                    var NewVoucherState = JsonConvert.DeserializeObject<Voucher>(await Response.Content.ReadAsStringAsync());
                    switch (NewVoucherState?.Status)
                    {
                        case (byte)VoucherStatus.Active:
                        case (byte)VoucherStatus.ActivePrivate:
                            {
                                if (NewVoucherState.StartingAt.GetValueOrDefault() <= DateTime.UtcNow && DateTime.UtcNow <= NewVoucherState.EndingAt.GetValueOrDefault())
                                {
                                    ToastNotifier.Success("Đã tiếp tục Voucher thành công!");
                                }
                                else
                                {
                                    ToastNotifier.Success("Đổi trạng thái thành công!");
                                }
                                break;
                            }
                        case (byte)VoucherStatus.Disabled:
                        case (byte)VoucherStatus.DisabledPrivate:
                            {
                                if (NewVoucherState.StartingAt.GetValueOrDefault() <= DateTime.UtcNow && DateTime.UtcNow <= NewVoucherState.EndingAt.GetValueOrDefault())
                                {
                                    ToastNotifier.Success("Đã tạm dừng Voucher thành công!");
                                }
                                else
                                {
                                    ToastNotifier.Success("Đã kết thúc Voucher thành công!");
                                }
                                break;
                            }
                        default:
                            ToastNotifier.Success("Đổi trạng thái thành công!");
                            break;
                    }
                }
                else
                {
                    ToastNotifier.Error("Đổi trạng thái thất bại.");
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                TempData["Error"] = $"Đã có lỗi xảy ra! Lỗi:\n{Msg.Message} ({Msg.HResult})";
                return View();
            }
        }

        public async Task<ActionResult> StopThisRightNow(Guid ID)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/Voucher/StopVoucher/{ID}";
                var Response = await Client.PatchAsync(URL, null);

                if (Response.IsSuccessStatusCode)
                {
                    ToastNotifier.Success("Đã kết thúc Voucher thành công!");
                }
                else
                {
                    ToastNotifier.Error("Kết thúc Voucher thất bại.");
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                TempData["Error"] = $"Đã có lỗi xảy ra! Lỗi:\n{Msg.Message} ({Msg.HResult})";
                return View();
            }
        }
    }
}
