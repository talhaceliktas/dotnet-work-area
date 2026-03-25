using Example_2.Constraints;
using Example_2.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder();

builder.Services.AddTransient<YetkiKontrolcusu>();
builder.Services.AddRouting((options) =>
{
    options.ConstraintMap.Add("kinsystem", typeof(KartalConstraint));
});

var app = builder.Build();

app.UseGlobalErrorHandler();

app.UseIstekGunlukcusu();

app.UseWhen((HttpContext) =>
    {
        if(HttpContext.Request.Query.TryGetValue("debug", out var debug))
        {
            return debug[0]?.ToString() == "true";
        }
        return false;

    },
    (IApplicationBuilder app) =>
    {
        app.Use(async (HttpContext context, RequestDelegate next) =>
        {
            context.Response.Headers["X-Debug-Mode"] = "aktif";
            await next(context);
        });
    });

app.UseYetkiKontrolcusu();

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "PublicAssets")),
    RequestPath = "/publicassets"
});


app.MapGet("saglik", async (HttpContext context) =>
{
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

app.MapGet("urunler/{id:int:min(1)}", async (HttpContext context, int id, [FromQuery] string? kategori) =>
{
    await context.Response.WriteAsync($"ID: {id} - Kategori: {kategori}");
});


app.MapGet("calisanlar/{ad:alpha:length(3,10)}", async (HttpContext context, string ad) =>
{
    await context.Response.WriteAsync($"Çalışan: {ad}");
});

app.MapGet("raporlar/{yil:int:min(2000)}/{donem:regex(^(Q1|Q2|Q3|Q4)$)}", async (HttpContext context, int yil, string donem) =>
{
    await context.Response.WriteAsync($"Yıl: {yil} - Donem: {donem}");
});

app.MapGet("dosyalar/{*yol}", async (HttpContext context, string yol) =>
{
    await context.Response.WriteAsync($"Yol: {yol}");
});


app.MapGet("envanter/{arac:kinsystem}", async (HttpContext context, string arac) =>
{
    await context.Response.WriteAsync($"Araç: {arac}");
});

app.MapGet("patlat", async () =>
{
    throw new Exception("Test Patlaması");
});






app.Run();
