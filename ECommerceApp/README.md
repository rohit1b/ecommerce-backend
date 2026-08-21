# Sole & Stitch — Backend (Onion Architecture, ASP.NET Core + SQLite)

Layers:
- ECommerce.Domain        -> entities only (Category, Item, Order, OrderItem, AdminUser)
- ECommerce.Application    -> DTOs, interfaces, business logic (services)
- ECommerce.Infrastructure -> EF Core DbContext, repositories, SQLite database
- ECommerce.API            -> Controllers, Program.cs (the web project you actually run)

Database is SQLite (a single file, ecommerce.db) — no SQL Server install needed.
It seeds 4 categories (GentsWear, LadiesWear, MeansWear, ChildrenWear) and 1 admin login.

## How to run this (step by step, in VS Code)

1. Open this `ECommerceApp` folder in VS Code (File > Open Folder).
2. Open a terminal in VS Code: Terminal > New Terminal.
3. Install the EF Core CLI tool (only needed once on your machine):
   ```
   dotnet tool install --global dotnet-ef
   ```
4. Restore all NuGet packages:
   ```
   dotnet restore
   ```
5. Create the database migration (only needed once, or whenever you change an entity):
   ```
   cd ECommerce.API
   dotnet ef migrations add InitialCreate --project ../ECommerce.Infrastructure --startup-project .
   ```
6. Run the API:
   ```
   dotnet run
   ```
   The database file and tables are created automatically on startup (Program.cs calls
   `db.Database.Migrate()`), so you don't need a separate "update database" step.
7. It should open Swagger in your browser at something like http://localhost:5000/swagger
   — that's a page where you can test every API endpoint directly.

Demo login (seeded automatically):
- Email: admin@shoeshop.com
- Password: Admin@123

## If port 5000 is already used on your machine

Edit `ECommerce.API/Properties/launchSettings.json` and change `"applicationUrl"`, then also
update `API_BASE_URL` in the React app's `src/api/api.js` to match.

## API endpoints

- POST   /api/auth/login
- GET/POST/PUT/DELETE  /api/category, /api/category/{id}
- GET/POST/PUT/DELETE  /api/item, /api/item/{id}
- PUT    /api/item/add-quantity   (body: { itemId, amount })
- GET/POST  /api/order
- PUT    /api/order/{id}/status   (body: { status: "Complete" | "Pending" })
