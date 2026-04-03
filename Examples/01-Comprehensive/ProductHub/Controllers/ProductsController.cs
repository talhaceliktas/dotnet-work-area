using Microsoft.AspNetCore.Mvc;
using ProductHub.Models;

namespace ProductHub.Controllers
{
    [Route("api")]
    public class ProductsController : Controller
    {
        [HttpGet("products")]
        public IActionResult Index()
        {
            return Ok(ProductStore.ProductList);
        }

        [HttpGet("products/{id:int:min(1)}")]
        public IActionResult GetProductById([FromRoute] int id)
        {
            Product? product = ProductStore.ProductList.FirstOrDefault(x => x.Id == id);

            if (product != null) {
                return Ok(product);
            }
            return NotFound();

        }

        [HttpGet("products/search")]
        public IActionResult SearchProduct([FromQuery(Name ="name")] string? keyword)
        {
            return View();
        }

        [HttpPost("products")]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost("products/report/{year:int:min(2020)}/{month:regex(^(jan|feb)}")]
        public IActionResult GetReportProduct()
        {
            return View();
        }
    }
}
