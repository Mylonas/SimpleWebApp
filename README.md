# SimpleWebApp — Products API

A simple backend REST API built with ASP.NET Core 8, demonstrating CRUD operations, price range filtering, and validation using a `Product` entity and an in-memory EF Core database.

[![CI](https://github.com/Mylonas/SimpleWebApp/actions/workflows/ci.yml/badge.svg)](https://github.com/Mylonas/SimpleWebApp/actions/workflows/ci.yml)

## Technologies

- **ASP.NET Core 8** — web API framework
- **Entity Framework Core (InMemory)** — data access, in-memory database for simplicity
- **Swagger / Scalar** — interactive API docs at `/swagger` in development
- **xUnit** — unit tests

## Setup

### Prerequisites

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)

### Run

```bash
git clone https://github.com/Mylonas/SimpleWebApp.git
cd SimpleWebApp
dotnet restore
dotnet run --project TodoApi
```

The API starts at `https://localhost:7XXX`. Open `/swagger` in a browser to explore endpoints interactively.

### Test

```bash
dotnet test
```

## API Endpoints

### CRUD

| Method | Endpoint            | Description              |
|--------|---------------------|--------------------------|
| GET    | `/api/products`     | List all products        |
| GET    | `/api/products/{id}`| Get a product by ID      |
| POST   | `/api/products`     | Create a product         |
| PUT    | `/api/products/{id}`| Update a product         |
| DELETE | `/api/products/{id}`| Delete a product         |

### Filtering

| Method | Endpoint                                      | Description                          |
|--------|-----------------------------------------------|--------------------------------------|
| GET    | `/api/products/ByPriceRange?min=10&max=100`   | List products within a price range   |

## Product Schema

```json
{
  "id": 1,
  "name": "Apple",
  "price": 1.50,
  "category": "Fruit"
}
```

## Error Handling

| Status | Meaning                                          |
|--------|--------------------------------------------------|
| 400    | Invalid input (id mismatch, bad price range)     |
| 404    | Product not found                                |
