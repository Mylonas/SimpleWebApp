# Product

This is a simple backend API built with ASP.NET Core, demonstrating CRUD operations, SQL data querying, data aggregation, and validation using a `Product` entity.

## Table of Contents

- [Project Overview](#project-overview)
- [Technologies Used](#technologies-used)
- [Setup Instructions](#setup-instructions)
- [API Endpoints](#api-endpoints)
- [Assumptions](#assumptions)
- [Running Tests](#running-tests)

## Project Overview

This API is designed to manage a `Product` entity in a local SQLite database. The project includes CRUD operations, filtering based on price range and validation.

## Technologies Used

- **ASP.NET Core** - for building the API
- **SQLite** - as the database
- **Entity Framework Core** - for database interactions

## Setup Instructions

### Prerequisites

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- (Optional) A tool to interact with SQLite databases, such as [DB Browser for SQLite](https://sqlitebrowser.org/)

### Installation Steps

1. Clone this repository:
    ```bash
    git clone https://github.com/Mylonas/SimpleWebApp.git
    ```

2. Install dependencies:
    ```bash
    dotnet restore
    ```

3. Set up the SQLite database by running migrations:
    ```bash
    dotnet add package Microsoft.Data.Sqlite
    ```

4. Run the application:
    ```bash
    dotnet run
    ```

## API Endpoints

### CRUD Endpoints

| Method | Endpoint               | Description                     |
| ------ | ----------------------- | ------------------------------- |
| POST   | `/api/products`         | Create a new product            |
| GET    | `/api/products`         | Retrieve all products           |
| GET    | `/api/products/{id}`    | Retrieve a product by ID        |
| PUT    | `/api/products/{id}`    | Update an existing product      |
| DELETE | `/api/products/{id}`    | Delete a product by ID          |

### Data Querying Endpoints

| Method | Endpoint                        | Description                                        |
| ------ | --------------------------------| -------------------------------------------------- |
| GET    | `/api/products/ByPriceRange`    | Retrieve products within a specified price range   |

### Data Validation

The API includes validation to ensure that:
- `id` is required for each product.

## Error Handling

The API includes error handling for:
- **Not found errors**: Returns `404 Not Found` if a product by ID or search parameter isn’t available.
