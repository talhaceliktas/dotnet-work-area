🌟 Task 1: “VIP Payment Guardian” (Security Middleware)
I expect you to write code that meets the following 5 requirements exactly:

1. Scope (UseWhen):
The guardian you write will not interfere with every request coming into the system. It will only intervene in requests containing /api/odeme in the URL. (Requests going to other pages will pass through unimpeded.)

2. Club Rules (HTTP Request & Method Check):
The user has arrived at the /api/odeme page but sent the request using the GET method. (Payment transactions are not performed via GET!) If the request is not a POST, immediately return a 405 Method Not Allowed status code and terminate the process.

3. VIP Ticket Check (HTTP Request Headers):
If the request is a POST, this time you’ll check the user’s headers. Is there a ticket named X-Firma-Token in the request headers?

If it’s missing or its value isn’t MultiPayVIP: Return a 401 Unauthorized status code and send the user away without letting them proceed (Short-circuit).

4. Stamping (HTTP Response Headers – Caution: Trap!):
The user has met all the conditions—well done. Before sending them inside (await next) or after sending them (without falling into that infamous “Headers are read-only” error you just experienced!), stamp the response headers with “X-Security-Check: Passed.”

5. Aesthetics (Extension Method):
I don’t want to call this middleware you wrote inside the Program.cs file using that ugly `app.UseMiddleware<...>` syntax. Write me an extension method so it works seamlessly like `app.UseOdemeFedaisi()`.

What I Expect
Write these three components from scratch:

The OdemeFedaisiMiddleware class (use either IMiddleware or a constructor).

The static class containing the extension method.

The magic lines in the Program.cs file showing how to chain these together (the UseWhen and UseOdemeFedaisi sections).




🌪️ Task 2: “Crisis Center” (Global Exception Handler)
I want you to write a “Crisis Center” middleware at the very top of the project that wraps everything. You will place the entire production pipeline inside a try-catch block.
Here are the challenging task requirements:
1. Scope (Container): This middleware will not use UseWhen. It will wrap the entire system without distinction. When the innermost workers (controllers or other middleware) throw an error, that error must propagate upward and land in this middleware’s catch block. (The order in Program.cs is critical for this!)
2. Catching and Handling (Catch & Response): When you catch an error, do the following:
Set the response status code to 500 Internal Server Error.
Set the content type to application/json.
Send the following JSON response to the user (Postman) (You can use `WriteAsJsonAsync` for this):
JSON
{
    “success”: false,
    “message”: “An unexpected system failure has occurred; our teams are addressing it.”,
    “errorDetail”: “Print the actual error message (Exception.Message) here.”
}
3. Test Bomb (Endpoint): Add the following route to your Program.cs file specifically to test this system—it intentionally throws an exception when a request is made:
C#
app.MapGet(“/bomba”, () => {
    throw new Exception(“Database connection lost!”);
});
Expected Delivery
The KrizMasasiMiddleware class.
The UseKrizMasasi() extension method.
The final version of the Program.cs file.
Big Hint: If a worker slips and falls on the assembly line (an error), who can save him? Of course, the person who let him in through the door at the very beginning of that line! The location of the crisis desk within Program.cs is the key to success.




