using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers 
{
    public class HomeController
    {
        [Route("home")]
        [Route("/")]
        public string Index() {
            return "Hello from Index";
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

        [Route("product/{id}")]
        public string Product(int id)
        {
            return $"Hello from products {id}";
        }
    }
}
