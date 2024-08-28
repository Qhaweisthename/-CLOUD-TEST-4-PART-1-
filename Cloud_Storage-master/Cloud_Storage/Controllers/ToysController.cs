using Cloud_Storage.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Cloud_Storage.Services;

public class ToysController : Controller
{
    private readonly TableStorageService _tableStorageService;
    private readonly QueueService _queueService;

    public ToysController(TableStorageService tableStorageService, QueueService queueService)
    {
        _tableStorageService = tableStorageService;
        _queueService = queueService;
    }

    // Action to display all toys
    public async Task<IActionResult> Index()
    {
        var toys = await _tableStorageService.GetAllToysAsync();
        return View(toys);
    }

    // Action to create a new toy entry
    public IActionResult Create()
    {
        return View();
    }

    // Action to handle the form submission and add the toy
    [HttpPost]
    public async Task<IActionResult> Create(Toys toy)
    {
        if (ModelState.IsValid)
        {
            toy.PartitionKey = "ToysPartition";
            toy.RowKey = Guid.NewGuid().ToString();
            await _tableStorageService.AddToyAsync(toy);

            // MessageQueue
            string message = $"New toy added: {toy.Product_Name} with ID {toy.Product_Id}";
            await _queueService.SendMessageAsync(message);

            return RedirectToAction("Index");
        }

        return View(toy);
    }

    // Action to handle deleting a toy
    public async Task<IActionResult> Delete(string partitionKey, string rowKey)
    {
        await _tableStorageService.DeleteToyAsync(partitionKey, rowKey);
        return RedirectToAction("Index");
    }

    // Action to view details of a specific toy
    public async Task<IActionResult> Details(string partitionKey, string rowKey)
    {
        var toy = await _tableStorageService.GetToyAsync(partitionKey, rowKey);
        if (toy == null)
        {
            return NotFound();
        }
        return View(toy);
    }
}
