# Library Management System

A full-stack Library Management System built as a learning-focused project demonstrating modern backend architecture, integration testing, and a typed frontend consuming a REST API.

The backend is built using ASP.NET Core Web API with Clean Architecture principles, Entity Framework Core, and MySQL, while the frontend is implemented using React + TypeScript.

---

## Features

### Backend

- Full CRUD operations for books (GET, POST, PUT, PATCH, DELETE)
- Author resource with read and create endpoints
- Books can belong to multiple genres (many-to-many)
- Hierarchical genre structure (parent / sub-genres)
- Clean, RESTful API design
- DTO-based input and output models
- Explicit mapping between domain models and DTOs
- Explicit Result-based error handling (no exception-driven flow)
- Centralized controller error handling via a shared base controller
- Async data access with Entity Framework Core
- Swagger / OpenAPI documentation
- Automated integration tests with database reset per test run

### Frontend

- React + TypeScript frontend consuming the REST API
- Typed service layer for API communication
- Component-based UI architecture
- Vite development environment

---

## Tech Stack

### Backend

- **.NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **MySQL / MariaDB**
- **Swagger (OpenAPI)**
- **xUnit integration testing**
- **WebApplicationFactory test host**
- **Respawn database reset**

### Frontend

- **React**
- **Typescript**
- **Vite**
- **Fetch-based API client**

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
│   ├── Extensions
│   ├── Json
│   ├── Properties
│   ├── Program.cs
│   └── appsettings.json
│
├── WebApi.Tests
│   ├── Controllers
│   ├── Helpers
│   ├── ApiFactory.cs
│   └── Integration test setup
│
└── Frontend (React)
    ├── src
    │   ├── components
    │   ├── pages
    │   ├── services
    │   └── models
    ├── package.json
    └── vite.config.ts
```

---

## Architectural Overview

This project follows a layered architecture inspired by **Clean Architecture** principles.

### Layers

#### Domain

- Core business entities
- Business rules and interfaces
- No dependencies on other layers

#### Application

- Use-case driven application services
- DTO definitions
- Mapping logic between entities and DTOs
- Business workflow orchestration

#### Data

- Persistence logic
- EF Core DbContext
- Entity configurations
- Database migrations
- Seed data

#### WebApi

- HTTP controllers
- Request / response handling
- Delegation to application services
- Centralized error mapping

#### Frontend

- React UI consuming REST endpoints
- Typed service layer communicating with API
- Component-based interface

### Controllers

Controllers are intentionally kept thin and inherit from a shared `BaseApiController`.

Responsibilities:

- Translate HTTP requests into application commands
- Delegate all business logic to application services
- Convert application results into HTTP responses

All controllers follow a consistent pattern:

- Call application service
- A shared BaseApiController is used to centralize error-to-HTTP mapping and keep controllers consistent and minimal
- Map successful results to DTOs

### DTO Usage

DTOs are used to:

- Decouple internal domain models from external API contracts
- Prevent overexposing domain entities
- Enable safe refactoring of internal architecture without breaking API behavior

### Error Handling Strategy

The application uses an explicit `Result` / `Result<T>` pattern to handle success and failure cases.

- Application services return `Result<T>` instead of throwing exceptions for expected error scenarios
- Errors are categorized using an `ErrorType` enum (e.g. NotFound, ValidationError, Conflict)
- HTTP error mapping is handled centrally in the WebApi layer
- Controllers remain thin and free of duplicated error-handling logic

This approach makes control flow explicit, improves testability, and avoids exception-driven business logic.

---

## API Overview

| Method | Endpoint          | Description                           |
| ------ | ----------------- | ------------------------------------- |
| GET    | /api/books        | Get all books                         |
| GET    | /api/books/{id}   | Get a book by ID                      |
| POST   | /api/books        | Create a new book                     |
| PUT    | /api/books/{id}   | Replace an existing book              |
| PATCH  | /api/books/{id}   | Partially update an existing book     |
| DELETE | /api/books/{id}   | Delete an existing book               |
| GET    | /api/genres       | Get all genres                        |
| GET    | /api/genres/{id}  | Get a genre by ID                     |
| GET    | /api/authors      | Get all authors (without books)       |
| GET    | /api/authors/{id} | Get an author by ID (including books) |
| POST   | /api/authors      | Create a new author                   |

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

- Happy-path API behavior for books, authors, and genres
- Validation and not-found scenarios
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
- Advanced frontend features
- CI pipeline
- Expanded integration test coverage (edge cases, error scenarios)
- Unit tests for domain and service logic

---

## License

This project is for educational purposes.
