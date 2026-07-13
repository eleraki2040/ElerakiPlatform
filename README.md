# Eleraki Platform

> **Understanding Reality. Engineering Business.**

---

## Overview

Eleraki Platform is an engineering platform for building modern business applications using Clean Architecture and Domain-Driven Design.

It is **not just an ERP system**.

ERP is the first application that will be built on top of the platform.

The long-term vision is to create a reusable foundation capable of supporting different business systems such as:

- ERP
- Hospital Management
- School Management
- Factory Management
- CRM
- HR
- Hotel Management
- Other future business applications

---

## Core Philosophy

Before writing code...

We understand reality.

Before designing software...

We understand business.

Before choosing technology...

We understand the problem.

Our software is not built around screens or database tables.

It is built around real business behavior.

---

## Engineering Principles

- Reality First
- Business Before Technology
- Behavior Before Data
- Simplicity Before Complexity
- Documentation Before Implementation
- Architecture Before Coding
- Code Is the Result of Understanding

---

## Technology Stack

| Component | Technology |
|-----------|-----------|
| Language | C# 12 |
| Runtime | .NET 9 |
| Framework | ASP.NET Core Web API |
| Database | SQLite (dev) / SQL Server (prod) |
| ORM | Entity Framework Core |
| Validation | FluentValidation |
| Mediator | MediatR |
| API Docs | Swashbuckle / Swagger |
| Logging | Serilog |
| Testing | xUnit, FluentAssertions |

---

## Project Structure

```
ElerakiPlatform/
  docs/
    architecture/        # Architecture docs, ADRs, diagrams
    project/             # Project identity, principles, roadmap
    business/            # Business discovery and analysis
    business-language/   # Official business vocabulary
    development/         # Coding standards, testing, release
  src/
    BuildingBlocks/
      Eleraki.Framework/        # Core primitives (Guard, Clock, Result)
      Eleraki.SharedKernel/     # DDD base classes (Entity, AggregateRoot, ValueObject)
    Engines/
      Enterprise/               # Enterprise aggregate root
      OrganizationEngine/       # Organization hierarchy
      IdentityEngine/           # Users and authentication
      AuthorizationEngine/      # Roles and permissions
      WorkflowEngine/           # Workflow orchestration
      (future engines...)
    Hosts/
      Eleraki.Platform.Web/     # Main web host
  samples/               # Sample applications (ERP, CRM, Hospital, School, Factory)
  packages/              # NuGet packages
  scripts/               # Build and deployment scripts
  tests/                 # Test projects
```

---

## Architecture Style

**Modular Monolith with Clean Architecture per Engine**

Each Engine follows Clean Architecture:
- **Domain** — Entities, Events, ValueObjects, Repository interfaces
- **Application** — Commands, Queries, Validators, DI registration
- **Infrastructure** — Persistence, Repositories, Configurations, DI registration
- **API** — Controllers, Program, Middleware

Engines communicate through:
- Public interfaces
- Domain events

---

## Current Status

**Implementation Phase**

The engineering foundation is complete. Business Discovery and Business Analysis are in progress.
Implementation of Foundation Engines and Business Engines is underway.

---

## Engines

### Foundation Engines
- Enterprise — Root aggregate, tenant management
- Organization — Organizational hierarchy and units
- Identity — Users, authentication
- Authorization — Roles, permissions
- Workflow — Workflow definitions and execution

### Business Engines (Planned)
- Inventory
- Purchasing
- Sales
- Delivery
- Finance
- HR

### Integration Engines (Planned)
- Notification
- Audit
- Configuration
- Reporting
- Analytics

---

## Current Phase

Implementation Phase

The team is implementing Foundation Engines and Business Engines based on approved architecture.

---

## Documentation

Project documentation starts here:

```
docs/project/           # Project identity, principles, roadmap
docs/architecture/      # Architecture, ADRs, diagrams
docs/business/          # Business discovery and analysis
docs/business-language/ # Official business vocabulary
```

---

## Motto

> **We do not develop software.**
>
> **We understand business, design its models, then let the code reflect that understanding.**

---

## License

Private Project

Copyright © Mohamed Eleraki
