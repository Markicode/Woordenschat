# Library Management API

A RESTful Web API for managing a library system.
This project is built as a learning-focused backend application using ASP.NET Core and Entity Framework Core, 
with a strong emphasis on clean architecture, separation of concerns, and testability.

The API supports managing books with multiple genres, including hierarchical genres.

---

## Features

- Full CRUD operations for books (GET, POST, PUT, PATCH, DELETE)
- Books can belong to multiple genres (many-to-many)
- Hierarchical genre structure (parent / sub-genres)
- Clean, RESTful API design
- DTO-based input and output models
- Explicit mapping between domain models and DTOs
- Async data access with Entity Framework Core
- Swagger / OpenAPI documentation
- Automated integration tests

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

```
Library.sln
├── Application
│   ├── Common
│   ├── Dtos
│   ├── Enums
│   ├── Mappings
│   └── Services
│
├── Data
│   ├── Configurations
│   ├── Migrations
│   ├── Seed
│   └── LibraryContext.cs
│
├── Domain
│   ├── Entities
│   ├── Enums
│   └── Interfaces
│
├── WebApi
│   ├── Controllers
│   ├── Properties
│   ├── Json
│   ├── Program.cs
│   └── appsettings.json
│
└── WebApi.Tests
    ├── Controllers
    └── ApiFactory.cs
```

---

## Architectural Overview

This project follows a layered architecture inspired by **Clean Architecture** principles.

### Layers

#### Domain
- Contains core business entities, enums, and interfaces
- Has no dependencies on other layers
- Represents the core business model

#### Application
- Contains application services (use cases)
- Defines DTOs used for input and output
- Contains explicit mapping logic between domain entities and DTOs
- Orchestrates business operations without knowledge of HTTP or persistence details

#### Data
- Handles persistence concerns
- Contains the EF Core `DbContext`, entity configurations, migrations, and seed data
- Implements repository and data access logic

#### WebApi
- Acts as the delivery layer
- Exposes HTTP endpoints via controllers
- Handles request/response concerns only
- Delegates all business logic to the Application layer

### DTO Usage

DTOs are used to:
- Decouple internal domain models from external API contracts
- Prevent overexposing domain entities
- Enable safe refactoring of internal architecture without breaking API behavior

---

## API Overview

| Method | Endpoint            | Description                         |
|--------|---------------------|-------------------------------------|
| GET    | /api/books          | Get all books                       |
| GET    | /api/books/{id}     | Get a book by ID                    |
| POST   | /api/books          | Create a new book                   |
| PUT    | /api/books/{id}     | Replace an existing book            |
| PATCH  | /api/books/{id}     | Partially update an existing book   |
| DELETE | /api/books/{id}     | Update an existing book             |
| GET    | /api/genres         | Get all genres                      |

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

The tests run against the API using a test host (`WebApplicationFactory`),
exercising the full HTTP request pipeline including routing, controllers,
dependency injection, and Entity Framework Core.

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
- Layered / Clean Architecture principles
- Entity Framework Core relationships
- DTO mapping strategies
- Asynchronous programming in ASP.NET Core
- Database migrations and seeding
- Refactoring for separation of concerns
- Writing maintainable and testable code

---

## Future Improvements

- Add user authentication and authorization
- Implement pagination and filtering for book listings
- Improved validation and error handling
- Additional API endpoints and resources
- Frontend applications to consume the API
- CI pipeline
- Expanded integration test coverage (edge cases, error scenarios)
- Unit tests for domain and service logic

---

## License

This project is for educational purposes.