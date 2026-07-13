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

### Business Engines
- Inventory — Warehouse management, stock movements
- Purchasing — Vendors, purchase orders
- Sales — Customers, sales orders
- Delivery — Tracking, driver assignment, delivery execution
- Finance — Accounts, financial transactions
- HR — Departments, employees
- School Management — Students, teachers, classes, courses
- Hospital — Patients, appointments, admissions, invoices

### Integration Engines (Planned)
- Notification
- Audit
- Configuration
- Reporting
- Analytics

---

## Current Status

**Implementation Phase**

Foundation Engines and Business Engines are implemented with tests.
Current test status: **266+ tests passing** across Domain, Application, Infrastructure, and Integration test projects.

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
      Eleraki.Framework/        # Core primitives (Guard, Clock, Result, Error)
      Eleraki.SharedKernel/     # DDD base classes (Entity, AggregateRoot, ValueObject)
    Engines/
      Enterprise/               # Enterprise aggregate root
      OrganizationEngine/       # Organization hierarchy
      IdentityEngine/           # Users and authentication
      AuthorizationEngine/      # Roles and permissions
      WorkflowEngine/           # Workflow orchestration
      SalesEngine/              # Sales orders, customers
      DeliveryEngine/           # Deliveries, drivers, vehicles
      PurchasingEngine/         # Purchase orders, vendors
      InventoryEngine/          # Warehouses, stock movements
      FinanceEngine/            # Accounts, financial operations
      HREngine/                 # Departments, employees
      SchoolManagementEngine/   # Students, teachers, classes, courses
      HospitalEngine/           # Patients, appointments, admissions
    Hosts/
      Eleraki.Platform.Web/     # Main web host
  samples/               # Sample applications (ERP, CRM, Hospital, School, Factory)
  packages/              # NuGet packages
  scripts/               # Build and deployment scripts
  tests/                 # Test projects
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
