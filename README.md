# BlazorBlog

> A modern blog platform built with Blazor and Clean Architecture

---

## About

BlazorBlog is a clean and scalable blogging system designed using modern .NET architectural patterns.

The goal of this project is to build a real-world, production-style blog while keeping the codebase simple, maintainable, and extensible.

This project focuses on:

- Clean Architecture principles
- Feature-based design (CQRS)
- Incremental, real-world development approach

---

## Architecture

The solution follows a **Hybrid Clean Architecture + Feature-based CQRS approach**:

- **Domain**
  - Core business logic
  - Entities (Article, Comment, User)
- **Application**
  - Features (CQRS style)
  - Interfaces & DTOs
- **Infrastructure**
  - Persistence (EF Core)
  - External integrations
- **Web**
  - Blazor Server UI
  - API Endpoints

---

##  Tech Stack

- .NET
- Blazor Server
- ASP.NET Core
- Entity Framework Core
- Clean Architecture
- CQRS Pattern

---

##  Getting Started

### Prerequisites

- .NET SDK

### Run locally

```bash
dotnet restore
dotnet build
dotnet run --project Blog.Web
```

---

##  Project Structure

```text
Blog.Domain
Blog.Application
Blog.Infrastructure
Blog.Web
Blog.Tests
```

---

##  Design Principles

- Separation of concerns
- Feature-based structure
- Simplicity over over-engineering
- Scalable architecture

---

##  Author

Fatemeh Ghasemi
