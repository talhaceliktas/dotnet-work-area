using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers 
{
    public class HomeController
    {
        [Route("home")]
        [Route("/")]
        public ContentResult Index() {
            return new ContentResult()
            {
                ContentType = "text/plain",
                Content = "Hello from Index"
            };
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
