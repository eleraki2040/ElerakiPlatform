# PROJECT_MEMORY.md

Version: 1.0
Status: Approved
Purpose:
This document records the approved knowledge, decisions, philosophy, and agreements of the Eleraki Platform project.
It serves as the permanent memory of the project and should be updated whenever a major decision is approved.

============================================================
PROJECT OVERVIEW
============================================================

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

============================================================
FOUNDING IDEA
============================================================

The project is not an ERP project.

It is an Engineering Platform.

ERP is simply the first implementation.

The platform must outlive any individual application.

============================================================
PROJECT MOTTO
============================================================

"We do not develop software.

We understand business,
design its models,
then let the code reflect that understanding."

============================================================
THE FUNDAMENTAL BELIEF
============================================================

Reality is the source of truth.

Software must represent reality.

Business comes before technology.

Technology serves business.

============================================================
HOW WE THINK
============================================================

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

============================================================
BUSINESS THINKING MODEL
============================================================

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

============================================================
ENGINEERING PRINCIPLES
============================================================

Reality First

Business Before Technology

Architecture Before Coding

Behavior Before Data

Documentation Before Implementation

Code Is The Result Of Understanding

Simple Before Complex

Maintainability Over Speed

============================================================
DOCUMENTATION PHILOSOPHY
============================================================

The project must never depend on conversation history.

The project must never depend on human memory.

Knowledge belongs to documentation.

Every approved decision must become a document.

Documentation is part of the product.

============================================================
ARCHITECTURE DECISIONS
============================================================

Current Architecture

Modular Monolith

Reason:

Simple

Maintainable

Easy to test

Easy to deploy

Migration to Microservices will happen only when business needs justify it.

============================================================
PLATFORM STRATEGY
============================================================

Layer One

Eleraki Framework

Contains reusable engineering components.

Layer Two

Business Applications

ERP

Future Applications

Hospital

School

Factory

CRM

HR

Hotel

All future applications should reuse the framework.

============================================================
PROJECT PHASES
============================================================

Phase 1

Project Brain

Phase 2

Business Discovery

Phase 3

Business Language

Phase 4

Research

Phase 5

Analysis

Phase 6

Architecture

Phase 7

Design

Phase 8

Development

============================================================
DECISION MAKING
============================================================

Every decision must answer:

What business problem does it solve?

Who benefits?

What business event does it represent?

What rules govern it?

Can it be reused?

If these questions cannot be answered,
the decision is postponed.

============================================================
QUALITY PRINCIPLES
============================================================

Readable Architecture

Readable Documentation

Readable Code

Explicit Decisions

Low Complexity

High Maintainability

Long-Term Thinking

============================================================
TEAM AGREEMENT
============================================================

Founder

Mohamed Eleraki

Role

Product Owner

Responsibilities

Business Vision

Business Decisions

Final Approval

---

AI Partner

Name

Arsh

Role

Chief Software Architect

Responsibilities

Protect the architecture.

Protect engineering quality.

Protect project philosophy.

Challenge weak decisions.

Prevent unnecessary complexity.

============================================================
WORKING STYLE
============================================================

No coding before understanding.

No feature before understanding business.

No technology decision before understanding business needs.

No unnecessary complexity.

No undocumented decision.

============================================================
LONG-TERM GOAL
============================================================

Build one engineering platform capable of supporting multiple business systems
without rebuilding the engineering foundation every time.

============================================================
CURRENT STATUS
============================================================

Project Brain is under construction.

Documentation is currently the highest priority.

Implementation has not started.

This is intentional.

============================================================
SUCCESS DEFINITION
============================================================

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

============================================================
END OF DOCUMENT
