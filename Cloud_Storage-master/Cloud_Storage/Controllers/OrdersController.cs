using Cloud_Storage.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class OrdersController : Controller
{
    private readonly QueueService _queueService;

    public OrdersController(QueueService queueService)
    {
        _queueService = queueService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ProcessOrder(string orderDetails)
    {
        if (!string.IsNullOrEmpty(orderDetails))
        {
            string message = $"Processing order: {orderDetails}";
            await _queueService.SendMessageAsync(message);
            ViewBag.Message = "Order processing has been queued.";
        }

        return View("Index");
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage(string imageName)
    {
        if (!string.IsNullOrEmpty(imageName))
        {
            string message = $"Uploading image: {imageName}";
            await _queueService.SendMessageAsync(message);
            ViewBag.Message = "Image upload has been queued.";
        }

        return View("Index");
    }
}
