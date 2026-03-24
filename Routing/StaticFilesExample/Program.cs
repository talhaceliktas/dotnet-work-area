var builder = WebApplication.CreateBuilder(new WebApplicationOptions() {
    WebRootPath = "myroot"
});

var app = builder.Build();

app.UseStaticFiles();

app.MapGet("/", () => "Hello World!");

app.Run();
