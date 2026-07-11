# Organization

Version: 1.0

Status: Approved

Last Updated: 2026-07-05

Owner: Organization Engine

---

# PURPOSE

The Organization entity represents the root business context (Tenant) within the Eleraki Platform.

It owns the complete organizational structure and provides the business boundary in which all platform engines operate.

Every business application inside the platform belongs to exactly one Organization.

---

# DEFINITION

An Organization is the highest-level business entity inside the platform.

It is **NOT** a company, branch, department, or business unit.

Those concepts are represented by Organization Units.

The Organization acts as the root container for all organizational data.

---

# RESPONSIBILITIES

The Organization entity is responsible for:

- Defining the business identity.
- Owning the organizational hierarchy.
- Owning Organization Units.
- Providing organizational context to other platform engines.
- Defining the lifecycle of the organizational structure.

---

# NON-RESPONSIBILITIES

The Organization entity is NOT responsible for:

- Employees
- Users
- Authentication
- Authorization
- Roles
- Permissions
- Accounting
- Inventory
- Sales
- Purchasing
- Documents
- Workflows

Those responsibilities belong to other platform engines.

---

# BUSINESS EVENTS

The Organization entity may produce the following domain events.

## Organization Created

Triggered when a new organization is created.

---

## Organization Updated

Triggered when organization information changes.

---

## Organization Activated

Triggered when an inactive organization becomes active.

---

## Organization Deactivated

Triggered when an organization becomes inactive.

---

## Organization Archived

Triggered when an organization is archived.

Organizations should never be physically deleted.

---

# BUSINESS RULES

## ORG-001

Every Organization must have a unique identifier.

---

## ORG-002

Every Organization must have a unique business code.

---

## ORG-003

Every Organization must have a unique name.

---

## ORG-004

Every Organization owns exactly one organizational hierarchy.

---

## ORG-005

Every Organization must contain exactly one root Organization Unit.

---

## ORG-006

Organizations cannot be physically deleted.

Only archival is allowed.

---

# ATTRIBUTES

| Attribute   | Required | Description                  |
| ----------- | -------- | ---------------------------- |
| Id          | Yes      | Unique identifier            |
| Code        | Yes      | Business code                |
| Name        | Yes      | Official organization name   |
| DisplayName | No       | Friendly display name        |
| Description | No       | Optional description         |
| Status      | Yes      | Active / Inactive / Archived |
| CreatedAt   | Yes      | Creation timestamp           |
| UpdatedAt   | Yes      | Last modification timestamp  |

---

# RELATIONSHIPS

Organization

├── owns many Organization Units

├── owns many Organization Unit Types

└── is referenced by all platform engines

---

# OWNERSHIP

Owned by:

Organization Engine

Referenced by:

- Identity Engine
- Authorization Engine
- Workflow Engine
- Notification Engine
- Reporting Engine
- Audit Engine

No other engine may own Organization data.

---

# DESIGN PRINCIPLES

- Single Source of Truth
- Domain Independent
- Platform First
- Business First
- Immutable Identity
- Stable Business Boundary

---

# FUTURE EXTENSIONS

The following concepts are intentionally excluded from Version 1.

- Legal Entity
- Business Unit
- Fiscal Year
- Currency
- Time Zone
- Address
- Geographic Region

These concepts require separate architectural review before inclusion.

---

# RELATED DOCUMENTS

- organization-unit.md
- organization-unit-type.md
- business-rules.md

---

END OF DOCUMENT
