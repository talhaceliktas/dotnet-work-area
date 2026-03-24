var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Routing is automatically enabled.
// No need for app.UseRouting() anymore.

app.MapGet("map1", async (HttpContext context) => {
    await context.Response.WriteAsync("Hello from Map 1");
});

app.MapPost("map2", async (HttpContext context) => {
    await context.Response.WriteAsync("Hello from Map 2");
});

app.MapFallback(async (HttpContext context) => {
    await context.Response.WriteAsync($"Request recieved from {context.Request.Path}");
});

app.Run();
