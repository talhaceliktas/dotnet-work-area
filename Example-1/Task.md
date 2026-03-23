🌟 Task: “VIP Payment Guardian” (Security Middleware)
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

Translated with DeepL.com (free version)