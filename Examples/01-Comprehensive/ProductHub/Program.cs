using ProductHub.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddTransient<RequestLoggingMiddleware>();

var app = builder.Build();

app.UseGlobalExceptionMiddleware();
app.UseRequestLogging();
app.UsePerformanceMiddleware();
app.UseRouting();
app.MapControllers();


app.MapGet("/", () => "Hello World!");

app.Run();
