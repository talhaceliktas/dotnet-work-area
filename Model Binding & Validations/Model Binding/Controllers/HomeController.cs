using Microsoft.AspNetCore.Mvc;

namespace IActionResultExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("bookstore")]
        public IActionResult Index()
        {
            // bookId should be supllied
            if (!Request.Query.ContainsKey("bookId"))
            {
                //Response.StatusCode = 400;
                //return new BadRequestResult();
                return BadRequest("bookId is not supplied");
               
            }

            // bookId can't be empty
            if (string.IsNullOrWhiteSpace(Request.Query["bookId"][0]))
            {
                return BadRequest("bookId can't be null or whitespace");
            }

            // bookId should be between 1 and 1000
            int bookId = Convert.ToInt32(ControllerContext.HttpContext.Request.Query["bookId"]);

            if (bookId <= 0) {
                return BadRequest("Book id can't be less then or equal zero.");
            }

            if (bookId > 1000)
            {
                //Response.StatusCode = 404;

                //return Content("Book id can't be greater than 1000");

                return NotFound("Book id can't be greater than 1000");
            }

            if (!Convert.ToBoolean(Request.Query["isLoggedIn"]))
            {
                //return Unauthorized("User must be logged in");
                return StatusCode(401, "User must be logged in");
            }

            //return new RedirectToActionResult("Books", "Store", new { }); // 302 - Found
            //return RedirectToAction("Books", "Store", new {id = bookId });


            //return new RedirectToActionResult("Books", "Store", new { }, true); // 302 - Moved Permanently
            //return RedirectToActionPermanent("Books", "Store", new { }); // 302 - Moved Permanently


            //return new LocalRedirectResult($"store/books/{bookId}");
            //return LocalRedirect($"store/books/{bookId}");

            //return new LocalRedirectResult($"store/books/{bookId}", true); // 302
            //return LocalRedirectPermanent($"store/books/{bookId}");

            //return Redirect($"store/books/{bookId}");
            return RedirectPermanent($"store/books/{bookId}");


        }
    }
}
