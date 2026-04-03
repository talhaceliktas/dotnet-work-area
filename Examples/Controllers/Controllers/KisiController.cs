using Example_3.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Example_3.Controllers
{
    [ApiController]

    public class KisiController : ControllerBase
    {
        [HttpGet("kisi")]
        public IActionResult Index()
        {
            Kisi kisi = new Kisi(Guid.NewGuid(), "Talha", "Soyad", 22);

            return Ok(kisi);
        }
    }
}
