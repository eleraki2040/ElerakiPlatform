# Organization Domain Glossary

Version: 1.0

Status: Approved

Last Updated: 2026-07-05

Owner: Organization Engine

---

# PURPOSE

This glossary defines the official vocabulary (Ubiquitous Language) used throughout the Organization Domain.

Every architect, developer, analyst, tester, and technical writer must use these terms consistently.

The definitions contained in this document are considered the official terminology for the Organization Domain.

---

# CORE TERMS

---

## Organization

The highest-level business entity (Tenant) inside the platform.

An Organization owns the complete organizational structure and defines the business context in which all platform engines operate.

An Organization is NOT:

- Company
- Branch
- Department
- Division

These concepts are represented by Organization Units.

---

## Organization Unit

A structural node within an Organization.

Every Organization Unit belongs to exactly one Organization and participates in the organizational hierarchy.

Examples:

- Company
- Branch
- Department
- Division
- Office
- Factory
- Warehouse
- Hospital
- School
- Faculty
- Laboratory

---

## Organization Unit Type

A configurable business classification assigned to an Organization Unit.

Organization Unit Types describe what a unit represents.

They never define hierarchy.

---

## Organizational Hierarchy

The recursive parent-child structure formed by Organization Units.

Hierarchy has unlimited depth.

Hierarchy is independent of Organization Unit Type.

---

## Root Organization Unit

The first Organization Unit in the hierarchy.

Every Organization must contain exactly one Root Organization Unit.

---

## Parent Organization Unit

The Organization Unit directly above another Organization Unit in the hierarchy.

A Parent may own multiple Children.

---

## Child Organization Unit

An Organization Unit that belongs directly to another Organization Unit.

A Child has exactly one Parent unless it is the Root Organization Unit.

---

## Aggregate Root

A Domain-Driven Design concept representing the entry point for modifying a business aggregate.

Within the Organization Domain, the following Aggregate Roots exist:

- Organization
- Organization Unit Type

Organization Unit is managed through Organization.

---

## Domain

A business boundary containing related concepts, rules, and behaviors.

The Organization Domain models organizational structure only.

---

## Organization Engine

The platform engine responsible for managing:

- Organizations
- Organization Units
- Organization Unit Types

It is the only engine allowed to modify organizational data.

---

## Tenant

A logical business boundary representing one independent Organization inside the platform.

Each tenant owns its own organizational hierarchy.

---

## Business Rule

A mandatory constraint that protects the integrity of the Organization Domain.

Business Rules are independent of:

- Programming language
- Database
- UI
- Framework

---

## Business Event

An event representing something meaningful that has happened inside the domain.

Examples:

- Organization Created
- Organization Unit Moved
- Organization Archived

Business Events may later become Domain Events in software implementation.

---

## Ownership

Defines which Engine is responsible for creating, updating, and maintaining a business concept.

Only one Engine may own a concept.

Other Engines may reference it.

---

## Single Source of Truth

A design principle stating that every business concept has one authoritative owner.

Organization data is owned exclusively by Organization Engine.

---

## Domain Independence

The Organization Domain must remain independent from any specific business sector.

It must support:

- ERP
- CRM
- Hospital
- Factory
- Government
- University
- NGO

without changing its business model.

---

## Configurable

Behavior or metadata that can be changed through business configuration instead of source code modification.

Organization Unit Types are configurable.

---

## Archive

A business operation that marks an entity as inactive while preserving historical information.

Entities within the Organization Domain should be archived rather than physically deleted.

---

# TERMS INTENTIONALLY EXCLUDED

The following concepts do NOT belong to the Organization Domain.

- Employee
- User
- Manager
- Role
- Permission
- Budget
- Salary
- Inventory
- Product
- Customer
- Supplier
- Invoice
- Purchase Order
- Sales Order
- Workflow

These concepts belong to their respective domains and engines.

---

# TERMINOLOGY PRINCIPLES

Every document inside the project must use these definitions.

Alternative terminology should not be introduced without architectural review.

Changes to this glossary require an Architecture Decision Record (ADR).

---

END OF DOCUMENT
