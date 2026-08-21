# Rohit Sole & Stitch — Backend (ECommerce API)

.NET (ASP.NET Core) backend for the Rohit Sole & Stitch shoe store — built with an Onion Architecture (Domain / Application / Infrastructure / API) and PostgreSQL.

## Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL (Npgsql)
- Swagger (API docs)

## Project Structure

```
ECommerceApp/
├── ECommerce.API/            # Controllers, Program.cs, appsettings
├── ECommerce.Application/    # DTOs, Services, Interfaces
├── ECommerce.Domain/         # Entities
└── ECommerce.Infrastructure/ # DbContext, Repositories, Migrations
```

## Setup

### 1. Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (net10.0)
- [PostgreSQL](https://www.postgresql.org/download/) running locally (default port `5432`)

### 2. Create the database

Open **SQL Shell (psql)** and run:

```sql
CREATE DATABASE ecommerce;
```

### 3. Configure the connection string

This repo does **not** include `appsettings.json` (it's git-ignored to keep credentials out of source control). Create it yourself inside `ECommerce.API/`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=ecommerce;Username=postgres;Password=YOUR_PASSWORD"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Cors": {
    "AllowedOrigins": [ "http://localhost:5173", "http://localhost:3000" ]
  }
}
```

Replace `YOUR_PASSWORD` with your local PostgreSQL password.

### 4. Run the API

```powershell
cd ECommerceApp/ECommerce.API
dotnet run
```

Migrations are applied automatically on startup (`db.Database.Migrate()` in `Program.cs`), so the tables and seed data (categories + a default admin user) will be created the first time you run it.

The API will start at something like `https://localhost:XXXX` / `http://localhost:XXXX` — Swagger docs are available at `/swagger` in development.

### Default admin login

- Email: `admin@shoeshop.com`
- Password: `Admin@123`

## Notes

- CORS is configured to allow requests from `http://localhost:5173` and `http://localhost:3000` (the frontend dev servers) — update `Cors:AllowedOrigins` in `appsettings.json` if your frontend runs elsewhere.
- `appsettings.json`, `bin/`, `obj/`, and local SQLite files are git-ignored — see `.gitignore`.
