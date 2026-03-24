# 🧪 ASP.NET Core — Practical Task List
> **Level:** Intermediate–Advanced | **Topics:** Getting Started · HTTP · Middleware · Routing  
> All tasks will be completed using the **Minimal API** structure. You can review your code after completing each task.

---

## ✅ TASK 1 — Set Up the Project from Scratch and Test HTTP Basics

**Objective:** Create an empty ASP.NET Core Minimal API project and manually manage basic HTTP mechanics.

### Subtasks:
- [ ] Create a new project named `StajyerAPI` using `dotnet new web`
- [ ] Edit the `http` profile in `launchSettings.json` → set the port to `5050`
- [ ] Define the following 3 endpoints only in `Program.cs` (no controller):
  - `GET /saglik` → `200 OK`, body: `“System is up”`, Content-Type: `text/plain`
  - `GET /gizli` → **301 Redirect** → redirect to `/saglik` (use manual `Response.Redirect`)
  - `POST /yankilama` → write the request’s `Content-Type` header and body length to the response
- [ ] Test all 3 endpoints using Postman or `curl`, and verify that the status codes are correct

> **Tip:** `context.Response.StatusCode`, `context.Response.Headers`, `context.Request.ContentLength` — you can access all of these via `HttpContext`.

---

## ✅ TASK 2 — Design the Middleware Pipeline

**Objective:** Demonstrate the practical differences between `app.Use`, `app.Run`, and `app.UseWhen` by setting up a multi-layered, sequential middleware pipeline.

### Subtasks:
- [ ] Implement the `RequestLogger` class as **conventional middleware**:
  - Pass `RequestDelegate _next` to the constructor
  - In the `Invoke(HttpContext)` method, log the request’s HTTP method, path, and timestamp to the console
  - After calling `await _next(HttpContext)`, log the response status code to the console
  - Add an extension method: `UseRequestLogger()`
- [ ] Implement the **IMiddleware-based** `AuthorizationController` middleware:
  - Implement `InvokeAsync(HttpContext context, RequestDelegate next)`
  - If the `X-API-KEY` is missing from the request header, return `401 Unauthorized` and terminate the pipeline
  - If present, continue with `next(context)`
  - Save with `AddTransient<AuthorizationController>()`
- [ ] The pipeline order in `Program.cs` must **strictly** be as follows:
  ```
  RequestLogger → UseWhen → AuthorizationController → Endpoint
  ```
- [ ] Use `app.UseWhen` to add the following condition to a branch: If the query string contains `?debug=true`, add an additional `Use` block to append the `“[DEBUG MODE]”` prefix to the response
- [ ] If the pipeline breaks (401), verify that the `RequestLogger` still logs the response status code

> **Note:** If you're using `IMiddleware`, DI registration is required. Conventional middleware does not require DI registration — see the difference!

---

## ✅ TASK 3 — Advanced Routing and Constraints

**Objective:** Write robust routes using pure parameter binding **without** using `context.Request.RouteValues`.

### Subtasks:
- [ ] Add the following endpoints to `Program.cs`. **Access to RouteValues is prohibited**; parameters will be passed directly to the function signature:

  | Route | Constraint | Parameter Type |
  |------|-------|----------------|
  | `GET /products/{id:int:min(1)}` | id ≥ 1 | `int id` |
  | `GET /employees/{name:alpha:length(3,10)}` | letters only, 3–10 characters | `string name` |
  | `GET /reports/{year:int:min(2000)}/{period:regex(^(Q1|Q2|Q3|Q4)$)}` | year ≥ 2000, period Q1–Q4 | `int year, string period` |
  | `GET /files/{*path}` | catch-all | `string path` |

- [ ] Add Query String support to the `GET /products/{id}` endpoint:
  - Make `/products/5?category=electronics` work with the `[FromQuery] string? category` parameter
- [ ] **Extend** the `MonthsCustomConstraint` class in the training repository:
  - Copy the existing class and create a new class named `KartalConstraint`
  - Accept the values `“kartal”`, `‘siha’`, `“akinci”` (mock vehicle name validation)
  - Save it using `builder.Services.AddRouting(options => options.ConstraintMap.Add(“kinsystem”, typeof(KartalConstraint)))`
  - Add the `GET /inventory/{vehicle:kinsystem}` endpoint

> **Reminder:** If you don’t add the custom constraint to the `ConstraintMap`, the route returns a **404**—it won’t throw a runtime error. Don’t fall into this trap!

---

## ✅ TASK 4 — Global Error Handling and Static Files

**Objective:** Place a real `GlobalErrorHandler` at the top of the pipeline and configure the static file service.

### Subtasks:
- [ ] Write a **conventional middleware** named `GlobalErrorHandler`:
  - Wrap the entire pipeline in a try-catch block
  - Check `context.Response.HasStarted` — if the response has started, log it and exit
  - In case of an error: `500`, `Content-Type: application/json`, body: `{“error”: “Server crashed”, ‘time’: “...”}` 
  - Extension method: `UseGlobalErrorHandler()`
- [ ] Add a `GET /patlat` endpoint → have it throw `new Exception(“Test crash!”)`
- [ ] Pipeline order:
  ```
  UseGlobalErrorHandler → UseStaticFiles → (other middleware) → Endpoints
  ```
- [ ] Add a `wwwroot/` folder to the project and place a simple `index.html` file inside it
- [ ] Add `app.UseStaticFiles()` so that `/index.html` opens in the browser
- [ ] **Bonus:** Create a custom folder named `PublicAssets` instead of `wwwroot`, configure `StaticFileOptions` + `PhysicalFile

---

## ✅ TASK 5 — Put It All Together: Real-World Scenario

**Objective:** Write a realistic API skeleton that combines everything from the previous 4 tasks into a single `Program.cs` file.

### Scenario:
You are writing an **Unmanned Aerial Vehicle (UAV) Inventory API**. Unauthorized access must be prevented, requests must be logged, and errors must be handled.

### Subtasks:
- [ ] Pipeline (strict order):
  ```
  GlobalErrorHandler
    → UseStaticFiles
    → RequestLogger (log every request)
    → UseWhen(?debug=true → add debug header)
    → AuthorizationChecker (X-API-KEY check)
    → Endpoints
  ```
- [ ] Endpoints:
  - `GET /` → Return `wwwroot/index.html` or `“UAV Inventory API v1”`
  - `GET /inventory` → Return a fixed list: `[“TB2”, ‘Akıncı’, “SIHA”]` (as JSON)
  - `GET /inventory/{vehicle:kinetic-system}` → Return details for the specified vehicle (mock data is sufficient)
  - `GET /inventory/{id:int:min(1)}` → Search by ID, accept the `[FromQuery] bool? detailed` parameter
  - `GET /detonate` → Throw an exception; GlobalErrorHandler must catch it
- [ ] The entire page’s pipeline must be functional:
  - Request without `X-API-KEY` → `401`
  - Invalid route → `404` (customize with MapFallback)
  - Exception → `500 JSON`
  - Static file → direct service

> **Final Test:** Create a Postman collection and run each scenario. Monitor the console logs to see which middleware is triggered and when.

---

## 📊 Scorecard (For Self-Assessment)

| Task | Topic | Difficulty |
|------|------|--------|
| Task 1 | Getting Started + HTTP | ⭐⭐ |
| Task 2 | Middleware Pipeline | ⭐⭐⭐ |
| Task 3 | Routing + Constraints | ⭐⭐⭐⭐ |
| Task 4 | Error Handling + Static Files | ⭐⭐⭐ |
| Task 5 | Integration of All Topics | ⭐⭐⭐⭐⭐ |