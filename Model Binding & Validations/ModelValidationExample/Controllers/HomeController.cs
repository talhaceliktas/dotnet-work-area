using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelValidationExample.Controllers;
using ModelValidationExample.CustomModelBinders;
using ModelValidationExample.Models;
namespace ModelValidationExample.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost("register")]
        //[Bind(nameof(Person.PersonName), nameof(Person.Email), nameof(Person.Password), nameof(Person.Password))]
        //[ModelBinder(typeof(PersonModelBinder))]
        public IActionResult Index(Person person, [FromHeader(Name = "User-Agent")] string UserAgent)
        {
            if (!ModelState.IsValid)
            {
                string errors = string.Join("\n",
                    ModelState.Values.SelectMany(value => value.Errors)
                    .Select(err => err.ErrorMessage).ToList()
                    );

                return BadRequest(errors);
            }


            return Content($"{person}, {UserAgent}");
        }
    }
}
