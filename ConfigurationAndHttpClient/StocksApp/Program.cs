using StocksApp.Models;
using StocksApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.Configure<FinnhubOptions>(builder.Configuration.GetSection("Finnhub"));
builder.Services.AddScoped<MyService>();
var app = builder.Build();


app.UseStaticFiles();
app.UseRouting();
app.MapControllers();


app.Run();
