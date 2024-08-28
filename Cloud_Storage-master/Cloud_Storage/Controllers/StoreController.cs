using Cloud_Storage.Models;
using Cloud_Storage.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class StoreController : Controller
{
    private readonly TableStorageService _tableStorageService;

    public StoreController(TableStorageService tableStorageService)
    {
        _tableStorageService = tableStorageService;
    }

    // Customer Actions
    public async Task<IActionResult> CustomerIndex()
    {
        var customers = await _tableStorageService.GetAllCustomersAsync();
        return View(customers);
    }

    public IActionResult CreateCustomer()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(Customer customer)
    {
        customer.PartitionKey = "Customer";
        customer.RowKey = customer.CustomerId;

        await _tableStorageService.AddCustomerAsync(customer);
        return RedirectToAction("CustomerIndex");
    }

    public async Task<IActionResult> DeleteCustomer(string partitionKey, string rowKey)
    {
        await _tableStorageService.DeleteCustomerAsync(partitionKey, rowKey);
        return RedirectToAction("CustomerIndex");
    }

    // Product Actions
    public async Task<IActionResult> ProductIndex()
    {
        var products = await _tableStorageService.GetAllProductsAsync();
        return View(products);
    }

    public IActionResult CreateProduct()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(Product product)
    {
        product.PartitionKey = "Product";
        product.RowKey = product.ProductId;

        await _tableStorageService.AddProductAsync(product);
        return RedirectToAction("ProductIndex");
    }

    public async Task<IActionResult> DeleteProduct(string partitionKey, string rowKey)
    {
        await _tableStorageService.DeleteProductAsync(partitionKey, rowKey);
        return RedirectToAction("ProductIndex");
    }
}
