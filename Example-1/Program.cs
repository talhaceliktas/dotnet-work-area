using Example_1.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<OdemeFedaisi>();

var app = builder.Build();

app.UseGlobalErrorHandler();


app.MapGet("/", () => "Hello World!");


app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/odeme"), (IApplicationBuilder app) =>
{
    app.UseOdemeFedaisi();
});

app.Run();
