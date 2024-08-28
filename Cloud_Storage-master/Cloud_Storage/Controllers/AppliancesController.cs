using Cloud_Storage.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

public class AppliancesController : Controller
{
    private readonly TableStorageService _tableStorageService;

    public AppliancesController(TableStorageService tableStorageService)
    {
        _tableStorageService = tableStorageService;
    }

    public async Task<IActionResult> Index()
    {
        var appliances = await _tableStorageService.GetAllAppliancesAsync();
        return View(appliances);
    }

    // Action to Delete an Appliance
    [HttpPost]
    public async Task<IActionResult> DeleteAppliance(string partitionKey, string rowKey)
    {
        await _tableStorageService.DeleteApplianceAsync(partitionKey, rowKey);
        return RedirectToAction("Index");
    }

    public IActionResult AddAppliances()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddAppliances(Appliances appliance)
    {
        appliance.PartitionKey = "AppliancesPartition";
        appliance.RowKey = Guid.NewGuid().ToString();

        await _tableStorageService.AddApplianceAsync(appliance);
        return RedirectToAction("Index");
    }

    //public async Task<IActionResult> Delete(string partitionKey, string rowKey)
    //{
    //    await _tableStorageService.DeleteApplianceAsync(partitionKey, rowKey);
    //    return RedirectToAction("Index");
    //}

    public async Task<IActionResult> Details(string partitionKey, string rowKey)
    {
        var appliance = await _tableStorageService.GetApplianceAsync(partitionKey, rowKey);
        if (appliance == null)
        {
            return NotFound();
        }
        return View(appliance);
    }
}
