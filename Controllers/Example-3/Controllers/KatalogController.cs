using Microsoft.AspNetCore.Mvc;

namespace Example_3.Controllers
{
    [Controller]
    public class KatalogController : Controller
    {
        [HttpGet("/")]
        [HttpGet("/katalog")]
        public IActionResult Index()
        {
            return Content("Katalog Ana Sayfa", "text/plain");
        }

        [HttpGet("katalog/hakkimizda")]
        public IActionResult Hakkimizda()
        {
            return Content("<h1>Hakkimizda</h1>", "text/html");
        }

        [HttpGet("katalog/iletisim/{telefon:regex(^\\d{{10}}$)}")]
        public IActionResult Iletisim(string telefon)
        {
            return Content(telefon, "text/plain");
        }

    }
}
