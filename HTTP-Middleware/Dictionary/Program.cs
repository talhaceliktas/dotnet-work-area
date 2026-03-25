using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) => {
    StreamReader sr = new StreamReader(context.Request.Body);
    string body = await sr.ReadToEndAsync();

    Dictionary<string, StringValues> dictionary = QueryHelpers.ParseQuery(body);

    if (dictionary.ContainsKey("firstName"))
    {
        string firstName = dictionary["firstName"][0] ?? "";
        await context.Response.WriteAsync($"Isim: {firstName}");

    }

    int price = 0;
    if (dictionary.ContainsKey("price"))
    {

        foreach (string? sayi in dictionary["price"])
        {
            if (string.IsNullOrEmpty(sayi)) continue;
            price += Convert.ToInt32(sayi);
        }
    }

    await context.Response.WriteAsync($"Fiyatlar: {price}");
});


app.Run();
