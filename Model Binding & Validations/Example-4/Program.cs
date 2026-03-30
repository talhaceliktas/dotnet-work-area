using Example_4.Binders;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers((options) =>
{
    options.ModelBinderProviders.Insert(0, new SearchFilterBinderProvider());
})
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var hataListesi = context.ModelState.Values
            .SelectMany(x => x.Errors)
            .Select(x => x.ErrorMessage)
            .ToList();

            var hataKutusu = new
            {
                Success = false,
                Errors = hataListesi
            };

            return new BadRequestObjectResult(hataKutusu);
        };
    })
    
    ;
var app = builder.Build();

app.MapControllers();


app.Run();
