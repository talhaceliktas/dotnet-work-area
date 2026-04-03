# 🏗️ Master Task Series — ASP.NET Core Senior Developer Journey (English)

> **Scope:** 03.Middleware · 04.Routing · 05.Controllers & IActionResult · 06.Model Binding & Validations · 11.DI · 12.Environments · 13.Configuration  
> **Project:** `ProductHub` — a multi-environment, secure, layered product management API  
> **Format:** Each task **builds on the previous** — one project, four layers.

---

## ✅ Task 1 — Pipeline Architecture: Middleware + Routing + Controllers + IActionResult

### 🎯 Real-World Scenario
You joined the backend team of an e-commerce company. Your first mission: build `ProductHub`'s **request-response infrastructure** from scratch. Every HTTP request must pass through logging, performance measurement, and security checks — then reach the correct controller.

### 📋 Requirements

**Layer 1 — Custom Middleware**

1. Create `RequestLoggingMiddleware` (implement `IMiddleware`):
   - Log to console on entry:
     ```
     [LOG] {DateTime.Now:HH:mm:ss} | {HttpMethod} {Path} | Started
     ```
   - Log after `next(context)`:
     ```
     [LOG] {DateTime.Now:HH:mm:ss} | {HttpMethod} {Path} | {StatusCode} | Completed
     ```
   - Register via extension method: `UseRequestLogging()`

2. Create `PerformanceMiddleware` (conventional, `RequestDelegate` via constructor):
   - Measure elapsed ms using `Stopwatch`
   - Log `[SLOW REQUEST]` to console for requests exceeding 200ms

3. Set up middleware order in `Program.cs`:
   ```
   ExceptionHandler → RequestLogging → Performance → Routing → Controllers
   ```

**Layer 2 — Routing**

4. Create `ProductsController`. Define routes with attribute routing:

   | HTTP | Route | Description |
   |------|-------|-------------|
   | GET | `/api/products` | List all products |
   | GET | `/api/products/{id:int:min(1)}` | Get product by ID |
   | GET | `/api/products/search?name={keyword}` | Search by name |
   | POST | `/api/products` | Add new product |
   | GET | `/api/products/report/{year:int:min(2020)}/{month:regex(^(jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec)$)}` | Monthly report |

5. Use route constraints: `:int:min(1)` for `id`, `:int:min(2020)` + regex for report.

**Layer 3 — IActionResult**

6. Return the correct `IActionResult` from each action:
   - `GetAll` → `Ok(products)` (200)
   - `GetById` → `NotFound("Product not found")` (404) or `Ok(product)` (200)
   - `Search` → `BadRequest("Search keyword required")` (400) if empty
   - `Create` → `CreatedAtAction(nameof(GetById), new { id = newId }, product)` (201)
   - `GetReport` → `Ok(new { Year = year, Month = month, ReportedAt = DateTime.Now })`

7. Use `UseWhen` to add a middleware branch for `/api/admin` paths — the branch adds an `X-Admin-Access: true` response header.

### 💡 Hints
- `IMiddleware` → resolved from DI container; requires `builder.Services.AddTransient<RequestLoggingMiddleware>()`.
- Conventional middleware → no DI registration needed; `RequestDelegate` is injected via constructor.
- `CreatedAtAction` correct usage: `nameof(GetById)` + route value dictionary.

### ✔️ Evaluation Criteria

| Criterion | Expected |
|-----------|----------|
| `IMiddleware` for `RequestLoggingMiddleware` | Start + Completed log |
| Conventional `PerformanceMiddleware` | 200ms threshold, Stopwatch |
| Correct middleware order | Exception → Log → Perf → Routing → MVC |
| 5 routes + constraints | `id:int:min(1)`, regex month |
| IActionResult variety | 200/201/400/404 used correctly |
| `UseWhen` admin branch | Header added |

---

## ✅ Task 2 — Data Security: Model Binding + Validation Armor

### 🎯 Real-World Scenario
The product add/update endpoints are currently insecure — any data submitted creates a record. Your mission: equip the `ProductRequest` model with **production-grade validation** and protect against overposting attacks using `[BindNever]`.

### 📋 Requirements

**Layer 1 — Model Class & Basic Validations**

