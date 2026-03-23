using Microsoft.AspNetCore.Http.HttpResults;

namespace Example_1
{
    public class OdemeFedaisi : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
                if (context.Request.Method != "POST")
                {
                    context.Response.StatusCode = 405;
                    return;
                }

                if (!context.Request.Headers.ContainsKey("X-Firma-Token"))
                {
                    context.Response.StatusCode = 401;
                    return;
                }

                if (context.Request.Headers["X-Firma-Token"][0] != "MultiPayVIP")
                {
                    context.Response.StatusCode = 401;
                    return;
                }

                context.Response.Headers["X-Guvenlik-Kontrolu"] = "Gecti";
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync("Success");
                return;
        }

    }
    
    public static class OdemeFedaisiExtension
    {
        public static IApplicationBuilder UseOdemeFedaisi(this IApplicationBuilder app)
        {
            return app.UseMiddleware<OdemeFedaisi>();
        }
    }
}
