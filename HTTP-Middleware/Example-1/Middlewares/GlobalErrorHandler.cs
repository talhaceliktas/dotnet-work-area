using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Example_1.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class GlobalErrorHandler
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);

            }
            catch (Exception ex) {

                if (httpContext.Response.HasStarted)
                {
                    throw;
                }

                httpContext.Response.StatusCode = 500;
                    httpContext.Response.ContentType = "application/json";

                    object response = new
                    {
                        basari = false,
                        mesaj = "Sistemde beklenmeyen bir kriz oluştu, ekiplerimiz müdahale ediyor.",
                        hataDetayi = ex.Message,
                    };

                    await httpContext.Response.WriteAsJsonAsync(response);
            }

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class GlobalErrorHandlerExtensions
    {
        public static IApplicationBuilder UseGlobalErrorHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalErrorHandler>();
        }
    }
}
