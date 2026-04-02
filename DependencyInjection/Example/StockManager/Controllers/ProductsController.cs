using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace StockManager.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductStockService _productStockService;

        public ProductsController(IProductStockService productStockService)
        {
            _productStockService = productStockService;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return View(_productStockService.GetAllProducts());
        }

        [HttpGet("detail/{id}")]
        public IActionResult Detail([FromRoute] int id)
        {
            return Ok(_productStockService.IsInStock(id));
        }
    }
}
