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
    // minlength(x) - maxlength(x) - length(x, y) - length(x) - min(x) - max(x) - range(x, y)

app.Map("employee/profile/{employeeName:alpha=Scott}", async (HttpContext context, string employeeName) => {
    await context.Response.WriteAsync($"Hello {employeeName}");
});


// Eg: products/details/{id}
app.Map("product/details/{id:int:min(1):max(100)?}", async (HttpContext context, int? id) =>
{
    if(id.HasValue)
        await context.Response.WriteAsync($"Product details of ID:{id}");
    else
        await context.Response.WriteAsync($"Please provide product id!");
});


// Eg: daily-digest-report/{reportDate}
app.Map("daily-digest-report/{reportDate:datetime}", async (HttpContext context, DateTime reportDate) =>
{
    await context.Response.WriteAsync($"Report Date: {reportDate.ToShortDateString()}");
});


app.Map("cities/{cityId:guid}", async (HttpContext context, Guid cityId) =>
{
    await context.Response.WriteAsync($"City Information - {cityId}");
});


app.Map("sales-report/{year:int:min(1900)}/{month:regex(^apr|jul|oct|jan$)}",
    async (HttpContext context, int year, string month) => {


        await context.Response.WriteAsync($"Sales Report - {year} - {month}");
});


app.MapFallback(async (HttpContext context) => {
    await context.Response.WriteAsync($"No route matched at {context.Request.Path}");
});

app.Run();
