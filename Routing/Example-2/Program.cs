using Example_2.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<YetkiKontrolcusu>();

var app = builder.Build();

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

app.MapGet("urunler/{id:int:min(1)}", async (HttpContext context, int id) =>
{


});


app.MapGet("calisanlar/{ad:alpha:length(3,10)}", async (HttpContext context, string ad) =>
{

});

app.MapGet("raporlar/{yil:int:min(2000)}/{donem:regex(^Q1|Q2|Q3$)}", async (HttpContext context, int yil) =>
{

});

app.MapGet("dosyalar/{**yol}", async (HttpContext context, string yol) =>
{

});






app.Run();
