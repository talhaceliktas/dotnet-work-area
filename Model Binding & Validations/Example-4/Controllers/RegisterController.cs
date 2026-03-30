using Example_4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Example_4.Controllers
{
    [ApiController]
    [Route("api")]
    public class RegisterController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Index([FromBody] RegisterRequest model)
        {
            return Ok(new { message = "Registered", user = model.Username });
        }
    }
}   
