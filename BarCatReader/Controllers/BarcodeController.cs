using BarCatReader.Models;
using Microsoft.AspNetCore.Mvc;
using SkiaSharp;
using ZXing;
using ZXing.SkiaSharp;

namespace BarCatReader.Controllers
{
    public class BarcodeController : Controller
    {
        BarcodeReader reader = new BarcodeReader();

        HttpClient httpClient;
        public BarcodeController(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        [HttpGet("/decode")]
        public async Task<IActionResult> DecodeFromUrl(string url, CancellationToken cancellationToken = default)
        {
            if (url == null) 
                return ReturnError("Отримано порожнє посилання. Декодування неможливе.");

            try
            {
                var result = await httpClient.GetAsync(url, cancellationToken);

                if (result.IsSuccessStatusCode)
                {
                    using (Stream stream = result.Content.ReadAsStream())
                        return await DecodeFromStream(stream);
                }
                else
                    return ReturnError("Не вдалося завантажити зображення. Отримано помилку від серверу.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ReturnError("Не вдалося завантажити зображення.");
            }
        }

        [HttpPost("/decode")]
        public async Task<IActionResult> DecodeFromFile(IFormFile file, CancellationToken cancellationToken = default)
        {
            if (file == null) 
                return ReturnError("Отримано порожній файл. Декодування неможливе.");

            using (Stream stream = file.OpenReadStream())
                return await DecodeFromStream(stream);
        }

        private async Task<IActionResult> DecodeFromStream(Stream stream)
        {
            try
            {
                var image = SKBitmap.Decode(stream);
                var res = reader.Decode(new SKBitmapLuminanceSource(image));

                if(res==null) return ReturnError("Не вдалося декодувати зображення.");

                var resModel = new BarcodeModel(res);

                if (Request.Headers.Accept == "application/json")
                    return Json(resModel);
                else
                {
                    ViewBag.Data = resModel;
                    return View("../Decoded");
                }
            }
            catch(Exception e) {
                Console.WriteLine(e);
                return ReturnError();
            }
        }

        private IActionResult ReturnError(string error = "Йосип драний! Сталась халепа...")
        {
            ViewBag.Data = error;
            if (Request.Headers.Accept == "application/json")
                return Json(new { Error = error });
            else
                return View("../Decoded");
        }

    }
}
