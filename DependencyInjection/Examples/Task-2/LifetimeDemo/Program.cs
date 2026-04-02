using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ITransientOperation, TransientOperation>();
builder.Services.AddScoped<IScopedOperation, ScopedOperation>();
builder.Services.AddSingleton<ISingletonOperation, SingletonOperation>();

var app = builder.Build();

app.MapControllers();

app.Run();
