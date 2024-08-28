using Cloud_Storage.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

public class MediaController : Controller
{
    private readonly BlobService _blobService;

    public MediaController(BlobService blobService)
    {
        _blobService = blobService;
    }

    // Upload Action
    public IActionResult Upload()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            var fileName = Path.GetFileName(file.FileName);
            using var stream = file.OpenReadStream();
            var url = await _blobService.UploadAsync(stream, fileName);
            ViewBag.Message = $"File uploaded successfully. URL: {url}";
        }

        return View();
    }

    // Download Action
    public async Task<IActionResult> Download(string fileName)
    {
        var stream = await _blobService.DownloadBlobAsync(fileName);
        return File(stream, "application/octet-stream", fileName);
    }

    // Delete Action
    [HttpPost]
    public async Task<IActionResult> Delete(string fileName)
    {
        await _blobService.DeleteBlobAsync(fileName);
        return RedirectToAction("Index");
    }

    // List Files (Optional)
    public IActionResult Index()
    {
        // Implementation to list files if needed.
        return View();
    }
}
