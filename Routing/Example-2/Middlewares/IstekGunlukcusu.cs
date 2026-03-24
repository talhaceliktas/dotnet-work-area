namespace Example_2.Middlewares
{
    public class IstekGunlukcusu
    {
        private readonly RequestDelegate _next;

        public IstekGunlukcusu(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext) {
            Console.WriteLine($"METHOD: {httpContext.Request.Method} | PATH: {httpContext.Request.Path}" +
                $" | TIMESTAMP:{httpContext.Request.Headers.Date}");

            await _next(httpContext);

            Console.WriteLine($"RESPONSE STATUS CODE: {httpContext.Response.StatusCode}");
        }
    }

    public static class IstekGunlukcusuExtensions
    {
        public static IApplicationBuilder UseIstekGunlukcusu(this IApplicationBuilder builder) {
            return builder.UseMiddleware<IstekGunlukcusu>();
        }
    }
}
