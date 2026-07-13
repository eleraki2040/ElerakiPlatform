# Framework Overview

Version: 1.0
Status: Approved
Last Updated: 2026-07-12

---

## PURPOSE

This document provides an overview of the Eleraki Framework, the reusable engineering foundation that powers all Eleraki Platform applications.

---

## SCOPE

The Eleraki Framework provides:
- Core domain abstractions (Entity, ValueObject, AggregateRoot, Result)
- Architectural patterns (CQRS, Repository, Domain Events)
- Infrastructure components (database, messaging, caching)
- Cross-cutting concerns (validation, authorization, logging)
- Testing utilities and base classes

The framework does NOT provide:
- Business-specific logic for any vertical (ERP, Hospital, etc.)
- User interface components
- Third-party integrations specific to a domain

---

## DESIGN GOALS

1. **Consistency** - All applications share common patterns and abstractions
2. **Productivity** - Reduce boilerplate code across projects
3. **Maintainability** - Centralized improvements benefit all consumers
4. **Extensibility** - Easy to add new features without modifying core
5. **Testability** - First-class testing support built-in

---

## FRAMEWORK LAYERS

```
Eleraki.Framework
├── Core (abstractions and primitives)
├── Domain (DDD building blocks)
├── Application (use cases, CQRS, validation)
├── Infrastructure (data access, messaging, integrations)
├── Common (utilities, extensions, helpers)
└── Testing (test base classes, utilities, fakes)
```

---

## USAGE

Applications reference the framework as a project or NuGet package:

```csharp
// Reference framework modules as needed
using Eleraki.Framework.Domain;
using Eleraki.Framework.Application;
using Eleraki.Framework.Infrastructure;
```

---

## RELATED DOCUMENTS

- architecture/overview.md
- framework/architecture.md
- framework/patterns.md
- framework/abstractions.md

---

END OF DOCUMENT
