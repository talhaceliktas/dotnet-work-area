using Microsoft.AspNetCore.Mvc;
using ModelValidationExample.Controllers;
using ModelValidationExample.Models;
namespace ModelValidationExample.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost("register")]
        public IActionResult Index([Bind(nameof(Person.PersonName), nameof(Person.Email),
            nameof(Person.Password), nameof(Person.Password))] Person person)
        {
            if (!ModelState.IsValid)
            {
                string errors = string.Join("\n",
                    ModelState.Values.SelectMany(value => value.Errors)
                    .Select(err => err.ErrorMessage).ToList()
                    );

                return BadRequest(errors);
            }


            return Content(person.ToString());
        }
    }
}
