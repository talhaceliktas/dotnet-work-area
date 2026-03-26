using Microsoft.AspNetCore.Mvc;

namespace Example_3.Controllers
{
    public class KitapController : Controller
    {
        [HttpGet("kitap")]
        public IActionResult Index(int? kitapId, bool? girisYapildi)
        {

            if (!Request.Query.ContainsKey("kitapId"))
            {
                return BadRequest("KitapId Zorunludur.");
            }

            if (kitapId <= 0)
            {
                return BadRequest("KitapId 0 dan buyuk olmali.");    
            }

            if (kitapId > 1000)
            {
                return NotFound("Bu Id'de kitap yok.");
            }

            if (!girisYapildi.HasValue) {
                return Unauthorized();
            }

            if (!girisYapildi ?? false)
            {
                return StatusCode(403, "Giriş yapılmış ama yetkin yok");
            }

            return Content("Kitap bilgileri ID: " + kitapId, "text/plain");

        }
    }
}
