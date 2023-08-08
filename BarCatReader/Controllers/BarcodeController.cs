using BarCatReader.Models;
using IronBarCode;
using Microsoft.AspNetCore.Mvc;

namespace BarCatReader.Controllers
{
    public class BarcodeController : Controller
    {
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
            }
            catch { }

            return ReturnError("Не вдалося завантажити зображення.");
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
                var resultArray = await BarcodeReader.ReadAsync(stream);
                var res = resultArray.Count() > 0 ? new BarcodeModel(resultArray.First()) : null;

                if (Request.Headers.Accept == "application/json")
                    return Json(res);
                else
                {
                    ViewBag.Data = res;
                    return View("../Decoded");
                }
            }
            catch {}

            return ReturnError("Не вдалося декодувати зображення.");
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
