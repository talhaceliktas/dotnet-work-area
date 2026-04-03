using Microsoft.AspNetCore.Mvc;

namespace ProductHub.Controllers
{
    [Route("api")]
    public class ProductsController : Controller
    {
        [HttpGet("products")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("products/{id:int:min(1)}")]
        public IActionResult GetProduct([FromRoute] int id)
        {
            return View();
        }

        [HttpGet("products/search")]
        public IActionResult SearchProduct([FromQuery(Name ="name")] string? keyword)
        {
            return View();
        }

        [HttpPost("products")]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost("products/report/{year:int:min(2020)}/{month:regex(^(jan|feb)}")]
        public IActionResult RegexProduct()
        {
            return View();
        }
    }
}
