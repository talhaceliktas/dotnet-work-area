using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace CaptiveDependencyDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrderProcessingService  _orderProcessingService;

        public HomeController(IOrderProcessingService orderProcessingService)
        {
            _orderProcessingService = orderProcessingService;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return Ok(_orderProcessingService.ProcessPendingOrders());
        }
    }
}
