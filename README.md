# Bakery Management API

Bakery Management API helps a bakery manage its product catalog, customers, orders, and order items in one place. It provides paginated and filterable endpoints, calculates order totals as items change, and enforces rules that protect order history and prevent invalid operations. The API is built with C# and .NET 10 using ASP.NET Core, Entity Framework Core, and SQLite.

## What the application does

- Manage products and customers through CRUD endpoints.
- Create customer orders, add products, calculate totals, and move orders through an allowed status lifecycle.
- Preserve order history by preventing deletion of customers or products referenced by orders and retaining cancelled orders.
- Validate input and referenced entities, prevent duplicate customer emails and duplicate products on an order, and return consistent API errors.

## Project structure

The solution uses Clean Architecture, split into four projects:

| Project                           | Responsibility                                                                                                                               | Where to look                                   |
| --------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------- |
| `BakeryManagement.API`            | HTTP controllers, dependency injection, configuration, Swagger, and exception-to-HTTP handling.                                              | `Controllers/`, `Middleware/`, `Program.cs`     |
| `BakeryManagement.Application`    | Use cases and business workflows, request/response DTOs, service and repository interfaces, mapping, pagination, and application exceptions. | `Services/`, `DTOs/`, `Interfaces/`, `Mapping/` |
| `BakeryManagement.Domain`         | Core entities, enums, and domain rules that do not depend on HTTP or persistence.                                                            | `Entities/`, `Enums/`                           |
| `BakeryManagement.Infrastructure` | EF Core SQLite persistence, entity configuration, repository implementations, migrations, and seed data.                                     | `Persistence/`, `Repositories/`                 |

The API calls Application services. Application services use Domain entities and repository interfaces; Infrastructure implements those interfaces with EF Core. `Program.cs` connects the pieces through dependency injection.

## Requirements

- .NET 10 SDK
- Visual Studio with the ASP.NET and web development workload (Windows), or the .NET SDK on macOS

## Clone and restore

Clone the repository, enter the cloned repository folder, then enter its `BakeryManagement` directory. This inner directory contains `BakeryManagement.slnx`; run commands from here.

```sh
git clone https://github.com/TimeaTorok/BakeryAPI.git my-bakery-api
cd my-bakery-api/BakeryManagement
dotnet restore BakeryManagement.slnx
```

If you downloaded a ZIP, extract it and enter the inner `BakeryManagement` directory that contains the solution file.

## Initialize the database

The SQLite initial migration is already included in the repository. From the directory containing `BakeryManagement.slnx`, install the cross-platform EF Core CLI tool and apply the migration:

```sh
dotnet tool install --global dotnet-ef --version 10.0.15
dotnet ef database update --project BakeryManagement.Infrastructure --startup-project BakeryManagement.API
```

This creates the local SQLite database, applies the schema, and inserts the seed data: 90 products, 5 customers, 3 orders, and 6 order items (104 records total).

The API does not run migrations automatically. After changing the model, create and apply a migration using either option from the solution directory. Replace `AddInventoryFields` with a descriptive migration name.

.NET CLI:

```sh
dotnet ef migrations add AddInventoryFields --project BakeryManagement.Infrastructure --startup-project BakeryManagement.API --output-dir Persistence/Migrations
dotnet ef database update --project BakeryManagement.Infrastructure --startup-project BakeryManagement.API
```

Visual Studio Package Manager Console (Default project: `BakeryManagement.Infrastructure`):

```powershell
Add-Migration AddInventoryFields -Project BakeryManagement.Infrastructure -StartupProject BakeryManagement.API -OutputDir Persistence\Migrations
Update-Database
```

The Infrastructure project includes `Microsoft.EntityFrameworkCore.Tools` and the API project includes `Microsoft.EntityFrameworkCore.Design` (both version `10.0.15`).

## Run the API

From the solution directory:

```sh
dotnet run --project BakeryManagement.API
```

