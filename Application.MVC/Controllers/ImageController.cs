//using Application.Data.DTOs;
//using Application.Data.Models;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using NuGet.Protocol;
//using System.Net.Http.Headers;
//using System.Net.Mime;

//namespace Application.MVC.Controllers
//{
//    public class ImageController : Controller
//    {
//        HttpClient Client = new HttpClient();
//        // GET: ImageController

//        [HttpGet]
//        public async Task<ActionResult> Index()
//        {
//            string URL = $@"https://localhost:7187/api/Image";
//            var Response = await Client.GetFromJsonAsync<List<Image>>(URL);
//            return View(Response);
//        }

//        // GET: ImageController/Details/5
//        [HttpGet]
//        public async Task<ActionResult> Details(Guid ID)
//        {
//            string URL = $@"https://localhost:7187/api/Image/{ID}";
//            var Response = await Client.GetFromJsonAsync<Image>(URL);
//            if (Response != null && Response.ImageFileName != null)
//            {
//                ViewData["URLFetchImg"] = $@"https://localhost:7187/Images/{Response.ImageFileName}";
//            }
//            return View(Response);
//        }

//        // GET: ImageController/Create
//        [HttpGet]
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: ImageController/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create(ImageDTO Input, IFormFile ImageFile)
//        {
//            try
//            {
//                string URL = $@"https://localhost:7187/api/Image";
//                var ImageStream = new StreamContent(ImageFile.OpenReadStream());

//                MultipartFormDataContent Contents = new()
//                {
//                    { new StringContent(Input.ImageName!), nameof(Input.ImageName) },
//                    { new StringContent(Input.ImageDescription!), nameof(Input.ImageDescription) },
//                    { new StringContent(Input.Status.ToString()), nameof(Input.Status) },
//                    { ImageStream, nameof(ImageFile), ImageFile.FileName }
//                };

//                Contents.Add(ImageStream, nameof(ImageFile), ImageFile.FileName);

//                var Response = await Client.PostAsync(URL, Contents);
//                return RedirectToAction(nameof(Index));
//            }
//            catch (Exception Msg)
//            {
//                Console.ForegroundColor = ConsoleColor.Red;
//                Console.WriteLine(Msg.Message);
//                Console.ForegroundColor = ConsoleColor.Gray;
//                return View();
//            }
//        }

//        // GET: ImageController/Edit/5
//        [HttpGet]
//        public async Task<ActionResult> Edit(Guid ID)
//        {
//            string URL = $@"https://localhost:7187/api/Image/{ID}";
//            var Response = await Client.GetFromJsonAsync<ImageDTO>(URL);

//            return View(Response);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        // POST: ImageController/Edit/5
//        public async Task<ActionResult> Edit(Guid ID, ImageDTO NewInput)
//        {
//            try
//            {
//                string URL = $@"https://localhost:7187/api/Image/{ID}";
//                var Response = await Client.PutAsJsonAsync(URL, NewInput);
//                return RedirectToAction(nameof(Index));
//            }
//            catch (Exception Msg)
//            {
//                Console.ForegroundColor = ConsoleColor.Red;
//                Console.WriteLine(Msg.Message);
//                Console.ForegroundColor = ConsoleColor.Gray;
//                return View();
//            }
//        }

//        // POST: ImageController/Delete/5
//        public async Task<ActionResult> Delete(Guid ID)
//        {
//            try
//            {
//                string URL = $@"https://localhost:7187/api/Image/{ID}";
//                var Response = await Client.DeleteAsync(URL);
//                return RedirectToAction(nameof(Index));
//            }
//            catch (Exception Msg)
//            {
//                Console.ForegroundColor = ConsoleColor.Red;
//                Console.WriteLine(Msg.Message);
//                Console.ForegroundColor = ConsoleColor.Gray;
//                return View();
//            }
//        }
//    }
//}
