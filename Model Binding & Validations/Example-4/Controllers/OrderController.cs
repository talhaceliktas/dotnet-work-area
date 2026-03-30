using Microsoft.AspNetCore.Mvc;

namespace Example_4.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet("order/{customerId}/{status?}")]
        public IActionResult Index(
            [FromRoute] int customerId,
            [FromQuery] string? status,
            [FromQuery] int? page            
        )
        {
            var json = new { customerId, status, page };

            return Json(json);
        }

    }
}
