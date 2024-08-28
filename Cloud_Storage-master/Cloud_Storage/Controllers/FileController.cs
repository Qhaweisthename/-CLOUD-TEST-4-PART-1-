using Cloud_Storage.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

public class FileController : Controller
{
    private readonly AzureFileShareService _fileShareService;

    public FileController(AzureFileShareService fileShareService)
    {
        _fileShareService = fileShareService;
    }

    // Upload Contract or Log File
    public IActionResult Upload()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file, string directoryName)
    {
        if (file != null && file.Length > 0)
        {
            var fileName = Path.GetFileName(file.FileName);
            using var stream = file.OpenReadStream();
            await _fileShareService.UploadFileAsync(directoryName, fileName, stream);
            ViewBag.Message = $"File uploaded successfully to directory {directoryName}.";
        }

        return View();
    }

    // Download Contract or Log File
    public async Task<IActionResult> Download(string directoryName, string fileName)
    {
        var stream = await _fileShareService.DownloadFileAsync(directoryName, fileName);
        return File(stream, "application/octet-stream", fileName);
    }

    // Delete Contract or Log File
    [HttpPost]
    public async Task<IActionResult> Delete(string directoryName, string fileName)
    {
        await _fileShareService.DeleteFileAsync(directoryName, fileName);
        return RedirectToAction("Index");
    }

    // List Files (Optional)
    public IActionResult Index()
    {
        // Implementation to list files if needed.
        return View();
    }
}
