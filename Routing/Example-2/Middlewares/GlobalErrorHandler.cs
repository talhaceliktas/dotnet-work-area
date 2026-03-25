namespace Example_2.Middlewares
{
    public class GlobalErrorHandler
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex) {
                if (context.Response.HasStarted)
                {
                    Console.WriteLine("Hata olustu: " + ex.ToString());
                    return;
                }

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new
                {
                    hata = "Sunucu patladı",
                    zaman = DateTimeOffset.Now.ToUnixTimeSeconds()

                });


            
            
            }

        }
    }

    public static class GlobalErrorHandlerExtensions
    {
        public static IApplicationBuilder UseGlobalErrorHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalErrorHandler>();
        }
    }
}
