# 🎯 Dependency Injection — Senior Developer Task Series (English)

> **Course Analysis:** Harsha's 12-section DI module was fully analyzed.
> **Goal:** Don't copy concepts — **design and apply** them in your own scenarios.
> Each task contains a **scenario**, **requirements**, **hints**, and **evaluation criteria**.

---

## ✅ Task 1 — Interface-Based Service Design & Constructor Injection

### 🎯 Scenario

You are building an **e-commerce** application. You need a service that handles product stock control. Product data is currently stored in-memory, but it will be moved to a database later. The controller should **not be affected** by this transition at all.

### 📋 Requirements

1. Create a new ASP.NET Core MVC project: **`StockManager`**
2. Set up the following structure (separate class libraries, just like the course projects):
   - `ServiceContracts` — contains only interfaces
   - `Services` — implementations live here
   - `StockManager` — main web project
3. Design the `IProductStockService` interface. It must include:
   - `List<Product> GetAllProducts()`
   - `bool IsInStock(int productId)`
   - `bool UpdateStock(int productId, int quantity)`
4. Create the `ProductStockService` class. Store products in-memory using `List<Product>`.
5. Use this service in `ProductsController` via **constructor injection**.
6. The `Product` model should contain: `Id`, `Name`, `StockQuantity`.
7. The `Index` action returns all products; `Detail/{id}` returns the stock status.

### 💡 Hints

- The course example had only one method `GetCities()` in `ICitiesService`. Your interface has **multiple responsibilities** — is this correct from a DIP perspective? Think about it.
- Use `builder.Services.AddScoped<IProductStockService, ProductStockService>()` — why not `Transient`?
- **Never** write `new ProductStockService()` inside the controller. The DI container handles that.

### ✔️ Evaluation Criteria

| Criterion | Expected |
|-----------|----------|
| Interface separation | `ServiceContracts` is a separate project/namespace |
| No tight coupling | Controller depends only on the interface |
| DI registration | Registered with `AddScoped` in `Program.cs` |
| Working app | Product list and stock status are displayed |

---

## ✅ Task 2 — Prove Service Lifetime Differences with GUIDs

### 🎯 Scenario

A junior teammate asks you: *"What's the difference between Transient, Scoped, and Singleton — does it really matter?"* Explaining it in theory is not enough — you need to **prove it with code**.

### 📋 Requirements

1. New project: **`LifetimeDemo`**
2. Create three separate interfaces + implementations:
   - `ITransientOperation` → `TransientOperation`
   - `IScopedOperation` → `ScopedOperation`
   - `ISingletonOperation` → `SingletonOperation`
3. Each service generates a `Guid.NewGuid()` in its constructor and exposes it via an `OperationId` property.
4. Register with appropriate lifetimes in `Program.cs`:
   ```csharp
   builder.Services.AddTransient<ITransientOperation, TransientOperation>();
   builder.Services.AddScoped<IScopedOperation, ScopedOperation>();
   builder.Services.AddSingleton<ISingletonOperation, SingletonOperation>();
   ```
5. In `OperationController`, inject **all three services twice simultaneously** (constructor + ViewBag).
6. Display the GUIDs from two separate requests in the View in **table format**.
7. Add a `<div>` or comment explaining which GUIDs change on page refresh and which remain the same.

### 💡 Hints

- In the course project, `_citiesService1`, `_citiesService2`, `_citiesService3` were injected from the same interface three times. Apply the same technique for **three different lifetimes**.
- To confirm the Singleton GUID **never** changes without restarting the app, refresh the page 5 times without stopping it.
- Use `Guid.ToString("D")` to display GUIDs in a readable format.

### ✔️ Evaluation Criteria

| Criterion | Expected |
|-----------|----------|
| 3 lifetimes correctly registered | Transient / Scoped / Singleton registered separately |
| GUID observation | Transient changes each request, Scoped stays same per request, Singleton never changes |
| View output | Comparative table displayed |
| Explanation | A comment/text explaining why each behaves the way it does |

---

## ✅ Task 3 — Captive Dependency Trap & IServiceScopeFactory Fix

### 🎯 Scenario

A very common real-world mistake: **injecting a Scoped service into a Singleton**. This is called the "captive dependency" problem. First write it in its **broken state**, observe the error, then fix it using `IServiceScopeFactory`.

### 📋 Requirements

**Phase 1 — Create the Broken Version**

1. New project: **`CaptiveDependencyDemo`**
2. `IOrderRepository` (Scoped) → `OrderRepository`: stores orders in memory.
3. `IOrderProcessingService` (Singleton) → `OrderProcessingService`:
   - Injects `IOrderRepository` in its constructor (**wrong!**)
   - Has a `ProcessPendingOrders()` method.
