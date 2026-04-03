using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Example_3.Controllers
{
    [ApiController]
    [Route("dosya")]
    public class DosyaController : ControllerBase
    {
        [HttpGet("virtual")]
        public IActionResult Index()
        {
            return new VirtualFileResult("/ornek.txt", "text/plain");

        }

        [HttpGet("bellek")]
        public IActionResult Bellek()
        {
            byte[] dosya = System.IO.File.ReadAllBytes
                (@"C:\Users\why\Desktop\dotnet-work-area\Controllers\Example-3\wwwroot\ornek.txt");

            return File(dosya, "text/plain");

        }

        [HttpGet("indir")]
        public IActionResult Indir()
        {
            var dosya = new VirtualFileResult("/ornek.txt", "text/plain");

            dosya.FileDownloadName = "indirilen.txt";

            return dosya;

        }
    }
}
