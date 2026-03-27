using Microsoft.AspNetCore.Mvc;
using ModelValidationExample.Controllers;
using ModelValidationExample.Models;
namespace ModelValidationExample.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost("register")]
        public IActionResult Index(Person person)
        {
            return Content(person.ToString());
        }
    }
}
