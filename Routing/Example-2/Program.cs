using Example_2.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<YetkiKontrolcusu>();

var app = builder.Build();

app.UseIstekGunlukcusu();

app.UseYetkiKontrolcusu();



app.MapGet("saglik", async (HttpContext context) =>
{
    context.Response.StatusCode = 200;
    context.Response.ContentType = "text/plain";

    await context.Response.WriteAsync("Sistem ayakta");
});

app.MapGet("gizli", async (HttpContext context) => {
    context.Response.Redirect("saglik", true);

});

app.MapPost("yankilama", async (HttpContext context) => {
    string contentType = context.Request.ContentType ?? "Content Type belirtilmedi.";
    long? contentLength = context.Request.ContentLength ?? 0;

    await context.Response.WriteAsync($"Gelen content-type: {contentType} \nContent-length: {contentLength}");

});

app.Run();
