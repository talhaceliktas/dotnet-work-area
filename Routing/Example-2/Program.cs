using Example_2.Constraints;
using Example_2.Data;
using Example_2.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<YetkiKontrolcusu>();
builder.Services.AddRouting((options) =>
{
    options.ConstraintMap.Add("kinsystem", typeof(KartalConstraint));
});

var app = builder.Build();

app.UseGlobalErrorHandler();

app.UseDefaultFiles();

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "PublicAssets")),
    RequestPath = "/publicassets"
});


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


app.UseRouting();

app.UseYetkiKontrolcusu();


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



app.MapGet("envanter", async (HttpContext context) =>
{
        await context.Response.WriteAsJsonAsync(AracData.Araclar);
});

app.MapGet("envanter/{arac:kinsystem}", async (HttpContext context, string arac) =>
{
        await context.Response.WriteAsJsonAsync(AracData.Araclar.Where(x=>x.name == arac).FirstOrDefault());
});

app.MapGet("envanter/{id:int:min(1)}", async (HttpContext context, int id, [FromQuery] bool? detayli) =>
{
    if (!detayli ?? true)
        await context.Response.WriteAsJsonAsync(new { AracData.Araclar.Where(x => x.id == id).FirstOrDefault()?.name });
    else
        await context.Response.WriteAsJsonAsync(AracData.Araclar.Where(x => x.id == id).FirstOrDefault());

});

app.MapGet("patlat", async () =>
{
    throw new Exception("Test Patlaması");
});

app.MapFallback(async(HttpContext context) => {
    context.Response.StatusCode = 404;

    await context.Response.WriteAsJsonAsync(new
    {
        status = "Failed",
        message = "Unhandled Route"
    });
});






app.Run();
