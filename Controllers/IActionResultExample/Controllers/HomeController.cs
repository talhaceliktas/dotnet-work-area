using Microsoft.AspNetCore.Mvc;

namespace IActionResultExample.Controllers
{
    public class HomeController : Controller
    {
        public ContentResult Index()
        {
            // bookId should be supllied
            if (!Request.Query.ContainsKey("bookId"))
            {
                return Content("bookId is not supplied");
            }

            // bookId can't be empty
            if (string.IsNullOrWhiteSpace(Request.Query["bookId"][0]))
            {
                return Content("bookId can't be null or whitespace");
            }

            // bookId should be between 1 and 1000
            int bookId = Convert.ToInt32(ControllerContext.HttpContext.Request.Query["bookId"]);

            if (bookId <= 0) {
                return Content("Book id can't be less then or equal zero.");
            }

            if (bookId > 1000)
            {
                return Content("Book id can't be greater than 1000");
            }

        }
    }
}
