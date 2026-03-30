using Microsoft.AspNetCore.Mvc;

namespace Example_4.Controllers
{
    [ApiController]
    public class SecureController : ControllerBase
    {


        [HttpGet("gizli-veri")]
        public IActionResult GizliVeriyiGetir([FromHeader(Name = "X-Client-ID")] string clientId)
        {
            if (clientId != "VALID-123")
                return Unauthorized("Geçersiz istemci kimliği");

            return Ok("Gizli verilere ulaştın!");
        }
    }
}
