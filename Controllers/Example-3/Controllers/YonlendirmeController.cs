using Microsoft.AspNetCore.Mvc;

namespace Example_3.Controllers
{
    [ApiController]
    [Route("yon")]
    public class YonlendirmeController : ControllerBase
    {
        [Route("gecici")]
        public IActionResult GeciciYonlendir()
        {
            return RedirectToAction("Index", "Katalog");
        }

        [Route("kalici")]
        public IActionResult KaliciYonlendir()
        {
            return RedirectToActionPermanent("Hakkimizda", "Katalog");
        }

        [Route("local")]
        public IActionResult LocalYonlendir()
        {
            return LocalRedirect("katalog");
        }

        [Route("dis")]
        public IActionResult DisYonlendir()
        {
            return Redirect(@"https://example.com");
        }
    }
}
