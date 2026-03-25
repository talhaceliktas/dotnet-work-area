namespace Example_2.Middlewares
{
    public class YetkiKontrolcusu : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
            if(!context.Request.Headers.ContainsKey("X-API-KEY")) {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Yetkisiz erişim: X-API-KEY header'ı eksik.");
                return;
            }

            await next(context);
        }
    }

    public static class YetkiKontrolcusuExtensions
    {
        public static IApplicationBuilder UseYetkiKontrolcusu(this IApplicationBuilder builder) {
            return builder.UseMiddleware<YetkiKontrolcusu>();
        }
    }
}
