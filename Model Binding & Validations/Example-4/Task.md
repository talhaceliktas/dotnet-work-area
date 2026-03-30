# 🧪 Model Binding & Validations — Tasks
> This is the trickiest chapter so far. Every task is designed to expose a confusion point.  
> **Project:** Create `Example-4` with a fresh controller-based project.

---

## ✅ TASK 1 — Binding Source Confusion Test

**Goal:** Understand *exactly* where each binding attribute reads from — and what happens when sources conflict.

### Sub-Tasks:
- [ ] Create `OrderController` with route: `GET /order/{customerId}/{status?}`
- [ ] Action method signature:
  ```csharp
  public IActionResult Get(
      [FromRoute] int customerId,
      [FromQuery] string? status,
      [FromQuery] int? page
  )
  ```
- [ ] Return a JSON object showing all 3 values
- [ ] Now send this request and predict the output **before** running it:
  ```
  GET /order/42/active?customerId=999&status=pending&page=2
  ```
  **Question to answer:** `customerId` will be `42` or `999`? Why?

- [ ] Now **remove** `[FromRoute]` and `[FromQuery]` from all parameters. Send the same request again.  
  What does `customerId` bind to now? Does `status` still come from query string?  
  Write your reasoning as a comment in the code.

> **The trap:** Default model binding order is Form → Route → Query. Explicit attributes **override** this. Most devs get burned by this.

---

## ✅ TASK 2 — Model Class + Validation Layer

**Goal:** Build a full validated model from scratch, with every built-in attribute in use.

### Sub-Tasks:
- [ ] Create `Models/RegisterRequest.cs` with these properties and validation rules:

  | Property | Type | Rules |
  |---|---|---|
  | `Username` | `string` | Required, length 3–20, only `[a-zA-Z0-9_]` via RegularExpression |
  | `Email` | `string` | Required, valid email format |
  | `Phone` | `string` | Valid phone format |
  | `Password` | `string` | Required, min length 8 |
  | `ConfirmPassword` | `string` | Required, must match `Password` using `[Compare]` |
  | `Age` | `int` | Range 18–99 |
  | `Website` | `string?` | Optional, valid URL if provided |

- [ ] Create `POST /register` action that accepts `[FromBody] RegisterRequest model`
- [ ] If `ModelState.IsValid` is false → return `BadRequest(ModelState)`
- [ ] If valid → return `Ok(new { message = "Registered", user = model.Username })`

- [ ] Test these Postman scenarios:
  - Missing `Email` → expect 400
  - `Age = 15` → expect 400
  - `Password = "abc"` (too short) → expect 400
  - `ConfirmPassword` mismatch → expect 400
  - All valid → expect 200

> **The trap:** `[Compare]` only works when **both** properties exist. If `Password` is null, `[Compare]` on `ConfirmPassword` silently passes — test this.

---

## ✅ TASK 3 — Custom Validation Attributes + IValidatableObject

**Goal:** Implement cross-property and class-level validation that built-in attributes can't handle.

### Sub-Tasks:

**A) Custom Attribute:**
- [ ] Create `Attributes/FutureDateAttribute.cs` that inherits `ValidationAttribute`
- [ ] `IsValid` logic: the date must NOT be in the past. If it is → return error message
- [ ] Apply it to a `Models/EventRequest.cs` model: `StartDate` (DateTime, Required, FutureDate)

**B) IValidatableObject:**
- [ ] Add `EndDate` (DateTime?) to `EventRequest`
- [ ] Implement `IValidatableObject.Validate()` with this rule:
  - If `EndDate` has a value and `EndDate <= StartDate` → yield error: `"EndDate must be after StartDate"`
- [ ] **Critical question:** If `StartDate` fails `[FutureDateAttribute]`, does `Validate()` still run?  
  Test it and write the answer as a comment.

**C) Controller:**
- [ ] `POST /event` accepts `[FromBody] EventRequest`, checks `ModelState.IsValid`, returns 400 or 201

> **The trap:** `IValidatableObject.Validate()` does NOT run if any property-level annotation fails. This catches everyone off guard.

---

## ✅ TASK 4 — Over-posting Attack & Bind / BindNever

**Goal:** Simulate an over-posting attack and stop it with `[Bind]` and `[BindNever]`.

