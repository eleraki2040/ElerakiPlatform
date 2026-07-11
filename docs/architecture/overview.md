# Architecture Overview

Version: 1.0
Status: Approved
Last Updated: 2026-07-05

---

## PURPOSE

This document provides a high-level overview of the Eleraki Platform architecture.

---

## PLATFORM LAYERS

### Layer 1: Eleraki Framework
Reusable engineering foundation containing:
- Common components
- Infrastructure
- Utilities
- Patterns

### Layer 2: Business Applications
Applications built on top of the framework:
- Eleraki ERP (first implementation)
- Future applications (Hospital, School, Factory, CRM, HR, Hotel)

---

## ARCHITECTURE STYLE

**Modular Monolith**

The platform follows a modular monolith architecture with clear module boundaries.

Migration to Microservices will occur only when justified by business and technical needs.

---

## CORE PRINCIPLES

1. **Reality First** - Software must represent business reality
2. **Business Before Technology** - Technology serves business
3. **Architecture Before Coding** - Done before implementation
4. **Documentation Before Implementation** - Knowledge preserved in docs
5. **Single Source of Truth** - Each concept has one owner

---

## KEY ENGINES

- **Organization Engine** - Manages organizational structure
- **Identity Engine** - Manages users and authentication
- **Authorization Engine** - Manages permissions and roles
- *(Additional engines to be defined)*

---

## RELATED DOCUMENTS

- principles.md
- decisions/
- domain/organization/
- engines/

---

END OF DOCUMENT