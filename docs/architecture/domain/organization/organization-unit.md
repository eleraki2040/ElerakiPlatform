# Organization Unit

Version: 1.0

Status: Approved

Last Updated: 2026-07-05

Owner: Organization Engine

---

# PURPOSE

The Organization Unit represents any structural element within an Organization.

It provides a universal, configurable building block capable of modeling the organizational structure of any business domain without changing the platform architecture.

Instead of creating dedicated entities such as Branch, Department, Division, Factory, Hospital, or School, the platform represents them all as Organization Units with different types.

---

# DEFINITION

An Organization Unit is a node in the organizational hierarchy.

Each Organization Unit belongs to exactly one Organization and may have a parent Organization Unit.

Together, Organization Units form a hierarchical tree that represents the organizational structure.

---

# RESPONSIBILITIES

The Organization Unit is responsible for:

- Representing a structural node.
- Participating in the organizational hierarchy.
- Maintaining parent-child relationships.
- Identifying its organizational type.
- Providing structural information to other platform engines.

---

# NON-RESPONSIBILITIES

The Organization Unit is NOT responsible for:

- Employees
- Managers
- User Accounts
- Roles
- Permissions
- Budgets
- Accounting
- Inventory
- Sales
- Documents
- Workflows

Those concepts belong to their respective platform engines.

---

# BUSINESS EVENTS

## Organization Unit Created

Triggered when a new organizational unit is created.

---

## Organization Unit Updated

Triggered when information changes.

---

## Organization Unit Moved

Triggered when the parent changes.

---

## Organization Unit Activated

Triggered when a unit becomes active.

---

## Organization Unit Deactivated

Triggered when a unit becomes inactive.

---

## Organization Unit Archived

Triggered when a unit is archived.

Organization Units should never be physically deleted.

---

# BUSINESS RULES

## OU-001

Every Organization Unit belongs to exactly one Organization.

---

## OU-002

Every Organization Unit has exactly one Organization Unit Type.

---

## OU-003

Every Organization Unit may have zero or one Parent.

---

## OU-004

Every Organization Unit may have zero or many Children.

---

## OU-005

Circular references are prohibited.

Example:

A

└── B

    └── C

        └── A ❌

---

## OU-006

A Parent Organization Unit must belong to the same Organization.

---

## OU-007

Organization Units cannot be physically deleted.

Only archival is allowed.

---

## OU-008

Inactive Organization Units cannot receive new children.

---

## OU-009

Organization Unit Codes must be unique within the same Organization.

---

## OU-010

Hierarchy depth is unlimited.

The platform must never impose a fixed hierarchy depth.

---

# ATTRIBUTES

| Attribute      | Required | Description                  |
| -------------- | -------- | ---------------------------- |
| Id             | Yes      | Unique identifier            |
| OrganizationId | Yes      | Owner Organization           |
| TypeId         | Yes      | Organization Unit Type       |
| ParentId       | No       | Parent Organization Unit     |
| Code           | Yes      | Business code                |
| Name           | Yes      | Official name                |
| DisplayName    | No       | Friendly name                |
| Description    | No       | Optional description         |
| Status         | Yes      | Active / Inactive / Archived |
| CreatedAt      | Yes      | Creation timestamp           |
| UpdatedAt      | Yes      | Last modification timestamp  |

---

# RELATIONSHIPS

Organization

↓

Organization Unit

↓

Parent

↓

Children

The hierarchy forms a recursive tree.

---

# EXAMPLES

Business Company

Organization

└── Head Office

    ├── Finance

    ├── Sales

    ├── HR

    └── IT

---

Hospital

Organization

└── Main Hospital

    ├── Emergency

    ├── Radiology

    ├── Pharmacy

    └── ICU

---

University

Organization

└── University

    ├── Faculty of Engineering

    │   ├── Computer Science

    │   └── Civil Engineering

    └── Faculty of Medicine

---

Factory

Organization

└── Factory

    ├── Production

    ├── Quality

    ├── Warehouse

    └── Maintenance

---

# OWNERSHIP

Owned exclusively by:

Organization Engine

Referenced by:

- Identity Engine
- Authorization Engine
- Workflow Engine
- Notification Engine
- Reporting Engine
- Audit Engine

No other engine may modify Organization Units directly.

---

# DESIGN PRINCIPLES

- Unlimited Hierarchy
- Self-Referencing Structure
- Domain Independent
- Configurable
- Stable Identity
- Platform First

---

# FUTURE EXTENSIONS

The following concepts are intentionally excluded from Version 1:

- Organizational Policies
- Capacity
- Geographic Coordinates
- Contact Information
- Working Hours
- Cost Centers
- Business Ownership
- Security Classification

These concepts require separate architectural review before inclusion.

---

# RELATED DOCUMENTS

- organization.md
- organization-unit-type.md
- business-rules.md

---

END OF DOCUMENT
