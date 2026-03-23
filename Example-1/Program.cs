var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/odeme"), (IApplicationBuilder app) =>
{
    app.Use(async (HttpContext context, RequestDelegate next) => {
        if(context.Request.Method != "POST")
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

        await next(context);

    });
});

app.Run();
