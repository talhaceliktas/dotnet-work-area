# 🧪 ASP.NET Core — Controllers & IActionResult Tasks
> **Prerequisites:** Middleware pipeline, Routing, Custom Constraints already covered — not repeated here.  
> **Setup:** Add a new `Controllers/` folder to your project and implement each task there.

---

## ✅ TASK 1 — Transition to Controller Architecture

**Goal:** Move away from Minimal API endpoints and set up real MVC Controller architecture.

### Sub-Tasks:
- [ ] Add `builder.Services.AddControllers()` and `app.MapControllers()` to `Program.cs`
- [ ] Create a `Controllers/` folder
- [ ] Create a controller class named `KatalogController`:
  - Mark it with `[Controller]` attribute (class name already ends with "Controller" — this is the second way)
  - Inherit from the `Controller` base class
  - Add the following action methods:

    | Method | Route | Return |
    |---|---|---|
    | `Index()` | `GET /katalog` and `GET /` | `Content("Catalog Home Page", "text/plain")` |
    | `About()` | `GET /katalog/about` | `Content("<h1>About Us</h1>", "text/html")` |
    | `Contact(string phone)` | `GET /katalog/contact/{phone:regex(^\\d{10}$)}` | Return the phone number as content |

- [ ] Test all 3 routes from the browser

> **Note:** Instead of `MapGet(...)` calls, you're now using `[Route]` attributes. Notice the difference between `[HttpGet]` and `[Route]`.

---

## ✅ TASK 2 — Smart Response Management with IActionResult

**Goal:** Return different `IActionResult` types conditionally from a single action method.

### Sub-Tasks:
- [ ] Create `BookController`, with a single action: `GET /book`
- [ ] Read the following values from the query string (directly as function parameters):
  - `int? bookId` — required, must be between 1 and 1000
  - `bool? isLoggedIn` — must be true
- [ ] Apply the following rules **in order**, returning the correct `IActionResult` for each:

  | Condition | Return |
  |---|---|
  | `bookId` not provided | `BadRequest("bookId is required")` |
  | `bookId <= 0` | `BadRequest("bookId must be greater than 0")` |
  | `bookId > 1000` | `NotFound("No book found with this ID")` |
  | `isLoggedIn != true` | `Unauthorized()` |
  | All checks pass | `Content("Book info: " + bookId, "text/plain")` |

- [ ] Test every scenario in Postman and verify the status codes are correct
- [ ] **Bonus:** For the `isLoggedIn == false` case, return `StatusCode(403, "Logged in but not authorized")` instead of `Unauthorized()`. Think about the difference between 401 and 403.

---

## ✅ TASK 3 — File Results & JSON Responses

**Goal:** Implement 3 different ways to serve files and practice JSON serialization.

### Sub-Tasks:

**A) JSON response:**
- [ ] Create `PersonController`, `GET /person` endpoint:
  - Define `record Person(Guid Id, string FirstName, string LastName, int Age)` (outside the controller, below Program.cs)
  - In the action method, create a `Person` instance and return it using `Json(person)`
  - Observe that the JSON comes back in `camelCase` format in the browser

**B) File serving:**
- [ ] Create `sample.txt` inside `wwwroot/` (content: "VirtualFile works")
- [ ] Create `FileController` with 3 action methods:

  | Method | Route | Type |
  |---|---|---|
  | `VirtualFile()` | `GET /file/virtual` | `File("/sample.txt", "text/plain")` |
  | `MemoryFile()` | `GET /file/memory` | Read `sample.txt` with `ReadAllBytes`, return `File(bytes, "text/plain")` |
  | `DownloadableFile()` | `GET /file/download` | Virtual file + also set `FileDownloadName = "downloaded.txt"` |

- [ ] Check whether the browser triggers a file download when hitting `/file/download`

> **Think about it:** Is there a performance difference between `VirtualFileResult` and `FileContentResult`? When should each be preferred?

---

## ✅ TASK 4 — Redirect Types & Security

**Goal:** Distinguish between the 3 redirect types and see how open redirect attacks are prevented.

### Sub-Tasks:
- [ ] Create `RedirectController`:

  | Method | Route | Behavior |
  |---|---|---|
  | `Temporary()` | `GET /redirect/temporary` | `RedirectToAction("Index", "Katalog")` → 302 |
  | `Permanent()` | `GET /redirect/permanent` | `RedirectToActionPermanent("About", "Katalog")` → 301 |
  | `Local()` | `GET /redirect/local` | `LocalRedirect("/katalog")` → 302 |
  | `External()` | `GET /redirect/external` | `Redirect("https://example.com")` → 302 |

- [ ] In Postman, inspect the `Location` header for each and verify whether the status code is 301 or 302

- [ ] **Security test — Open Redirect:**
  - Add a new action: `GET /redirect/user?url=...`
  - Read the `url` parameter from query string, redirect with `Redirect(url)`
  - Test with `/redirect/user?url=https://evil.com` → it works
  - Now replace `Redirect(url)` with `LocalRedirect(url)` and try the same request → what happens?
  - Why does `LocalRedirect` reject external URLs? How does the framework know?

> **Takeaway:** Never use `Redirect()` with user-supplied URLs. `LocalRedirect()` blocks this attack at the framework level.

---

## 📊 Score Table

| Task | Topic | Difficulty |
|---|---|---|
| Task 1 | Controller architecture setup | ⭐⭐ |
| Task 2 | IActionResult decision tree | ⭐⭐⭐ |
| Task 3 | File results + JSON | ⭐⭐⭐ |
| Task 4 | Redirect types + security | ⭐⭐⭐⭐ |
