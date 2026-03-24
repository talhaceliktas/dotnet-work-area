# dotnet-work-area

ASP.NET Core middleware ve Minimal API kavramlarını öğrenmek için hazırlanmış çalışma alanı. Her proje bağımsız olup farklı bir konsepti örnekler.

## Gereksinimler

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)

## Projeler

| Proje | Açıklama | Temel Kavram |
|---|---|---|
| **Calculator** | Basit aritmetik işlemler API'si | Query string parsing, Minimal API |
| **Dictionary** | Form verisi ayrıştırma | Request body okuma, `QueryHelpers` |
| **Middlewares** | Özel middleware zincirleme | `IMiddleware`, extension method, zamanlayıcı |
| **UseWhenExample** | Koşullu middleware dal oluşturma | `UseWhen` |
| **Example-1** | Ödeme koruma + global hata yakalama | `UseWhen`, global exception handler |

---

## Calculator

Sorgu parametreleriyle temel aritmetik işlemler yapan Minimal API.

**Endpoint:** `GET /calculate?firstNumber=X&secondNumber=Y&operation=Z`

Desteklenen operasyonlar: `plus`, `minus`, `multiply`, `divide`

```bash
cd Calculator
dotnet run
# http://localhost:5248/calculate?firstNumber=10&secondNumber=5&operation=multiply
```

**Örnek yanıt:**
```json
{
  "status": "Succes",
  "firstNumber": 10,
  "secondNumber": 5,
  "operation": "multiply",
  "result": 50
}
```

---

## Dictionary

POST isteğinin gövdesinden form-encoded veri okur; `QueryHelpers.ParseQuery` ile ayrıştırır ve birden fazla `price` değerini toplar.

```bash
cd Dictionary
dotnet run
# POST http://localhost:5000/
# Body (x-www-form-urlencoded): firstName=Ali&price=100&price=200
```

**Örnek yanıt:** `Isim: AliFiyatlar: 300`

---

## Middlewares

İki farklı middleware yazım stilini gösterir ve bunları zincirler.

| Middleware | Stil | İşlevi |
|---|---|---|
| `MyCustomMiddleware` | `IMiddleware` | İstek süresini konsola yazdırır |
| `HelloCustomMiddleware` | Constructor-based | `firstName`/`lastName` query param varsa selamlama mesajı döner |
| Anonim middleware | `app.Use(...)` | Fırlatılan hataları yakalar, `Status:Error` döner |

Her ikisi de `app.UseXxx()` extension method ile eklenir.

```bash
cd Middlewares
dotnet run
# GET http://localhost:5000/?firstName=Ali&lastName=Veli
# GET http://localhost:5000/hata   <- hata testi
```

---

## UseWhenExample

`UseWhen()` ile belirli bir yola gelen isteklere özel middleware uygulanır; diğer istekler bu middleware'i atlar.

```bash
cd UseWhenExample
dotnet run
# GET http://localhost:5000/api/gizli/herhangi   -> "Gizli Odaya hoşgeldin! Merhaba dünya!"
# GET http://localhost:5000/                     -> "Merhaba dünya!"
```

---

## Example-1

İki görevin birleşimi:

### Görev 1 — OdemeFedaisi (VIP Ödeme Koruyucusu)

`/api/odeme` yoluna gelen istekleri denetleyen güvenlik middleware'i. Yalnızca `UseWhen` kapsamında çalışır.

| Kural | Başarısız olursa |
|---|---|
| İstek POST olmalı | `405 Method Not Allowed` |
| `X-Firma-Token` header'ı olmalı | `401 Unauthorized` |
| Header değeri `MultiPayVIP` olmalı | `401 Unauthorized` |
| Her şey doğruysa | `X-Guvenlik-Kontrolu: Gecti` header'ı eklenir, `200 OK` |

### Görev 2 — GlobalErrorHandler (Kriz Masası)

Pipeline'ın en başına eklenen try-catch middleware'i. Herhangi bir yerde fırlatılan exception'ı yakalar.

```json
{
  "basari": false,
  "mesaj": "Sistemde beklenmeyen bir kriz oluştu, ekiplerimiz müdahale ediyor.",
  "hataDetayi": "<exception mesajı>"
}
```

```bash
cd Example-1
dotnet run
# GET  http://localhost:5000/           -> "Hello World!"
# GET  http://localhost:5000/bomb       -> 500 JSON hata yanıtı
# POST http://localhost:5000/api/odeme  (X-Firma-Token: MultiPayVIP) -> 200 Success
```

---

## Çalıştırma

Her proje bağımsız çalışır:

```bash
cd <ProjeKlasörü>
dotnet run
```

Tüm projeleri derlemek için solution kökünde:

```bash
dotnet build
```
