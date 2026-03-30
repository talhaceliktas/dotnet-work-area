using Example_4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Example_4.Controllers
{
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        [HttpPost("profile")]
        public IActionResult Index([Bind(nameof(UserProfile.Name), nameof(UserProfile.Email))] UserProfile model)
        { 
           return Ok(new { model, isAdmin = false, createdAt = DateTime.Now });
        }
    }
}
