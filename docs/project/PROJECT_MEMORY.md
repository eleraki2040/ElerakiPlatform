# PROJECT_MEMORY.md

Version: 1.0
Status: Approved
Purpose:
This document records the approved knowledge, decisions, philosophy, and agreements of the Eleraki Platform project.
It serves as the permanent memory of the project and should be updated whenever a major decision is approved.

---

## PROJECT OVERVIEW

Project Name:
Eleraki Platform

First Product:
Eleraki ERP

Project Goal:

Build an engineering platform capable of creating multiple business systems
using one reusable engineering foundation.

ERP is only the first application.

The platform should later support systems such as:

- ERP
- Hospital Management
- School Management
- Factory Management
- CRM
- HR
- Hotel Management

---

## CURRENT PHASE

Implementation Phase (Phase 7)

The platform foundation is built. Engines are being implemented and tested.

---

## FOUNDING IDEA

The project is not an ERP project.

It is an Engineering Platform.

ERP is simply the first implementation.

The platform must outlive any individual application.

---

## PROJECT MOTTO

"We do not develop software.

We understand business,
design its models,
then let the code reflect that understanding."

---

## THE FUNDAMENTAL BELIEF

Reality is the source of truth.

Software must represent reality.

Business comes before technology.

Technology serves business.

---

## HOW WE THINK

The team never starts from:
Database
Screens
CRUD
Framework
Programming Language

Instead the order is:
Reality
↓
Business
↓
Architecture
↓
Technology
↓
Code

---

## BUSINESS THINKING MODEL

Everything inside the system is viewed as:

Reality
↓
Event
↓
Rule
↓
Entity
↓
Document
↓
State

Explanation:
Reality exists before software.
Events change reality.
Rules control events.
Entities participate in events.
Documents prove events happened.
State represents the latest known condition.

---

## ENGINEERING PRINCIPLES

Reality First
Business Before Technology
Architecture Before Coding
Documentation Before Implementation
Behavior Before Data
Code Is The Result Of Understanding
Simple Before Complex
Maintainability Over Speed

---

## DOCUMENTATION PHILOSOPHY

The project must never depend on conversation history.
The project must never depend on human memory.
Knowledge belongs to documentation.
Every approved decision must become a document.
Documentation is part of the product.

---

## ARCHITECTURE DECISIONS

Current Architecture:
Modular Monolith with Clean Architecture per Engine

Each Engine has:
- Domain (Entities, Events, ValueObjects, Repository interfaces)
- Application (Commands, Queries, Validators, DI)
- Infrastructure (Persistence, Repositories, Configurations, DI)
- API (Controllers, Program, Middleware)

Engines communicate through public interfaces and domain events.

Migration to Microservices will happen only when business needs justify it.

---

## TECHNOLOGY STACK

- Language: C# 12
- Runtime: .NET 9
- Framework: ASP.NET Core Web API
- Database: SQLite (dev) / SQL Server (prod)
- ORM: Entity Framework Core
- Validation: FluentValidation
- Mediator: MediatR
- API Docs: Swashbuckle / Swagger
- Logging: Serilog
- Testing: xUnit, FluentAssertions

---

## APPROVED ADRs

- ADR-001: Organization Engine Owns the Organizational Structure
- ADR-002: Organization Unit Type is an Aggregate Root
- ADR-003: Budget Allocation Strategy (Manual Allocation)
- ADR-004: Documentation-First Development Approach
- ADR-005: Platform-First Strategy

---

## IMPLEMENTED ENGINES

- Enterprise Engine (API, Application, Domain, Infrastructure)
- Organization Engine (API, Application, Domain, Infrastructure)
- Identity Engine (API, Application, Domain, Infrastructure)
- Authorization Engine (API, Application, Domain, Infrastructure)
- Workflow Engine (API, Application, Domain, Infrastructure)

---

## BUSINESS VOCABULARY

The official business vocabulary is maintained in:
- docs/business-language/ (business concepts)
- src/Engines/Enterprise/Domain/DomainVocabulary.md (technical mapping)

Key concepts:
Enterprise, Organization, OrganizationUnit, OrganizationUnitType,
Person, Position, Role, Permission, Document, Asset, Workflow, Transaction, Rule

---

## PLATFORM STRATEGY

Layer One: Eleraki Framework (BuildingBlocks)
Layer Two: Business Applications (ERP first)

All future applications should reuse the framework.

---

## PROJECT PHASES

Phase 1: Project Brain (Completed)
Phase 2: Business Discovery (In Progress)
Phase 3: Business Analysis (In Progress)
Phase 4: Platform Architecture (Completed)
Phase 5: Framework Design (In Progress)
Phase 6: ERP Design (In Progress)
Phase 7: Implementation (In Progress)
Phase 8: Testing (Pending)
Phase 9: First Release (Pending)
Phase 10: Platform Expansion (Pending)

---

## SUCCESS DEFINITION

Success is measured by:
Business Accuracy
Architecture Quality
Maintainability
Scalability
Engineering Quality

Not by:
Number of Screens
Lines of Code
Number of Modules

---

## CURRENT OBJECTIVE

Complete Business Discovery and Business Analysis for ERP and future domains.
Implement remaining Foundation Engines: Notification, Audit, Configuration.
Implement Business Engines: Inventory, Purchasing, Sales, Delivery, Finance, HR.
Write unit and integration tests for all Engines.

---

## IMPORTANT DECISIONS

- Modular Monolith with Clean Architecture per Engine
- Platform-First Strategy
- Documentation-First Development
- Single Source of Truth for organizational data
- Organization Unit Type as Aggregate Root
- Manual Budget Allocation
- Business rules documented before implementation

---

## TEAM AGREEMENT

Founder: Mohamed Eleraki
Role: Product Owner
Responsibilities: Business Vision, Business Decisions, Final Approval

AI Partner: Arsh
Role: Chief Software Architect
Responsibilities: Protect architecture, Protect engineering quality, Challenge weak decisions, Never prioritize speed over quality

Working Style:
Discussions are analytical.
Ideas are challenged.
Better ideas replace previous ideas.
No attachment to old decisions.
Every decision must serve the platform.

---

## WHEN STARTING A NEW CONVERSATION

Read the following documents in order:
1. README.md
2. docs/project/PROJECT_CHARTER.md
3. docs/project/AI_CONTEXT.md
4. docs/project/PROJECT_MEMORY.md (this file)
5. docs/project/CURRENT_STATE.md
6. docs/architecture/overview.md

Continue from the current task. Do not redesign approved decisions.

---

END OF DOCUMENT
