using System.Text.Json;

namespace ProductHub.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public GlobalExceptionMiddleware(RequestDelegate next, IWebHostEnvironment webHostEnvironment)
        {
            _next = next;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }       
            catch (Exception ex) {
                await HandleExceptionAsync(httpContext, ex, _webHostEnvironment.IsDevelopment());
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, bool isDevelopment)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            object errorResponse;
            if (isDevelopment)
            {
                errorResponse = new
                {
                    Status = 500,
                    Message = "Sunucuda beklenmeyen bir hata oluştu. Lütfen daha sonra tekrar deneyin.",
                    Detail = exception.Message 
                };
            }
            else
            {
                errorResponse = new
                {
                    Status = 500,
                    Message = "Sunucuda beklenmeyen bir hata oluştu. Lütfen daha sonra tekrar deneyin.",
                };
            }


            var result = JsonSerializer.Serialize(errorResponse);
            return context.Response.WriteAsync(result);
        }

    }

    public static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
