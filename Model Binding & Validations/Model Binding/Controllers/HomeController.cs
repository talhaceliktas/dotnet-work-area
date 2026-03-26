using Microsoft.AspNetCore.Mvc;

namespace IActionResultExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("bookstore")]
        // Url: bookstore?bookId=10&isLoggedIn=true
        public IActionResult Index(int? bookId, bool? isLoggedIn)
        {
            // bookId should be supllied
            if (!bookId.HasValue)
            {
                //Response.StatusCode = 400;
                //return new BadRequestResult();
                return BadRequest("bookId is not supplied or empty");
               
            }

            // bookId can't be empty
            if (bookId < 0)
            {
                return BadRequest("bookId can't be less than or equal to zero.");
            }

            // bookId should be between 1 and 1000

            if (bookId <= 0) {
                return BadRequest("Book id can't be less then or equal zero.");
            }

            if (bookId > 1000)
            {
                //Response.StatusCode = 404;

                //return Content("Book id can't be greater than 1000");

                return NotFound("Book id can't be greater than 1000");
            }

            if (isLoggedIn != true)
            {
                //return Unauthorized("User must be logged in");
                return StatusCode(401, "User must be logged in");
            }

            return Content($"Book Id: {bookId}");
        }
    }
}
