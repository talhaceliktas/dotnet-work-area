var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Routing is automatically enabled.
// No need for app.UseRouting() anymore.

// Eg: files/sample.txt
app.Map("files/{filename}.{extension}", async (HttpContext context, string extension) =>
{
    string? fileName = context.Request.RouteValues["filename"]?.ToString();

    await context.Response.WriteAsync($"In files - {fileName} - {extension}");
});


// Eg: employee/profile/{employeeName}
app.Map("employee/profile/{employeeName=Scott}", async (HttpContext context, string employeeName) => {
    await context.Response.WriteAsync($"Hello {employeeName}");
});


// Eg: products/details/{id}
app.Map("product/details/{id=1}", (HttpContext context, int id) =>
{
    context.Response.WriteAsync($"Product details - {id}");
});



app.MapFallback(async (HttpContext context) => {
    await context.Response.WriteAsync($"Request recieved from {context.Request.Path}");
});

app.Run();
