
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Kronometre
//app.UseMiddleware<MyCustomMiddleware>();

// Middleware 2
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    try
    {
        await next(context);
    }
    catch (Exception ex)
    {
        await context.Response.WriteAsync("Status:Error");

        Console.WriteLine(ex.ToString());

        return;
    }

});


app.MapGet("/hata", () =>
{
    throw new NotImplementedException();
});

app.MapGet("/", async (HttpContext context) =>
{
    await context.Response.WriteAsync("Status: Success");

});

app.Run();
