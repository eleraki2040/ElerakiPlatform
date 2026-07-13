# Framework Architecture

Version: 1.0
Status: Approved
Last Updated: 2026-07-12

---

## PURPOSE

This document describes the internal architecture of the Eleraki Framework, including its layers, components, and their responsibilities.

---

## LAYERED ARCHITECTURE

The framework follows a strict layered architecture with dependency rules enforced from the outside in.

```
┌─────────────────────────────────────────┐
│           Testing Layer                 │
├─────────────────────────────────────────┤
│         Infrastructure Layer            │
├─────────────────────────────────────────┤
│         Application Layer               │
├─────────────────────────────────────────┤
│           Domain Layer                  │
├─────────────────────────────────────────┤
│            Core Layer                   │
└─────────────────────────────────────────┘
```

### Dependency Direction

- Outer layers may depend on inner layers
- Inner layers must NOT depend on outer layers
- All dependencies point inward

---

## CORE LAYER

The foundation of the framework. Contains no external dependencies.

**Responsibilities:**

- Primitive types and base abstractions
- Common interfaces (IRepository, IUnitOfWork, IEventBus)
- Result type and functional primitives
- Guard clauses and validation utilities
- Extensions and helper methods

**Key Components:**

- `Result<T>` - Operation outcome wrapper
- `Guard` - Argument validation utilities
- `IAggregateRoot` - Aggregate marker interface
- `IDomainEvent` - Domain event marker interface

---

## DOMAIN LAYER

Contains all DDD building blocks. Depends only on Core.

**Responsibilities:**

- Entity and ValueObject abstractions
- AggregateRoot implementation
- Domain event definitions and dispatching
- Specification pattern implementation
- Domain exception types

**Key Components:**

- `Entity<T>` - Base entity implementation
- `ValueObject` - Base value object with equality semantics
- `AggregateRoot` - Entity with event sourcing capabilities
- `DomainEvent` - Base domain event
- `Specification<T>` - Specification pattern

---

## APPLICATION LAYER

Orchestrates domain logic without containing business rules.

**Responsibilities:**

- Command and Query definitions
- Handlers (use case implementations)
- Validation pipelines
- Authorization policies
- Event subscriptions

**Key Components:**

- `ICommand<TResponse>` / `IQuery<TResponse>`
- `CommandHandler<TCommand, TResponse>`
- `QueryHandler<TQuery, TResult>`
- `IValidator<T>` - FluentValidation integration
- `PipelineBehavior<T>` - Cross-cutting concerns

---

## INFRASTRUCTURE LAYER

Implements interfaces defined in inner layers.

**Responsibilities:**

- Database access (EF Core, Dapper)
- Messaging (in-process, out-of-process)
- Caching (Redis, in-memory)
- File storage and email
- Third-party service clients

**Key Components:**

- `EfRepository<T>` - EF Core repository implementation
- `InMemoryEventBus` - In-process event bus
- `RedisCacheProvider` - Redis caching
- `EmailSender` - Email infrastructure

---

## TESTING LAYER

Provides utilities for writing tests against framework-based applications.

**Responsibilities:**

- Test base classes and fixtures
- In-memory database setup
- Fake/mock implementations
- Test data builders
- Assertion helpers

---

## MODULE ORGANIZATION

Each layer is organized as a separate project/namespace:

```
src/
├── Eleraki.Framework.Core/
├── Eleraki.Framework.Domain/
├── Eleraki.Framework.Application/
├── Eleraki.Framework.Infrastructure/
├── Eleraki.Framework.Common/
└── Eleraki.Framework.Testing/
```

---

## RELATED DOCUMENTS

- framework/overview.md
- framework/patterns.md
- framework/abstractions.md

---

END OF DOCUMENT
