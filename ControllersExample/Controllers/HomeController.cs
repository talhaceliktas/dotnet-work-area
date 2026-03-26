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
        public VirtualFileResult FileDownload()
        {
            //return new VirtualFileResult("/test.pdf", "application/pdf");

            return File("/test.pdf", "application/pdf");
        }

        [Route("file-download2")]
        public PhysicalFileResult FileDownload2()
        {
            //return new PhysicalFileResult
            //    (@"C:\Users\why\Desktop\dotnet-work-area\ControllersExample\OtherPath\deneme.pdf",
            //    "application/pdf");

            return PhysicalFile(@"C:\Users\why\Desktop\dotnet-work-area\ControllersExample\OtherPath\deneme.pdf",
                "applicaton/pdf");
        }

        [Route("file-download3")]
        public FileContentResult FileDownload3()
        {
            byte[] image = System.IO.File.ReadAllBytes(@"C:\Users\why\Desktop\dotnet-work-area\ControllersExample\OtherPath\test.avif");

            //return new FileContentResult(image, "image/avif");

            return File(image, "image/avif");
        }
    }
}
