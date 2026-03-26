using ControllersExample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

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

        [Route("person")]
        public JsonResult About()
        {
            Person person = new Person() {
                Id = Guid.NewGuid(),
                FirstName = "Talha",
                LastName = "Celiktas",
                Age = 22
            };

            //return Content("{isim: \"talha\"}", "application/json");

            //return new JsonResult(person);

            return Json(person);
        }

        [Route("contact")]
        public string Contact()
        {
            return "Hello from Contact";
        }

        [Route("file-download")]
        public VirtualFileResult FileDownload(int id)
        {
            return new VirtualFileResult("/test.pdf", "application/pdf");
        }
    }
}
