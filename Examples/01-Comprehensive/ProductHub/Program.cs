using ProductHub.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddTransient<RequestLoggingMiddleware>();

var app = builder.Build();

app.UseGlobalExceptionMiddleware();
app.UseRequestLogging();
app.UsePerformanceMiddleware();
app.UseRouting();

app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/admin"), appBuilder =>
{
    appBuilder.Use(async (context, next) =>
    {
        context.Response.Headers.Append("X-Admin-Access", "true");

        await next();
    });
});

app.MapControllers();


app.MapGet("/", () => "Hello World!");

app.Run();
