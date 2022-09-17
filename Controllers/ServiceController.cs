using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using RemoveBg.Models;

namespace RemoveBg.Controllers;

public class ServiceController : Controller
{
    private readonly ILogger<ServiceController> _logger;
    private readonly HttpClient _client;
    private static List<Result>? results;

    public ServiceController(ILogger<ServiceController> logger)
    {
        _logger = logger;
        _client = new HttpClient();
    }

    public IActionResult RemoveBack() => View();

    [HttpPost]
    public async Task<IActionResult> RemoveBack(Photo photo)
    {
        MultipartFormDataContent form = new MultipartFormDataContent();
        var stream = photo.Image.OpenReadStream();
        var content = new StreamContent(stream);
        content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        {
            Name = "file",
            FileName = photo.Image.FileName
        };
        form.Add(content);
        var response = await _client.PostAsync("http://localhost:5000/", form);
        var image = Convert.ToBase64String(await response.Content.ReadAsByteArrayAsync());
        
        return View("ResultPhoto", new Result(){ Photo = "data:image/jpeg;base64," + image });
    }


}
