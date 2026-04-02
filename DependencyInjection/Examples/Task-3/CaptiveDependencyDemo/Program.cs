using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddSingleton<IOrderProcessingService, OrderProcessingService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
var app = builder.Build();

app.UseRouting();
app.MapControllers();


app.Run();