In Development, Swagger is available at [http://localhost:5069/swagger](http://localhost:5069/swagger). The HTTPS launch profile uses `https://localhost:7089/swagger`.

The default database file is `BakeryManagement.API/data/bakery-management.db`. Its relative path is resolved from the API content root, and the API creates the `data` directory when it starts. You can override the path with the `ConnectionStrings__BakeryManagementConnection` environment variable.

PowerShell:

```powershell
$env:ConnectionStrings__BakeryManagementConnection = "Data Source=$PWD\local.db"
dotnet run --project BakeryManagement.API
```

macOS/Linux:

```sh
ConnectionStrings__BakeryManagementConnection="Data Source=$PWD/local.db" dotnet run --project BakeryManagement.API
```

To apply migrations to an overridden database, set the same connection string before updating.

.NET CLI (macOS/Linux):

```sh
ConnectionStrings__BakeryManagementConnection="Data Source=$PWD/local.db" dotnet ef database update --project BakeryManagement.Infrastructure --startup-project BakeryManagement.API
```

PowerShell:

```powershell
$env:ConnectionStrings__BakeryManagementConnection = "Data Source=$PWD\local.db"
dotnet ef database update --project BakeryManagement.Infrastructure --startup-project BakeryManagement.API
```

## API reference

All routes are under `/api`. Swagger provides an interactive API reference while the app is running in Development.

### Products - `/api/products`

| Method and route            | Behavior                                                                                                                    |
| --------------------------- | --------------------------------------------------------------------------------------------------------------------------- |
| `GET /api/products`         | Get a paged product list. Supports `pageNumber`, `pageSize`, `category`, `isAvailable`, and optional `name` search filters. |
| `GET /api/products/{id}`    | Get one product by ID.                                                                                                      |
| `POST /api/products`        | Create a product.                                                                                                           |
| `PUT /api/products/{id}`    | Update a product.                                                                                                           |
| `DELETE /api/products/{id}` | Delete a product only if it is not referenced by any order item; otherwise returns a conflict.                              |

Product names and categories are required. Categories must be `Bread`, `Pastry`, `Cake`, `Cookie`, `Muffin`, or `Other`; prices must be non-negative with at most two decimal places. Products referenced by past orders cannot be deleted, preserving order history.

The optional `name` filter matches any part of a product name, case-insensitively. Example: `GET /api/products?pageNumber=1&pageSize=10&category=Bread&isAvailable=true&name=br`.

### Customers - `/api/customers`

| Method and route             | Behavior                                                                                  |
| ---------------------------- | ----------------------------------------------------------------------------------------- |
| `GET /api/customers`         | Get a paged customer list. Supports `pageNumber`, `pageSize`, and optional `name` search. |
| `GET /api/customers/{id}`    | Get one customer by ID.                                                                   |
| `POST /api/customers`        | Create a customer. Email addresses must be unique.                                        |
| `PUT /api/customers/{id}`    | Update a customer. Email addresses must remain unique.                                    |
| `DELETE /api/customers/{id}` | Delete a customer only if they have no orders; otherwise returns a conflict.              |

The optional `name` filter matches any part of the customer's full name (first and last name), case-insensitively. Example: `GET /api/customers?pageNumber=1&pageSize=10&name=jane`.

### Orders - `/api/orders`

| Method and route       | Behavior                                                                                                                                     |
| ---------------------- | -------------------------------------------------------------------------------------------------------------------------------------------- |
| `GET /api/orders`      | Get a paged order list. Supports `pageNumber`, `pageSize`, `customerId`, and `status` filters.                                               |
| `GET /api/orders/{id}` | Get order details, including all attached order items.                                                                                       |
| `POST /api/orders`     | Create an order for an existing customer, with optional notes. The API sets its ID, creation time, initial `Pending` status, and zero total. |
| `PUT /api/orders/{id}` | Update the status and optionally the notes, subject to the allowed status transitions.                                                       |

Orders are retained for historical reference, so there is no delete endpoint. Cancel an order by changing its status to `Cancelled` while that transition is allowed. Valid transitions are `Pending` -> `Confirmed` -> `InPreparation` -> `ReadyForPickup` -> `Completed`; an order can also be cancelled from `Pending`, `Confirmed`, `InPreparation`, or `ReadyForPickup`. `Completed` and `Cancelled` are terminal states.

Example: `GET /api/orders?pageNumber=1&pageSize=10&customerId={customerId}&status=Pending`.

### Order items - `/api/orderitems`

| Method and route              | Behavior                                                                       |
| ----------------------------- | ------------------------------------------------------------------------------ |
| `GET /api/orderitems`         | Get a paged order item list. Supports `pageNumber`, `pageSize`, and `orderId`. |
| `POST /api/orderitems`        | Attach an existing product to an existing order with a quantity.               |
| `PUT /api/orderitems/{id}`    | Change an order item's quantity.                                               |
| `DELETE /api/orderitems/{id}` | Remove an item from an order.                                                  |

An order can contain a given product only once. Items can only be added, changed, or removed while the order is `Pending`; unavailable products cannot be added. The API snapshots the product name and price on the item and recalculates the order total when quantities change.

Example: `GET /api/orderitems?pageNumber=1&pageSize=10&orderId={orderId}`.

### Pagination, validation, and errors

All collection endpoints are paged. Defaults are `pageNumber=1` and `pageSize=10`; page size must be between 1 and 100. Paged responses include `items`, `pageNumber`, `pageSize`, `totalCount`, and `totalPages`.

Request DTO validation and application-level checks protect writes. The API verifies referenced entities exist and enforces rules such as unique customer emails, valid order transitions, and the one-product-per-order constraint. Errors are returned as Problem Details: validation errors use HTTP 400, missing entities use 404, and business conflicts use 409.
