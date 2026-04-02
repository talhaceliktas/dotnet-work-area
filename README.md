# 🧱 .NET Work Area

A structured learning and practice workspace for **ASP.NET Core** development.  
Each top-level folder is an independent topic module containing one or more example projects that progressively cover core framework concepts.

---

## 📁 Project Structure

```
dotnet-work-area/
├── Controllers/                     # MVC Controllers & IActionResult
│   ├── ControllersExample/          # Basic controller setup, routing, models
│   ├── IActionResultExample/        # Exploring IActionResult return types
│   └── Example-3/                   # Additional controller scenarios
│
├── DependencyInjection/             # Dependency Injection (DI) in ASP.NET Core
│   ├── DIExample/                   # Constructor injection, service registration
│   ├── Examples/                    # DI lifetime comparisons (Transient/Scoped/Singleton)
│   ├── ServiceContracts/            # Interface-based service contracts
│   └── Services/                    # Concrete service implementations
│
├── EnvironmentsExample/             # Environment-specific configuration
│   ├── appsettings.json             # Base configuration
│   └── appsettings.Development.json # Development overrides
│
├── HTTP-Middleware/                 # Custom Middleware & Request Pipeline
│   ├── Calculator/                  # Middleware-based calculator app
│   ├── Dictionary/                  # Dictionary lookup middleware
│   ├── Middlewares/                 # Reusable custom middleware classes
│   ├── UseWhenExample/              # Conditional middleware branching (UseWhen)
│   └── Example-1/                   # Basic middleware introduction
│
├── Model Binding And Validations/   # Model Binding, Validation & Data Annotations
│   ├── Model Binding/               # Query string, route, body binding
│   ├── ModelValidationExample/      # Data annotations & IValidatableObject
│   └── Example-4/                   # Advanced binding scenarios
│
└── Routing/                         # Routing & Static Files
    ├── RoutingExample/              # Conventional & attribute routing
    ├── StaticFilesExample/          # Serving static files (wwwroot)
    └── Example-2/                   # Additional routing patterns
```

---

## 🗂️ Topic Modules

### 1. Controllers
Covers MVC controller fundamentals — action methods, routing conventions, model binding at the controller level, and the various `IActionResult` return types (`Ok`, `View`, `Redirect`, `NotFound`, etc.).

**Solution:** `Controllers/controllers.slnx`

---

### 2. Dependency Injection
Explores ASP.NET Core's built-in DI container. Topics include:
- Defining service contracts via interfaces (`IStockService`, etc.)
- Registering services with `AddTransient`, `AddScoped`, `AddSingleton`
- Constructor injection in controllers and services
- Separation of `ServiceContracts` and `Services` into dedicated projects

**Solution:** `DependencyInjection/dependency-injection.slnx`

---

### 3. Environments
Demonstrates how to work with multiple environments (`Development`, `Staging`, `Production`) in ASP.NET Core using `IWebHostEnvironment`, `appsettings.{Environment}.json`, and environment-specific middleware.

**Solution:** `EnvironmentsExample/EnvironmentsExample.slnx`

---

### 4. HTTP Middleware
Walks through the ASP.NET Core request pipeline and middleware:
- Writing custom middleware with `IMiddleware` and `Use` / `Run` / `Map`
- Branching pipelines with `UseWhen`
- Real-world examples: calculator and dictionary apps built entirely with middleware

**Solution:** `HTTP-Middleware/http-middleware.slnx`

---

### 5. Model Binding & Validations
Covers how ASP.NET Core maps HTTP request data to action parameters and models:
- Binding sources: `[FromQuery]`, `[FromRoute]`, `[FromBody]`, `[FromForm]`
- Data annotations: `[Required]`, `[Range]`, `[StringLength]`, etc.
- Custom validation with `IValidatableObject`
- Over-posting prevention patterns

**Solution:** `Model Binding And Validations/model-bindings-and-validations.slnx`

---

### 6. Routing
Examines both conventional and attribute-based routing:
- Route templates, constraints, and default values
- Serving static files from `wwwroot`
- Route grouping and endpoint configuration

**Solution:** `Routing/routing.slnx`

---

## ⚙️ Tech Stack

| Technology | Version |
|---|---|
| Runtime | .NET 8 / .NET 9 |
| Framework | ASP.NET Core (MVC) |
| Language | C# |
| Solution Format | `.slnx` (SDK-style) |
| IDE | Visual Studio 2022 |

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download) or later
- Visual Studio 2022 (17.9+) or VS Code with C# DevKit

### Running a Project

```bash
# Navigate to any example project
cd DependencyInjection/DIExample

# Restore & run
dotnet run
```

Or open the corresponding `.slnx` solution file in Visual Studio and press **F5**.

---

## 📌 Notes

- Each folder is a self-contained learning module — you don't need to run them together.
- Projects prefixed with `Example-N` are supplementary exercises that reinforce the main examples in the same topic.
- `wwwroot/` directories contain static assets (CSS, JS, images) for projects that serve a UI.
