# BlazorBlog (Backend API Only)

## About

BlazorBlog is a modern backend blog system built with:

- Clean Architecture
- CQRS pattern
- ASP.NET Core Minimal APIs
- Entity Framework Core (SQL Server)
- Swagger for API documentation

This project is designed as a production-style backend service with no frontend/UI layer.

## Architecture

The solution follows Clean Architecture with feature-based CQRS:

- `Blog.Domain`
  - Core business rules
  - Entities and value objects (such as `Slug`)
- `Blog.Application`
  - Use cases and CQRS handlers
  - DTOs and abstraction contracts
- `Blog.Infrastructure`
  - EF Core persistence and SQL Server integration
  - Implementation of application abstractions
- `Blog.Web` (API Host only)
  - HTTP API layer (Minimal APIs)
  - Swagger/OpenAPI exposure

There is no UI layer in this system. The web project acts as the API gateway only.

## Features

- Article management (Create / Read)
- Clean CQRS implementation
- SQL Server persistence
- Swagger API documentation
- Value Objects (`Slug`)

## Tech Stack

- .NET
- ASP.NET Core Minimal APIs
- Entity Framework Core
- SQL Server
- Swagger
- Clean Architecture

## API Overview

- `POST /api/articles`
  - Creates a new article
- `GET /api/articles`
  - Returns the list of persisted articles

Swagger UI is the primary interface for exploring and testing endpoints.

## Project Structure

```text
src/
  Blog.Domain
  Blog.Application
  Blog.Infrastructure
  Blog.Web
tests/
  Blog.Tests
```

Current repository layout keeps these projects at the solution root with the same boundaries.

## Design Principles

- Separation of concerns
- Feature-based architecture
- No UI dependency
- Backend-first design
- Scalability and maintainability

## Roadmap

- Article system (completed foundation)
- Comment system
- Authentication (JWT)
- Email notifications (RabbitMQ integration)
- Search (Elasticsearch)
- Event-driven architecture

## How to Run

```bash
dotnet restore
dotnet build
dotnet run --project Blog.Web
```

Then open Swagger UI:

- `http://localhost:<port>/swagger`

## License

MIT

## Author

Fatemeh Ghasemi
