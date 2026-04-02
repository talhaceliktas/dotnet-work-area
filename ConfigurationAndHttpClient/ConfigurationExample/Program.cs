var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();


app.MapGet("/", async (HttpContext context) =>
{
    await context.Response.WriteAsync(app.Configuration["MyKey"] ?? "");
});


app.MapControllers();


app.Run();
