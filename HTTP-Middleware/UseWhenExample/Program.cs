var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/gizli"), app => {
    app.Use(async (HttpContext context, RequestDelegate next) =>
    {
        await context.Response.WriteAsync("Gizli Odaya hoşgeldin!");
        await next(context);
    });
});

app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Merhaba dünya!");
});

app.Run();
