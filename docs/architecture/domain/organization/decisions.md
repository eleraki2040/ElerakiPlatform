# Organization Domain Decisions

Version: 1.0

Status: Active

Last Updated: 2026-07-05

Owner: Organization Engine

---

# PURPOSE

This document records architectural decisions related to the Organization Domain that are pending, deferred, approved, or rejected.

It serves as the official decision backlog for the domain.

Only decisions affecting the Organization Domain belong here.

Cross-platform architectural decisions must be documented as ADRs.

---

# DECISION STATUS

Each decision has one of the following states.

| Status   | Meaning                        |
| -------- | ------------------------------ |
| Pending  | Under discussion               |
| Approved | Accepted and implemented       |
| Rejected | Explicitly rejected            |
| Deferred | Postponed for a future release |

---

# DECISION OD-001

Title

Support Multiple Root Organization Units

Status

Rejected

Reason

The platform models exactly one organizational hierarchy per Organization.

Multiple roots introduce ambiguity and complicate traversal algorithms.

Decision

Each Organization owns exactly one Root Organization Unit.

---

# DECISION OD-002

Title

Maximum Hierarchy Depth

Status

Approved

Decision

Hierarchy depth is unlimited.

Reason

Different organizations require different structural depths.

The platform must not impose artificial limits.

---

# DECISION OD-003

Title

Physical Delete Support

Status

Rejected

Decision

Organization entities are never physically deleted.

Archiving is the standard lifecycle strategy.

Reason

Maintaining historical integrity and auditability is a core platform requirement.

---

# DECISION OD-004

Title

Hardcoded Organization Unit Types

Status

Rejected

Decision

Organization Unit Types are configurable business data.

Reason

Different industries require different classifications.

Hardcoding organizational structures would reduce platform flexibility.

---

# DECISION OD-005

Title

Support Cross-Organization Hierarchies

Status

Rejected

Decision

Organization Units cannot belong to multiple Organizations.

Reason

Ownership boundaries must remain explicit.

---

# DECISION OD-006

Title

Introduce Legal Entity

Status

Deferred

Target Version

2.x

Reason

Legal Entity represents a separate business concept.

Current requirements do not justify introducing it into Version 1.

---

# DECISION OD-007

Title

Introduce Geographic Structure

Status

Deferred

Target Version

2.x

Reason

Geographic hierarchy belongs to a separate domain.

Current Organization Domain should remain location-independent.

---

# DECISION OD-008

Title

Support Organizational Policies

Status

Deferred

Target Version

Future

Reason

Policies vary significantly across industries.

A dedicated Policy Engine may be introduced later.

---

# DECISION OD-009

Title

Support Cost Centers

Status

Deferred

Target Version

Future

Reason

Cost Centers belong primarily to Accounting Domain.

Adding them now would violate separation of concerns.

---

# DECISION OD-010

Title

Allow Organization Unit Type Hierarchies

Status

Pending

Question

Should Organization Unit Types themselves form a hierarchy?

Example

Administrative Unit

└── Department

    └── Finance Department

Current Position

Flat classification.

Architectural review required before approval.

---

# REVIEW POLICY

Deferred decisions must be reviewed before every major platform release.

Approved decisions may only change through a formal Architecture Decision Record (ADR).

---

# RELATED DOCUMENTS

- organization.md
- organization-unit.md
- organization-unit-type.md
- business-rules.md

---

END OF DOCUMENT
