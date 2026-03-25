using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers 
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        [Route("home")]
        [Route("/")]
        public ContentResult Index() {
            //return new ContentResult()
            //{
            //    ContentType = "text/plain",
            //    Content = "Hello from Index"
            //};

            //return Content("Hello from Index", "text/plain");

            return Content("<h1 style='color:red;'>Hello I'm talha celiktas</h1>", "text/html");
        }

        [Route("about")]
        public string About()
        {
            return "Hello from About";
        }

        [Route("contact")]
        public string Contact()
        {
            return "Hello from Contact";
        }

        [Route("product/{id:regex(\\d)}")]
        public string Product(int id)
        {
            return $"Hello from products {id}";
        }
    }
}
