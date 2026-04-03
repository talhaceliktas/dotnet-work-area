namespace ProductHub.Middlewares
{
    public class RequestLoggingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine($"[LOG] {DateTime.Now:HH:mm:ss} | {context.Request.Method} {context.Request.Path} | Started");
            await next(context);
            Console.WriteLine($"[LOG] {DateTime.Now:HH:mm:ss} | {context.Request.Method} {context.Response.StatusCode} | Completed");
        }
    }

    public static class RequestLoggingExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
