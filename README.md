# Library Management API

A RESTful Web API for managing a library system.
This project is built as a learning-focused backend application using ASP.NET Core and Entity Framework Core.

The API supports managing books with multiple genres, including hierarchical genres, and follows clean API design principles such as DTO usage and separation of concerns.

---

## Features

- CRUD operations for books
- Books can belong to multiple genres (many-to-many)
- Hierarchical genre structure (parent / sub-genres)
- RESTful API design
- DTO-based input and output models
- Async data access with Entity Framework Core
- Swagger / OpenAPI documentation

---

## Tech Stack

- **.NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **MySQL / MariaDB**
- **Swagger (OpenAPI)**
- **xUnit (integration testing with ASP.NET WebApplicationFactory)** 

---

## Project Structure

```Library.sln
├── Domain
│ └── Entities (Book, Genre, User, etc.)
├── Data
│ ├── DbContext
│ ├── Entity Configurations
│ ├── Migrations
│ └── Seed Data
├── WebApi
│ ├── Controllers
│ ├── DTOs
│ └── API configuration
└── WebApi.Tests
    ├── Controllers
    │   ├── BooksControllerTests
    │   └── GenresControllerTests
    └── ApiFactory (test host configuration)
```

---

### Architectural Notes
- **Domain** contains core business entities
- **Data** handles persistence and EF Core configuration
- **WebApi** exposes HTTP endpoints and DTOs
- DTOs are used to separate internal models from API contracts

---

## API Overview

| Method | Endpoint            | Description                |
|--------|---------------------|----------------------------|
| GET    | /api/books          | Get all books              |
| GET    | /api/books/{id}     | Get a book by ID           |
| POST   | /api/books          | Create a new book          |
| GET    | /api/genres         | Get all genres             |

Full API documentation is available via **Swagger** when running the project.

---

## Getting Started

### Prerequisites

- .NET 8 SDK
- MySQL or MariaDB
- Visual Studio or VS Code

### Setup

1. Clone the repository
2. Configure the database connection in `appsettings.json`
3. Apply database migrations
4. Run the API

```bash
dotnet ef database update
dotnet run
```

Swagger will be available at:

```url
https://localhost:<port>/swagger
```

---

## Testing 

This project includes automated integration tests using **xUnit**.

The tests run against the API using an in-memory test server
(`WebApplicationFactory`), exercising the full HTTP request pipeline
including routing, controllers, dependency injection, and Entity Framework Core.

Current test coverage includes:

- Happy-path API behavior for books and genres
- End-to-end HTTP request/response validation
- Database interaction through EF Core
- Verification of RESTful status codes

Tests are executed using:

```bash 
dotnet test
```

The testing setup is intentionally focused on integration tests to validate
realistic API behavior. Unit tests and additional edge-case coverage are
planned as future improvements.

---

## Learning Goals

This project was created to practice and demonstrate:

- Clean API design 
- Entity Framework Core relationships
- DTO mapping strategies
- Asynchronous programming in ASP.NET Core
- Database migrations and seeding
- Writing maintainable and testable code

---

## Future Improvements

- Add user authentication and authorization
- Implement pagination and filtering for book listings
- Improved validation and error handling
- Frontend applications to consume the API
- CI pipeline
- Expanded integration test coverage (edge cases, error scenarios)
- Unit tests for domain and service logic

---

## License

This project is for educational purposes.