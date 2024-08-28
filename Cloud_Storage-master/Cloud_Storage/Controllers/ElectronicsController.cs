using Cloud_Storage.Models;
using Cloud_Storage.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

public class ElectronicsController : Controller
{
    private readonly BlobService _blobService;
    private readonly TableStorageService _tableStorageService;

    public ElectronicsController(BlobService blobService, TableStorageService tableStorageService)
    {
        _blobService = blobService;
        _tableStorageService = tableStorageService;
    }

    [HttpGet]
    public IActionResult AddElectronics()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddElectronics(Electronics electronics, IFormFile file)
    {
        if (file != null)
        {
            using var stream = file.OpenReadStream();
            var imageUrl = await _blobService.UploadAsync(stream, file.FileName);
            electronics.ImageUrl = imageUrl;
        }

        if (ModelState.IsValid)
        {
            electronics.PartitionKey = "ElectronicsPartition"; // Updated partition key to match the context
            electronics.RowKey = Guid.NewGuid().ToString();
            await _tableStorageService.AddElectronicsAsync(electronics); // Updated method call to AddElectronicsAsync
            return RedirectToAction("Index");
        }
        return View(electronics);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteElectronics(string partitionKey, string rowKey, Electronics electronics)
    {
        if (electronics != null && !string.IsNullOrEmpty(electronics.ImageUrl))
        {
            // Delete the associated blob image
            await _blobService.DeleteBlobAsync(electronics.ImageUrl);
        }
        // Delete Table entity
        await _tableStorageService.DeleteElectronicsAsync(partitionKey, rowKey); // Updated method call to DeleteElectronicsAsync

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Index()
    {
        var electronicsList = await _tableStorageService.GetAllElectronicsAsync(); // Updated method call to GetAllElectronicsAsync
        return View(electronicsList);
    }
}
