
# Purchase_Microservice - Dokumentacija projekta







## Pregled projekta
Purchase_Microservice je mikroservis razvijen u .NET 9 okruženju, koji implementira Domain-Driven Design (DDD) pristup za upravljanje kupovinama. Mikroservis nudi CRUD funkcionalnosti za upravljanje podacima o kupovinama, uključujući detalje o kupcima, isporuci i oglasima.

Ovaj projekat demonstrira najbolje prakse u razvoju mikroservisa uz integraciju alata za validaciju, logovanje i API dokumentaciju.


## Funkcionalnosti
- CRUD operacije za upravljanje podacima o kupovinama.
- Validacija unosa pomoću FluentValidation biblioteke.
- Logovanje pomoću Serilog biblioteke.
- Upravljanje bazom podataka korišćenjem Entity Framework Core i SQL Server baze.
- API dokumentacija sa Swagger/OpenAPI.
- Mapiranje podataka pomoću AutoMapper biblioteke.

## Korišćene tehnologije

- .NET 9
- Entity Framework Core (Code-First pristup)
- MSSQL (SQL Server) za bazu podataka
- Swagger/OpenAPI za API dokumentaciju
- FluentValidation za validaciju DTO objekata
- Serilog za logovanje
- AutoMapper za mapiranje DTO u entitete i obrnuto
- Docker (opciono, za kontejnerizaciju aplikacije)

## Pokretanje projekta
## 1. Preduslovi
Pre pokretanja projekta potrebno je da imate sledeće instalirano:

- .NET SDK 9 (Preuzmi ovde)
- SQL Server (Preuzmi ovde)
- Git (za kloniranje repozitorijuma)

## 2. Instalacija
 Klonirajte repozitorijum:
```bash
git clone https://github.com/srdjanbozic/PurchaseRepository.git
cd PurchaseRepository
```

Instalirajte zavisnosti:
```bash
dotnet restore
```
Ažurirajte bazu podataka:
```bash
dotnet ef database update
```
Pokrenite aplikaciju:
```bash
dotnet run --project Purchase_Microservice/Purchase_Microservice.csproj
```

## Pristup API-ju
Swagger UI:
https://localhost:7237/swagger

Osnovni API URL:
https://localhost:7237

## API Endpoints


| Method | Endpoint     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `GET`      | `/api/purchases` | Prikazuje sve kupovine
 |
| `GET`      | `/api/purchases/{id}	`    | Prikazuje kupovinu po ID-u
 |
| POST       | /api/purchases	          | Kreira novu kupovinu
 |
| PUT         | /api/purchases/{id}	       | Ažurira postojeću kupovinu
|
| DELETE       | /api/purchases/{id}	    | Briše kupovinu po ID-u|










## API Referenca

#### Preuzmi sve kupovine

```http
  GET /api/Purchase
```

Ovaj endpoint vraća sve kupovine iz baze podataka.



#### Preuzmi kupovinu po Id

```http
  GET /api/Project/${id}
```
Ovaj endpoint vraća jednu kupovinu sa zadatim Id.
-
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Obavezno.** Id kupovine koju treba preuzeti |

#### Kreiraj kupovinu


```http
  POST /api/Purchase
```
Ovaj endpoint kreira novu kupovinu u bazi podataka.
-

#### Ažuriraj kupovinu
```http
  PUT /api/Purchase/ ${id}
```
This endpoint updates a single purchase with specified Id in the database.

#### Remove purchase
```http
  DELETE /api/Project/ ${id}
```
Ovaj endpoint ažurira pojedinačnu kupovinu sa zadatim Id u bazi podataka.





## Projektna structure
Projekat je organizovan prema **DDD** principima:
```bash
├── Application
│   ├── Controllers           # API kontroleri
│   ├── Dtos                  # DTO objekti za transfer podataka
│   ├── Validators            # FluentValidation validatori
│   └── MapperProfiles        # AutoMapper profili
│
├── Domain
│   ├── Entities              # Entiteti domena (Purchase, Buyer, Post, Delivery)
│
├── Infrastructure
│   ├── Persistence           # DbContext i konfiguracija baze podataka
│   ├── Repositories          # Repozitorijumi sa implementacijom logike
│   └── Logging               # Konfiguracija za Serilog
│
├── Tests                     # Jedinični i integracioni testovi
│
└── Purchase_Microservice.sln # Rešenje projekta
```

## Konfiguracija
**Baza podataka**
Connection string se nalazi u appsettings.json fajlu:
```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=PurchaseDatabase;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```
## Validacija
Validacija podataka implementirana je pomoću FluentValidation:

- PurchaseCreateDtoValidator: Validacija unosa prilikom kreiranja kupovine.
- PurchaseUpdateDtoValidator: Validacija unosa prilikom ažuriranja kupovine.
Primer pravila za PurchaseCreateDto:
```bash 
RuleFor(x => x.PurchasePrice)
    .NotEmpty().WithMessage("Purchase price is required")
    .GreaterThan(0).WithMessage("Purchase price must be greater than 0");

RuleFor(x => x.BuyerEmail)
    .NotEmpty().WithMessage("Buyer email is required")
    .EmailAddress().WithMessage("Invalid email format");

```
## Logovanje
Logovanje je implementirano pomoću Serilog biblioteke. Logovi se čuvaju u konzoli i fajlu:
```bash
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
```
## Primer rada aplikacije
**Kreiranje kupovine (POST /api/purchases)**
**Ulazni DTO (PurchaseCreateDto):**
```bash
{
  "PurchaseDate": "2024-12-17",
  "PurchasePrice": 120.50,
  "Currency": "USD",
  "BuyerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "BuyerUsername": "john_doe",
  "BuyerEmail": "john@example.com",
  "DeliveryId": "e7b30d64-4716-41e8-9038-2c2e303fdd6d",
  "DeliveryPrice": 12.99,
  "PostId": "c321f2e6-85c4-4a1b-b3c2-017d34e3a67d",
  "PostTitle": "New Laptop",
  "PostDate": "2024-12-15",
  "PostPrice": 120.50,
  "OwnerId": "f82b64e4-74b5-4e8e-a4d3-c2a2e374b08e",
  "OwnerName": "Jane Doe",
  "OwnerEmail": "jane@example.com",
  "OwnerPhone": "123456789"
}
```
**Odgovor**
```bash
{
  "PurchaseId": "d6479e6e-4188-4df8-8c36-d795c251df5d",
  "PurchaseDate": "2024-12-17",
  "PurchasePrice": 120.50,
  "Currency": "USD",
  "BuyerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "BuyerUsername": "john_doe",
  "BuyerEmail": "john@example.com",
  "DeliveryId": "e7b30d64-4716-41e8-9038-2c2e303fdd6d",
  "DeliveryPrice": 12.99,
  "PostId": "c321f2e6-85c4-4a1b-b3c2-017d34e3a67d",
  "PostTitle": "New Laptop",
  "PostDate": "2024-12-15",
  "PostPrice": 120.50,
  "OwnerId": "f82b64e4-74b5-4e8e-a4d3-c2a2e374b08e",
  "OwnerName": "Jane Doe",
  "OwnerEmail": "jane@example.com",
  "OwnerPhone": "123456789"
}
```
## Licenca

Ovaj projekat je licenciran pod MIT licencom. Pogledajte fajl LICENSE za više informacija.