1. Create `ProductRequest` model class:
   ```csharp
   public class ProductRequest : IValidatableObject
   {
       [BindNever] public Guid ProductId { get; set; }    // overposting protection
       [BindNever] public DateTime CreatedAt { get; set; } // overposting protection

       [Required(ErrorMessage = "Product name is required")]
       [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be 3-100 characters")]
       public string? Name { get; set; }

       [Required]
       [Range(0.01, 99999.99, ErrorMessage = "Price must be between 0.01 and 99999.99")]
       public double Price { get; set; }

       [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
       public int Stock { get; set; }

       [Required]
       public DateTime ManufactureDate { get; set; }

       public DateTime? ExpiryDate { get; set; }

       // IValidatableObject implementation below
   }
   ```

2. Implement `IValidatableObject.Validate` — cross-field validation:
   - If `ExpiryDate` is provided, it cannot be before `ManufactureDate`
   - If `Price < 1`, then `Stock > 1000` is not allowed (low-price items can't have huge stock)

**Layer 2 — Custom Validation Attribute**

3. Create `FutureDateAttribute : ValidationAttribute`:
   - `ManufactureDate` cannot be in the future (products can't be manufactured tomorrow)
   - Error message: `"Manufacture date cannot be in the future"`

4. Apply this attribute to `ProductRequest.ManufactureDate`.

**Layer 3 — Controller Update**

5. Update `ProductsController.Create`:
   - Accept `ProductRequest` via `[FromBody]`
   - Check `ModelState.IsValid`
   - If invalid: return `BadRequest(ModelState)`
   - If valid: set `ProductId = Guid.NewGuid()`, `CreatedAt = DateTime.UtcNow`
   - Return 201 via `CreatedAtAction`

6. Add `[FromQuery] string? category` to `GetAll`:
   - Filter if category is provided, return all if not
   - Return `NotFound($"No products in category: {category}")` if category doesn't match

7. Test scenarios (Swagger/Postman):
   ```json
   // ❌ Invalid: empty name, negative price
   { "Name": "", "Price": -5, "Stock": 10, "ManufactureDate": "2020-01-01" }

   // ❌ Overposting attempt: ProductId, CreatedAt should be ignored
   { "ProductId": "some-guid", "CreatedAt": "2000-01-01", "Name": "Test", "Price": 10, "Stock": 5, "ManufactureDate": "2023-01-01" }

   // ✅ Valid
   { "Name": "Gaming Mouse", "Price": 299.99, "Stock": 50, "ManufactureDate": "2024-01-01" }
   ```

### 💡 Hints
- `[BindNever]` is applied to model properties — values from the request are completely ignored.
- `IValidatableObject.Validate` is only triggered after data annotation validations pass.
- `BadRequest(ModelState)` → returns all validation errors as JSON.

### ✔️ Evaluation Criteria

| Criterion | Expected |
|-----------|----------|
| `[Required]`, `[StringLength]`, `[Range]` | Applied correctly to model |
| `[BindNever]` | `ProductId` and `CreatedAt` not bound |
| `FutureDateAttribute` | Future dates rejected |
| `IValidatableObject` | Cross-field validation works |
| `[FromBody]` usage | JSON body is read |
| `ModelState.IsValid` check | Invalid → `BadRequest(ModelState)` |
| Overposting test | `ProductId` is always `Guid.NewGuid()` |

---

## ✅ Task 3 — Service Layer: DI + Environments Integration

### 🎯 Real-World Scenario
Controllers currently hold in-memory data and business logic. A senior developer doesn't accept this. Your mission: **move business logic to a service layer**, loosely couple everything with DI, and use environment information intelligently.

### 📋 Requirements

**Layer 1 — Interface + Service Layer**

1. Create `IProductService` interface:
   ```csharp
   public interface IProductService
   {
       List<ProductResponse> GetAll(string? category = null);
       ProductResponse? GetById(Guid id);
       ProductResponse Create(ProductRequest request);
       bool IsLowStock(Guid id, int threshold);
   }
   ```

2. Implement `ProductService : IProductService` — holds an in-memory `List<ProductResponse>`.  
   Seed with 3 default products in the constructor.

3. Create `IProductAuditService` interface and `ProductAuditService`:
   ```csharp
   public interface IProductAuditService
   {
       void LogAction(string action, Guid productId);
       List<string> GetAuditLog();
       Guid ServiceInstanceId { get; }  // GUID pattern from Task 2 — lifetime proof
   }
   ```

4. Register lifetimes in `Program.cs`:
   ```csharp
   builder.Services.AddScoped<IProductService, ProductService>();
   builder.Services.AddSingleton<IProductAuditService, ProductAuditService>();
   ```

**Layer 2 — Controller Update (DI)**

5. Constructor-inject `IProductService` and `IProductAuditService` into `ProductsController`. Controller no longer holds any in-memory data.

6. In the `Create` action, call `_auditService.LogAction("CREATE", product.ProductId)`.

7. Add a new action for viewing the audit log:
   ```csharp
   [HttpGet("/api/audit")]
   public IActionResult GetAuditLog()
   {
       return Ok(new {
           ServiceId = _auditService.ServiceInstanceId,
           Logs = _auditService.GetAuditLog()
       });
   }
   ```

**Layer 3 — Captive Dependency Protection**

8. Add `ProductNotificationService` — it must be Singleton (simulates a notification queue) but needs **Scoped** `IProductService`. Inject it directly and observe what happens:
   - You'll get `InvalidOperationException` — captive dependency!
   - Fix: use `IServiceScopeFactory`.

**Layer 4 — Environments**

9. Create `HomeController`:
   - Inject `IWebHostEnvironment`
   - Show a landing page with environment info at `/`

10. Environment-based middleware in `Program.cs`:
    ```csharp
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        app.UseExceptionHandler("/error");
    }
    ```

11. Use `<environment>` tag helper in `Views/Home/Index.cshtml`:
    ```html
    <environment include="Development">
        <div class="alert alert-warning">⚠️ DEV MODE: Do not use real data!</div>
        <a href="/swagger">📋 Swagger UI</a>
    </environment>
    <environment exclude="Development">
        <div class="alert alert-success">✅ Production environment active</div>
    </environment>
    ```

### 💡 Hints
- A Scoped service injected into a Singleton → runtime exception in Development.
- Fix: inside the Singleton, use `IServiceScopeFactory.CreateScope()` for each operation.
- `ServiceInstanceId` → always the same GUID for Singleton; new GUID per request for Scoped.

### ✔️ Evaluation Criteria

| Criterion | Expected |
|-----------|----------|
| `IProductService` separation | Controller business logic lives in the service |
| `IProductAuditService` Singleton | Same GUID on page refresh |
| `IProductService` Scoped | New instance per request |
| Captive dependency detected + fixed | `IServiceScopeFactory` used |
| `IWebHostEnvironment` inject | Dev/Prod middleware differences |
| `<environment>` tag helper | Dev banner visible |

---

## ✅ Task 4 — Configuration Fortress: Options Pattern + Secrets + Precedence Chain

### 🎯 Real-World Scenario
`ProductHub` is getting ready for production. However, API keys are in source code and environment-specific settings are exposed in `appsettings.json`. Your mission: make the configuration infrastructure **production-grade**.

### 📋 Requirements

**Layer 1 — Hierarchical Configuration**

1. Add to `appsettings.json`:
   ```json
   {
     "ProductHub": {
       "AppName": "ProductHub",
       "Version": "1.0.0",
       "MaxProductsPerPage": 50,
       "AllowedCategories": ["Electronics", "Clothing", "Food", "Books"]
     },
     "Notification": {
       "Email": "admin@producthub.com",
       "SlackWebhook": "https://hooks.slack.com/placeholder",
       "Enabled": true
     }
   }
   ```

2. Add to `appsettings.Development.json`:
   ```json
   {
     "ProductHub": { "MaxProductsPerPage": 10, "AppName": "ProductHub [DEV]" }
   }
   ```

3. Add to `appsettings.Production.json`:
   ```json
   {
     "ProductHub": { "MaxProductsPerPage": 100, "AppName": "ProductHub [PROD]" }
   }
   ```

**Layer 2 — Options Pattern**

4. Create `ProductHubOptions` and `NotificationOptions` classes. All properties must be strongly typed.

5. Register in `Program.cs`:
   ```csharp
   builder.Services.Configure<ProductHubOptions>(
       builder.Configuration.GetSection("ProductHub"));
   builder.Services.Configure<NotificationOptions>(
       builder.Configuration.GetSection("Notification"));
   ```

6. Inject `IOptions<ProductHubOptions>` into `ProductService`:
   - `GetAll` → never return more products than `MaxProductsPerPage`
   - On service creation: `Console.WriteLine($"[SERVICE] Max page size: {options.MaxProductsPerPage}")`

7. Inject `IOptions<ProductHubOptions>` into `HomeController` — display `AppName` and `Version` in the View.

**Layer 3 — User Secrets & Secure Configuration**

8. Initialize User Secrets:
   ```
   dotnet user-secrets init
   dotnet user-secrets set "ExternalApi:Key" "dev-api-key-xyz-789"
   dotnet user-secrets set "ExternalApi:BaseUrl" "https://api.dev.producthub.com"
   ```

9. Create `ExternalApiOptions`. Register in `Program.cs`.

10. Inject `IOptions<ExternalApiOptions>` into `ProductsController`. Add `/api/config/status` endpoint:
    ```csharp
    [HttpGet("/api/config/status")]
    public IActionResult ConfigStatus()
    {
        return Ok(new {
            AppName = _hubOptions.AppName,
            Environment = _env.EnvironmentName,
            ApiKeyPreview = _apiOptions.Key.Length > 4
                ? $"{_apiOptions.Key[..4]}****"
                : "NOT SET",
            MaxPageSize = _hubOptions.MaxProductsPerPage
        });
    }
    ```

**Layer 4 — Precedence Chain Proof**

11. In PowerShell:
    ```powershell
    $env:ProductHub__AppName = "ProductHub [ENV-VAR]"
    dotnet run --no-launch-profile
    ```
    Call `/api/config/status` — observe `AppName` is `[ENV-VAR]`.

12. Add a comment block to `Program.cs`:
    ```csharp
    // CONFIGURATION PRECEDENCE ORDER (lowest to highest):
    // 1. appsettings.json                  → Base values
    // 2. appsettings.{Environment}.json    → Environment-specific override
    // 3. User Secrets (Dev only)           → Sensitive development data
    // 4. Environment Variables             → Highest priority (used in prod)
    // 5. Command Line Args                 → Runtime override
    ```

13. Write a `/api/config/full` endpoint that lists values from all sources separately.

### 💡 Hints
- `IOptions<T>.Value` → snapshot bound at startup; does NOT update if config changes at runtime.
- In Dev, `MaxProductsPerPage: 10` → requesting >10 products truncates the result.
- `dotnet run --no-launch-profile` → skips `launchSettings.json`; env vars come from shell.

### ✔️ Evaluation Criteria

| Criterion | Expected |
|-----------|----------|
| `ProductHubOptions` + `NotificationOptions` | Strongly-typed, `Configure<T>` registered |
| Dev override | Dev: `AppName: [DEV]`, `MaxPageSize: 10` |
| Prod override | Prod: `MaxPageSize: 100` |
| User Secrets | `ExternalApi:Key` not in `appsettings.json` |
| ApiKey masking | `{key[..4]}****` |
| Env variable override | `ProductHub__AppName` env var wins |
| Precedence chain comment | Documented in `Program.cs` |
| `/api/config/status` endpoint | Works, environment info correct |

---

## 📊 Topic → Task Map

| Topic | Task 1 | Task 2 | Task 3 | Task 4 |
|-------|--------|--------|--------|--------|
| **03. Middleware** | ✅ Custom (IMiddleware + Conventional) + UseWhen | | ✅ Dev/Prod middleware pipeline | |
| **04. Routing** | ✅ Route constraints, attribute routing | ✅ [FromQuery], [FromBody] | | |
| **05. Controllers & IActionResult** | ✅ 200/201/400/404 | ✅ CreatedAtAction, BadRequest(ModelState) | ✅ Audit endpoint | ✅ Config endpoint |
| **06. Model Binding & Validations** | | ✅ [Required], [Range], [BindNever], [FromBody], IValidatableObject, Custom Attr | | |
| **11. Dependency Injection** | | | ✅ Scoped/Singleton, captive dep, IServiceScopeFactory | ✅ IOptions<T> inject |
| **12. Environments** | | | ✅ IWebHostEnvironment, tag helper | ✅ Environment-specific config |
| **13. Configuration** | | | | ✅ Options Pattern, User Secrets, env var, precedence chain |

## 🏗️ Project Layer Chain

```
Task 1  →  Request infrastructure: middleware pipeline + routing + IActionResult
Task 2  →  Data security: model binding + validation + overposting protection
Task 3  →  Service layer: DI + environment awareness + captive dependency fix
Task 4  →  Configuration fortress: Options Pattern + Secrets + precedence chain proof
```

> 💬 Share your code after completing each task — we'll do a code review together!
