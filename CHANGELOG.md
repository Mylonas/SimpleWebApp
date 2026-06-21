# Changelog

All notable changes to this project will be documented in this file.

The format follows [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).
This project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## [1.1.0] - 2026-06-21

### Added
- **Unit tests** (`TodoApi.Tests/`) using xUnit — 9 tests covering all controller actions including happy paths, not-found cases, bad-request cases, and price range filtering
- **CI workflow** (`.github/workflows/ci.yml`) — builds and runs tests on every push and PR to `master`/`dev` using `actions/setup-dotnet@v4`
- **Dependabot** (`.github/dependabot.yml`) — weekly automated updates for NuGet packages and GitHub Actions

### Fixed
- **`GetByPriceRange`** — was completely broken: had a spurious `id` parameter, the price filter was commented out, and the not-found condition was inverted (returned 404 when a product *did* exist). Now correctly filters `Products` by `Price >= min && Price <= max` and returns 404 only when no results are found
- **`GetProductById`** — removed duplicate method that replicated `GetProduct` with a broken route (`[HttpGet("id")]` instead of `[HttpGet("{id}")]`)
- **`Product` model** — renamed properties from lowercase (`id`, `name`, `price`, `category`) to PascalCase (`Id`, `Name`, `Price`, `Category`) per C# conventions; JSON serialization is unaffected (ASP.NET Core uses camelCase by default)
- **`PutProduct`** error message — now returns a descriptive string on 400/404 instead of an empty body
- **`PostProduct`** — removed stale commented-out `CreatedAtAction` call

### Changed
- **`TodoApi.csproj`** — removed stale `NewFolder1` exclusion blocks; removed `Microsoft.Data.Sqlite` (unused — app uses InMemory); removed `Microsoft.VisualStudio.Web.CodeGeneration.Design` (scaffolding tool, not needed at runtime)
- **`ProductsController.cs`** — consolidated redundant `using` statements; converted namespace declaration to file-scoped; simplified `ProductExists` to expression body
- **`README.md`** — fixed incorrect setup step (`dotnet add package` is not a setup step); updated technology list (SQLite → InMemory); added test instructions and CI badge; added product schema and error handling table

---

## [1.0.0] - 2024-11-07

### Added
- Initial ASP.NET Core 8 Web API with `Product` entity
- CRUD endpoints: GET (all), GET (by id), POST, PUT, DELETE
- `GetByPriceRange` query endpoint
- EF Core InMemory database via `TodoContext`
- Swagger UI in development
