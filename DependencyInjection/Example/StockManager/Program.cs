using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<IProductStockService, ProductStockService>();
var app = builder.Build();


app.UseStaticFiles();

app.UseRouting();
app.MapControllers();




app.Run();
