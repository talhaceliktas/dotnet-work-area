var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Routing is automatically enabled.
// No need for app.UseRouting() anymore.

app.Map("map1", async (HttpContext context) => {
    await context.Response.WriteAsync("Hello from Map 1");
});

app.Map("map2", async (HttpContext context) => {
    await context.Response.WriteAsync("Hello from Map 2");
});


app.Run();
