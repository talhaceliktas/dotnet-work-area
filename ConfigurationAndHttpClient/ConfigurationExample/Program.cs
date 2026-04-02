var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();


app.MapGet("/", async (HttpContext context) =>
{
    await context.Response.WriteAsync(app.Configuration["MyKey"] + "\n" ?? "");

    await context.Response.WriteAsync(app.Configuration.GetValue<string>("MyKey", "Test") + "\n");

    await context.Response.WriteAsync(app.Configuration.GetValue<int>("X", 10) + "\n");
});


app.MapControllers();


app.Run();
