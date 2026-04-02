using ConfigurationExample;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.Configure<WeatherApiOptions>(builder.Configuration.GetSection("WeatherAPI"));
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

    
//app.MapGet("/config", async (HttpContext context) =>
//{
//    await context.Response.WriteAsync(app.Configuration["MyKey"] + "\n" ?? "");

//    await context.Response.WriteAsync(app.Configuration.GetValue<string>("MyKey", "Test") + "\n");

//    await context.Response.WriteAsync(app.Configuration.GetValue<int>("X", 10) + "\n");
//});


app.MapControllers();


app.Run();
