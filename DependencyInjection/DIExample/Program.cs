using Services;
using ServiceContracts;
using Autofac.Extensions.DependencyInjection;
using Autofac;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddControllersWithViews();
//builder.Services.Add(new ServiceDescriptor(
//    typeof(ICitiesService),
//    typeof(CitiesService),
//    ServiceLifetime.Scoped
//));
//builder.Services.AddTransient<ICitiesService, CitiesService>();
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuileder =>
{
    //containerBuileder.RegisterType<CitiesService>()
    //    .As<ICitiesService>().InstancePerDependency(); // Add transient

    containerBuileder.RegisterType<CitiesService>()
    .As<ICitiesService>().InstancePerLifetimeScope(); // Add scope

    //    containerBuileder.RegisterType<CitiesService>()
    //.As<ICitiesService>().SingleInstance(); // Add singleton
});
//builder.Services.AddSingleton<ICitiesService, CitiesService>();



var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
