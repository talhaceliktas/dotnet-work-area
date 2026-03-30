using Example_4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Example_4.Controllers
{
    [ApiController]
    [Route("api")]
    public class EventController : ControllerBase
    {
        [HttpPost("event")]
        public IActionResult Index([FromBody] EventRequest request)
        {
            return Ok(new {Success= true, request});
        }
    }
}
