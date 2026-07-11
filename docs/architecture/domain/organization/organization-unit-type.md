# Organization Unit Type

Version: 1.0

Status: Approved

Last Updated: 2026-07-05

Owner: Organization Engine

---

# PURPOSE

The Organization Unit Type defines the business classification of an Organization Unit.

It allows the platform to model different organizational structures without changing the source code.

Organization Unit Types are business configuration data rather than application logic.

---

# DEFINITION

An Organization Unit Type describes **what an Organization Unit represents**, not **where it exists** in the hierarchy.

Examples include:

- Company
- Branch
- Department
- Division
- Region
- Office
- Warehouse
- Factory
- Store
- Hospital
- Faculty
- Laboratory

Hierarchy is determined by Parent/Child relationships, not by the type itself.

---

# RESPONSIBILITIES

The Organization Unit Type is responsible for:

- Classifying Organization Units.
- Providing business meaning to structural nodes.
- Supporting configurable organizational models.
- Enabling reusable platform architecture.

---

# NON-RESPONSIBILITIES

Organization Unit Type is NOT responsible for:

- Defining hierarchy.
- Managing relationships.
- Managing employees.
- Managing permissions.
- Managing business rules outside organizational classification.
- Containing application logic.

---

# BUSINESS EVENTS

## Organization Unit Type Created

Triggered when a new type is added.

---

## Organization Unit Type Updated

Triggered when its metadata changes.

---

## Organization Unit Type Activated

Triggered when a type becomes available.

---

## Organization Unit Type Deactivated

Triggered when a type becomes unavailable.

---

# BUSINESS RULES

## OUT-001

Every Organization Unit Type must have a unique identifier.

---

## OUT-002

Every Organization Unit Type must have a unique business code.

---

## OUT-003

Organization Unit Types are configurable.

They must never be hardcoded.

---

## OUT-004

Deactivating a type does not affect existing Organization Units.

It only prevents creating new units of that type.

---

## OUT-005

Organization Unit Types may be reused by multiple Organizations.

---

## OUT-006

Organization Unit Types describe classification only.

They never define hierarchy.

---

# ATTRIBUTES

| Attribute   | Required | Description                 |
| ----------- | -------- | --------------------------- |
| Id          | Yes      | Unique identifier           |
| Code        | Yes      | Business code               |
| Name        | Yes      | Display name                |
| Description | No       | Optional description        |
| Icon        | No       | UI icon identifier          |
| Color       | No       | Optional UI color           |
| SortOrder   | No       | Display order               |
| IsActive    | Yes      | Availability status         |
| CreatedAt   | Yes      | Creation timestamp          |
| UpdatedAt   | Yes      | Last modification timestamp |

---

# RELATIONSHIPS

Organization Unit Type

↓

used by

↓

Organization Unit

One Organization Unit Type may classify many Organization Units.

---

# EXAMPLES

Example 1

Type

Company

Used By

ABC Holding

XYZ Group

---

Example 2

Type

Branch

Used By

Cairo Branch

Alexandria Branch

Dubai Branch

---

Example 3

Type

Department

Used By

Sales

Accounting

Human Resources

IT

---

# DESIGN PRINCIPLES

- Configurable
- Extensible
- Domain Independent
- No Hardcoded Types
- Business Driven
- Platform First

---

# FUTURE EXTENSIONS

The following capabilities may be added after architectural validation:

- Custom Icons Library
- Localization
- Type Categories
- Display Templates
- Validation Policies
- Default Configuration

---

# OWNERSHIP

Owned exclusively by:

Organization Engine

Referenced by:

- UI
- Reporting Engine
- Workflow Engine
- Identity Engine

No other engine may own Organization Unit Types.

---

# RELATED DOCUMENTS

- organization.md
- organization-unit.md
- business-rules.md

---

END OF DOCUMENT