4. Run the application, observe the runtime exception, and add a comment in `Program.cs` explaining why the error occurred.

**Phase 2 — Correct Solution**

5. Fix `OrderProcessingService`:
   - Inject **`IServiceScopeFactory`** instead of `IOrderRepository` in the constructor.
   - Inside `ProcessPendingOrders()`, open a scope with `IServiceScopeFactory.CreateScope()`, get the repository from it, do the work, then dispose the scope.

**Phase 3 — Verify**

6. Call `ProcessPendingOrders()` from an endpoint and return the result.

### 💡 Hints

- Error message: *"Cannot consume scoped service from singleton"* — try to produce this yourself rather than searching for it.
- After `using var scope = _scopeFactory.CreateScope();`, use `scope.ServiceProvider.GetRequiredService<IOrderRepository>()` to get the service.
- This pattern is especially common inside **background services** (HostedService).

### ✔️ Evaluation Criteria

| Criterion | Expected |
|-----------|----------|
| Broken version | Captive dependency error intentionally produced |
| Error explanation | Explanatory comment added in `Program.cs` |
| Correct fix | `IServiceScopeFactory` used correctly |
| Scope disposal | Scope properly closed with `using` |

---

## ✅ Task 4 — Cross-Cutting Concern with Decorator Pattern (Caching Layer)

### 🎯 Scenario

The course notes mention under Best Practices: *"Use Decorators to Add Cross-Cutting Concerns."* This task puts that principle into practice with a real scenario.

You have a slow `IWeatherService` (simulating an external API with `Task.Delay`). Instead of waiting on every call, you will write a **caching decorator**. `WeatherController` will know **nothing** about the decorator — the DI container will compose everything.

### 📋 Requirements

1. New project: **`WeatherWithCaching`**
2. `IWeatherService` interface:
   ```csharp
   public interface IWeatherService
   {
       Task<WeatherReport> GetCurrentWeatherAsync(string city);
   }
   ```
3. `WeatherReport` model: `City`, `TemperatureCelsius`, `Description`, `FetchedAt` (DateTime).
4. `RealWeatherService`: implements `IWeatherService`. Simulate a slow API with `Task.Delay(2000)` and generate a random temperature.
5. **`CachingWeatherServiceDecorator`**: implements `IWeatherService`.
   - Constructor takes `IWeatherService inner` and `IMemoryCache cache`.
   - In `GetCurrentWeatherAsync`: if the result is in cache, return it; otherwise call `inner.GetCurrentWeatherAsync()`, cache the result for 30 seconds, then return it.
6. `WeatherController` only uses `IWeatherService` (knows nothing about the decorator).
7. DI registrations in `Program.cs`:
   ```csharp
   builder.Services.AddMemoryCache();
   builder.Services.AddScoped<RealWeatherService>();
   builder.Services.AddScoped<IWeatherService>(sp =>
       new CachingWeatherServiceDecorator(
           sp.GetRequiredService<RealWeatherService>(),
           sp.GetRequiredService<IMemoryCache>()
       ));
   ```
8. In the View: if `FetchedAt` doesn't change, display a message indicating the response came from cache.

### 💡 Hints

- `builder.Services.AddMemoryCache()` is built-in — no additional NuGet package is required.
- The decorator **must implement** the `IWeatherService` interface and should not depend on anything other than it.
- Use `$"weather_{city.ToLower()}"` as the cache key.
- **OCP (Open/Closed Principle):** You didn't modify `RealWeatherService` at all — you extended its behavior without touching it.

### ✔️ Evaluation Criteria

| Criterion | Expected |
|-----------|----------|
| Interface separation | Controller only knows `IWeatherService` |
| Decorator correctly implements | `CachingWeatherServiceDecorator : IWeatherService` |
| Cache works | Second call for the same city takes ~0ms |
| DI registration | Correctly composed via factory lambda |
| OCP respected | `RealWeatherService` was not modified |

---

## 📊 Difficulty Map

```
Task 1: ████████░░  Interface Design & Constructor Injection     (Beginner-Intermediate)
Task 2: ████████░░  Lifetime Observation & Proof                 (Intermediate)
Task 3: ██████████  Captive Dep. & IServiceScopeFactory          (Intermediate-Advanced)
Task 4: ██████████  Decorator Pattern & Composition Root         (Advanced)
```

## 🗓 Recommended Order

| Step | Task | Goal |
|------|------|------|
| 1 | Task 1 | Solidify DI fundamentals |
| 2 | Task 2 | Understand lifetimes through observation, not memorization |
| 3 | Task 3 | Produce then fix a real production bug |
| 4 | Task 4 | Combine design patterns with DI → senior level |

> 💬 Show me your code after completing each task — I'll do a thorough code review just like a real senior developer would.
