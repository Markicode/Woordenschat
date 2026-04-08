# Library Management System (Woordenschat)

A full-stack Library Management System built as a learning-focused project demonstrating modern backend architecture, Domain-Driven Design concepts, integration testing, and a typed frontend consuming a REST API.

The backend is built using **ASP.NET Core Web API** with **Clean Architecture**, **Entity Framework Core**, and **MySQL**, while the frontend is implemented using **React + TypeScript**.

## Features

### Backend

- Full CRUD operations for books (GET, POST, PUT, PATCH, DELETE)
- Partial updates using a custom PATCH implementation with Optional value semantics
- Author resource with read and create endpoints
- Books can belong to multiple genres (many-to-many)
- Hierarchical genre structure (parent / sub-genres)
- Domain-driven entity design with encapsulation and private setters
- Value Objects (e.g. ISBN) with EF Core value converters
- Explicit Result-based error handling (no exception-driven flow)
- Lifecycle status tracking with audit timestamps
- DTO-based input and output models
- Explicit mapping between domain models and DTOs
- Centralized controller error handling via a shared base controller
- Async data access with Entity Framework Core
- Swagger / OpenAPI documentation
- Automated integration tests with database reset per test run

### Frontend

- React + TypeScript frontend consuming the REST API
- Typed service layer for API communication
- Component-based UI architecture
- Vite development environment

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

## Project Structure

Plain textANTLR4BashCC#CSSCoffeeScriptCMakeDartDjangoDockerEJSErlangGitGoGraphQLGroovyHTMLJavaJavaScriptJSONJSXKotlinLaTeXLessLuaMakefileMarkdownMATLABMarkupObjective-CPerlPHPPowerShell.propertiesProtocol BuffersPythonRRubySass (Sass)Sass (Scss)SchemeSQLShellSwiftSVGTSXTypeScriptWebAssemblyYAMLXML`   Library.sln  ├── Application  │   ├── Common (Result pattern, Optional, helpers)  │   ├── Dtos  │   ├── Enums  │   ├── Mappings  │   ├── Services  │   └── Commands  │  ├── Data  │   ├── Configurations  │   ├── Migrations  │   ├── Seed  │   └── LibraryContext.cs  │  ├── Domain  │   ├── Entities  │   ├── Enums  │   ├── ValueObjects  │   ├── Validation  │   └── Interfaces  │  ├── WebApi  │   ├── Controllers  │   ├── Extensions  │   ├── Helpers  │   ├── Json  │   └── Program.cs  │  ├── WebApi.Tests  │   ├── Controllers  │   ├── Helpers  │   └── Integration setup  │  └── Frontend (React)   `

## Architectural Overview

This project follows a layered architecture inspired by **Clean Architecture** and introduces selected **DDD patterns**.

### Domain

- Core business entities
- Value Objects (e.g. ISBN)
- Lifecycle state tracking
- Encapsulation via private setters and domain methods
- Business invariants and validation

### Application

- Use-case driven application services
- DTO definitions
- Mapping logic between entities and DTOs
- Business workflow orchestration
- Result pattern for explicit success/failure modeling
- Optional value semantics for PATCH operations

### Data

- Persistence logic
- EF Core DbContext
- Entity configurations
- ValueObject converters
- Database migrations
- Seed data

### WebApi

- HTTP controllers
- Request / response handling
- Delegation to application services
- Centralized error mapping
- PATCH parsing helpers

### Frontend

- React UI consuming REST endpoints
- Typed service layer communicating with API
- Component-based interface

## Error Handling Strategy

The application uses an explicit Result / Result pattern.

- Application services return Result types instead of throwing exceptions for expected scenarios
- Errors are categorized using an ErrorType enum
- HTTP error mapping is handled centrally in the WebApi layer
- Controllers remain thin and free of duplicated error-handling logic

This approach improves:

- testability
- readability
- explicit control flow
- domain clarity

## API Overview

MethodEndpointDescriptionGET/api/booksGet all booksGET/api/books/{id}Get a book by IDPOST/api/booksCreate a new bookPUT/api/books/{id}Replace an existing bookPATCH/api/books/{id}Partially update an existing bookDELETE/api/books/{id}Delete an existing bookGET/api/genresGet all genresGET/api/genres/{id}Get a genre by IDGET/api/authorsGet all authorsGET/api/authors/{id}Get author with booksPOST/api/authorsCreate author

Swagger documentation is available when running the project.

## Getting Started

### Prerequisites

- .NET 8 SDK
- MySQL or MariaDB
- Visual Studio or VS Code

### Setup

1.  Clone the repository
2.  Configure database connection in appsettings.json
3.  Apply database migrations
4.  Run the API

Plain textANTLR4BashCC#CSSCoffeeScriptCMakeDartDjangoDockerEJSErlangGitGoGraphQLGroovyHTMLJavaJavaScriptJSONJSXKotlinLaTeXLessLuaMakefileMarkdownMATLABMarkupObjective-CPerlPHPPowerShell.propertiesProtocol BuffersPythonRRubySass (Sass)Sass (Scss)SchemeSQLShellSwiftSVGTSXTypeScriptWebAssemblyYAMLXML`   dotnet ef database update  dotnet run   `

Swagger will be available at:

Plain textANTLR4BashCC#CSSCoffeeScriptCMakeDartDjangoDockerEJSErlangGitGoGraphQLGroovyHTMLJavaJavaScriptJSONJSXKotlinLaTeXLessLuaMakefileMarkdownMATLABMarkupObjective-CPerlPHPPowerShell.propertiesProtocol BuffersPythonRRubySass (Sass)Sass (Scss)SchemeSQLShellSwiftSVGTSXTypeScriptWebAssemblyYAMLXML`   https://localhost:/swagger   `

## Testing

This project includes automated integration tests using **xUnit**.

Tests run against the API using a test host (WebApplicationFactory) and validate:

- End-to-end HTTP behavior
- RESTful status codes
- Validation scenarios
- Error mapping
- Database interactions
- Result pattern correctness

Run tests with:

Plain textANTLR4BashCC#CSSCoffeeScriptCMakeDartDjangoDockerEJSErlangGitGoGraphQLGroovyHTMLJavaJavaScriptJSONJSXKotlinLaTeXLessLuaMakefileMarkdownMATLABMarkupObjective-CPerlPHPPowerShell.propertiesProtocol BuffersPythonRRubySass (Sass)Sass (Scss)SchemeSQLShellSwiftSVGTSXTypeScriptWebAssemblyYAMLXML`   dotnet test   `

## Learning Goals

This project was created to practice and demonstrate:

- Clean Architecture
- Domain-driven design concepts
- Value Objects and EF Core converters
- PATCH semantics and partial updates
- Explicit error modeling with Result pattern
- Migration discipline and schema evolution
- Integration testing strategies
- API design best practices
- Backend refactoring techniques

## Future Improvements

- Authentication and authorization
- Pagination and filtering
- Domain events
- Background jobs
- Caching
- Advanced validation pipeline
- CQRS / MediatR exploration
- Expanded integration and unit testing
- CI/CD pipeline

## License

This project is for educational purposes.
