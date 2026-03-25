namespace Middlewares.CustomMiddlewares
{
    public class MyCustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            DateTime start = DateTime.Now;

            await next(context);

            DateTime end = DateTime.Now;

            Console.WriteLine($"Islem Suresi: {(end - start).TotalMilliseconds}");
        }
    }

    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder app) {
            return app.UseMiddleware<MyCustomMiddleware>();
        }
    }

}
