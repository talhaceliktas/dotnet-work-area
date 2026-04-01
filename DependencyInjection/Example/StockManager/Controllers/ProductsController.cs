using Microsoft.AspNetCore.Mvc;

namespace StockManager.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
