namespace Example_2.Middlewares
{
    public class IstekGunlukcusu
    {
        private readonly RequestDelegate _next;

        public IstekGunlukcusu(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke() {
        
        }
    }

    public static class IstekGunlukcusuExtensions
    {
        public static IApplicationBuilder UseIstekGunlukcusu(this IApplicationBuilder builder) {
            return builder.UseMiddleware<IstekGunlukcusu>();
        }
    }
}