### Sub-Tasks:
- [ ] Create `Models/UserProfile.cs`:
  ```csharp
  public class UserProfile
  {
      public string Name { get; set; }
      public string Email { get; set; }
      public bool IsAdmin { get; set; }      // should NEVER be set by user
      public DateTime CreatedAt { get; set; } // should NEVER be set by user
  }
  ```
- [ ] `POST /profile` action that accepts `UserProfile` and returns `Ok(model)`
- [ ] **Attack test:** Send this JSON body and see what happens:
  ```json
  { "name": "Alice", "email": "a@b.com", "isAdmin": true, "createdAt": "2000-01-01" }
  ```
  — Is `IsAdmin` true in the response? **This is the over-posting vulnerability.**

- [ ] Fix it two ways:

  **Fix 1** — `[BindNever]` on the model:
  ```csharp
  [BindNever] public bool IsAdmin { get; set; }
  [BindNever] public DateTime CreatedAt { get; set; }
  ```

  **Fix 2** — `[Bind]` on the action parameter:
  ```csharp
  public IActionResult Create([Bind(nameof(UserProfile.Name), nameof(UserProfile.Email))] UserProfile model)
  ```

- [ ] Re-send the attack request after each fix. Does `IsAdmin` stay `false`?

> **Key insight:** `[BindNever]` is on the model (always safe). `[Bind]` is per-action (flexible but easy to forget). Which is safer for a `Create` endpoint vs. `Update` endpoint?

---

## ✅ TASK 5 — Custom Model Binder + [FromHeader] Token Auth

**Goal:** Build a custom model binder from scratch and use `[FromHeader]` for API key validation.

### Sub-Tasks:

**A) [FromHeader] API Key check:**
- [ ] All endpoints in a `SecureController` must receive `[FromHeader(Name = "X-Client-ID")] string clientId`
- [ ] If `clientId` is not `"VALID-123"` → return `Unauthorized("Invalid client ID")`
- [ ] Test: send request with no header → what's the ModelState error?  
  Send with wrong value → does your check catch it?

**B) Custom Model Binder:**
- [ ] Create `Models/SearchFilter.cs`:
  ```csharp
  public class SearchFilter
  {
      public string? Keyword { get; set; }
      public int Page { get; set; } = 1;
      public int PageSize { get; set; } = 10;
  }
  ```
- [ ] Create `Binders/SearchFilterBinder.cs` implementing `IModelBinder`:
  - Read `keyword` from query string via `bindingContext.ValueProvider.GetValue("keyword")`
  - Read `page`, default to `1` if missing or non-integer
  - Read `pageSize`, clamp it between 5 and 50 (user can't request more than 50 items)
  - Set `bindingContext.Result = ModelBindingResult.Success(filter)`

- [ ] Create `Binders/SearchFilterBinderProvider.cs` implementing `IModelBinderProvider`:
  - Return your binder if `context.Metadata.ModelType == typeof(SearchFilter)`

- [ ] Register in `Program.cs`:
  ```csharp
  builder.Services.AddControllers(options => {
      options.ModelBinderProviders.Insert(0, new SearchFilterBinderProvider());
  });
  ```

- [ ] `GET /search` action: `public IActionResult Search(SearchFilter filter)` — return `Ok(filter)`
- [ ] Test: `/search?keyword=hello&page=abc&pageSize=100`  
  Expected: `page=1` (fallback), `pageSize=50` (clamped), `keyword="hello"`

> **Why does `Insert(0, ...)` matter?** If your provider is not at index 0, ASP.NET's default binder runs first and handles `SearchFilter` before yours gets a chance. Test with `Add(...)` instead and see what breaks.

---

## 📊 Score Table

| Task | Topic | Difficulty |
|---|---|---|
| Task 1 | Binding source priority & conflicts | ⭐⭐⭐ |
| Task 2 | Full model validation with all built-ins | ⭐⭐⭐ |
| Task 3 | Custom attribute + IValidatableObject | ⭐⭐⭐⭐ |
| Task 4 | Over-posting attack + Bind/BindNever | ⭐⭐⭐⭐ |
| Task 5 | Custom model binder + FromHeader | ⭐⭐⭐⭐⭐ |
