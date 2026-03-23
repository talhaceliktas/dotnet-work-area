var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", (HttpContext context) =>
{
    context.Response.Redirect("http://localhost:5248/calculate?firstNumber=123&secondNumber=456&operation=multiply");
});

app.MapGet("/calculate", (HttpContext context) =>
{
    context.Response.ContentType = "application/json";

    IQueryCollection queries = context.Request.Query;

    var isError = !queries.ContainsKey("firstNumber") || !queries.ContainsKey("secondNumber")
    || !queries.ContainsKey("operation");

    object jsonObject;

    if (!isError)
    {
        double firstNumber = Convert.ToDouble(queries["firstNumber"][0]);
        double secondNumber = Convert.ToDouble(queries["secondNumber"][0]);
        string operation = queries["operation"][0] ?? "plus";
        double result;

        if (operation == "plus")
            result = firstNumber + secondNumber;
        else if (operation == "minus")
            result = firstNumber - secondNumber;
        else if (operation == "divide")
            result = firstNumber / secondNumber;
        else if (operation == "multiply")
            result = firstNumber * secondNumber;
        else
            result = 0;

        jsonObject = new
        {
            status = "Succes",
            firstNumber,
            secondNumber,
            operation,
            result,
        };
    }
    else
    {
        jsonObject = new
        {
            status = "Error"
        };
    }

    context.Response.WriteAsJsonAsync(jsonObject);
});

app.Run();
