using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProductHub.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class PerformanceMiddleware
    {
        private readonly RequestDelegate _next;

        public PerformanceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            await _next(httpContext);
            stopwatch.Stop();

            if (stopwatch.ElapsedMilliseconds > 200)
                Console.WriteLine($"[SLOW REQUEST] Yol: {httpContext.Request.Path} | Süre: {stopwatch.ElapsedMilliseconds} ms");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class PerformanceMiddlewareExtensions
    {
        public static IApplicationBuilder UsePerformanceMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PerformanceMiddleware>();
        }
    }
}
