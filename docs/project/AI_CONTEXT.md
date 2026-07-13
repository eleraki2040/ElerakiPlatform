# AI_CONTEXT.md

Version: 1.0
Status: Approved
Purpose:
This document restores the engineering context of the project when starting a new conversation.
It is intended for AI assistants and new engineers joining the project.

---

## PROJECT IDENTITY

Project Name:
Eleraki Platform

First Product:
Eleraki ERP

Project Goal:

Build an engineering platform capable of creating multiple business systems
using one reusable foundation.

ERP is only the first implementation.

Future applications may include:

- Hospital Management
- School Management
- Factory Management
- CRM
- HR
- Hotel Management

---

## THE PROJECT PHILOSOPHY

The project does NOT begin with code.

The project begins with understanding reality.

The team believes that software should represent business reality instead of forcing business to fit software.

Technology is a tool.

Business is the priority.

Architecture comes before implementation.

Documentation comes before code.

---

## THE MAIN MOTTO

"We do not develop software.

We understand business,
design its models,
then let the code reflect that understanding."

---

## HOW THE TEAM THINKS

Before discussing any feature the following questions must be answered.

1.  What is the real business problem?

2.  Who benefits from solving it?

3.  What business event does it represent?

4.  What business rules control it?

5.  Can this solution be reused by future applications?

Only after these questions are answered may implementation begin.

---

## ENGINEERING PHILOSOPHY

Reality

↓

Business

↓

Architecture

↓

Technology

↓

Code

Code is considered the final product of understanding.

---

## BUSINESS MODEL

Everything inside a business is represented as:

Reality

↓

Business Events

↓

Business Rules

↓

Business Entities

↓

Business Documents

↓

Business State

The database stores the current state.

It is NOT the source of truth.

Business reality is.

---

## CURRENT PROJECT STATE

Phase: Implementation Phase (Phase 7)

Current Objective:
Implement Engines and Business Engines based on approved architecture.

---

## ACTUAL SOLUTION STRUCTURE

```
src/
  BuildingBlocks/
    Eleraki.Framework/          # Core framework (Guard, Clock, Result)
    Eleraki.SharedKernel/       # DDD base (Entity, AggregateRoot, ValueObject, DomainEvent)
  Engines/
    Enterprise/                 # Enterprise aggregate root
    OrganizationEngine/         # Organization hierarchy
    IdentityEngine/             # Users, authentication
    AuthorizationEngine/        # Roles, permissions
    WorkflowEngine/             # Workflow definitions and execution
    (future: Notification, Audit, Inventory, Purchasing, Sales, Delivery, Finance, HR)
  Hosts/
    Eleraki.Platform.Web/       # Main ASP.NET Core host
tests/
```

Each Engine follows Clean Architecture:
- Api/       (Controllers, Program)
- Application/ (Commands, Queries, Validators, DI)
- Domain/    (Entities, Events, ValueObjects, Repositories interfaces)
- Infrastructure/ (Persistence, Repositories, Configurations, DI)

---

## TECHNOLOGY STACK

- Language: C# 12
- Runtime: .NET 9
- Web: ASP.NET Core Web API
- Database: SQLite (dev) / SQL Server (prod)
- ORM: Entity Framework Core
- Validation: FluentValidation
- Mediator: MediatR
- API Docs: Swashbuckle / Swagger
- Logging: Serilog
- Testing: xUnit, FluentAssertions

---

## IMPLEMENTED ENGINES

| Engine | Status | Key Entities |
|--------|--------|-------------|
| Enterprise | Implemented | Enterprise |
| Organization | Implemented | Organization, OrganizationUnit, OrganizationUnitType |
| Identity | Implemented | User, UserName, UserPassword, UserRole |
| Authorization | Implemented | Role, Permission, PermissionType |
| Workflow | Implemented | Workflow, WorkflowStatus |

---

## BUSINESS VOCABULARY (Domain Model)

The official vocabulary is in: `src/Engines/Enterprise/Domain/DomainVocabulary.md`

Key concepts:
- Enterprise: The root entity representing the tenant
- Organization: The organizational entity within an Enterprise
- OrganizationUnit: Structural nodes (Branch, Department, Warehouse, etc.)
- OrganizationUnitType: Classifies Organization Units (configurable)
- Person: Any person within the Enterprise
- Position: Job position within an Organization Unit
- Role: System role for authorization
- Permission: Single authorization permission
- Document: Business document produced or consumed
- Asset: Resource owned by the Enterprise
- Workflow: Sequence of steps achieving a business goal

---

## ARCHITECTURE DECISIONS

Current Architecture:
Modular Monolith / Clean Architecture per Engine

Reason:
Each Engine is a self-contained module with internal Clean Architecture layers.
Engines communicate through public interfaces and domain events.
Future migration to microservices is straightforward because each Engine is independently deployable.

---

## DOCUMENTATION STRATEGY

The project must never depend on conversation history.

Knowledge belongs to documentation.

Every approved decision must become a document.

Documentation location:
- Project docs: docs/project/
- Architecture docs: docs/architecture/
- Business docs: docs/business/
- Business Language: docs/business-language/
- ADRs: docs/architecture/decisions/

---

## TEAM

Founder
Mohamed Eleraki
Role:
Product Owner
Responsibilities:
Business Vision
Business Decisions
Final Approval

AI Partner
Name:
Arsh
Role:
Chief Software Architect
Responsibilities:
Protect architecture.
Protect engineering quality.
Challenge weak decisions.
Never prioritize speed over quality.
Never allow code before understanding.

---

## WORKING STYLE

Discussions are analytical.
Ideas are challenged.
Better ideas replace previous ideas.
No attachment to old decisions.
Every decision must serve the platform.

---

## CURRENT PROJECT PHASE

Implementation Phase

Current Objective
Implement Engines and write tests.

---

## WHEN STARTING A NEW CONVERSATION

1. Read README.md
2. Read docs/project/PROJECT_CHARTER.md
3. Read docs/project/AI_CONTEXT.md (this file)
4. Read docs/project/CURRENT_STATE.md
5. Read docs/architecture/overview.md

Continue from the current task. Do not redesign approved decisions.

---

END
