var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/odeme"), (IApplicationBuilder app) =>
{
    app.Run(async (HttpContext context) => {
        if(context.Request.Method != "POST")
        {
            context.Response.StatusCode = 405;
        }
    });
});

app.Run();
