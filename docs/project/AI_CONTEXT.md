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

## ARCHITECTURE DECISION

Current Architecture:

Modular Monolith

Reason:

Simple

Maintainable

Easy to evolve

Migration to Microservices will happen ONLY when required.

---

## PLATFORM STRATEGY

Layer 1

Framework

Reusable engineering foundation.

Layer 2

Applications

ERP

Future systems

All applications should reuse the framework.

---

## DOCUMENTATION STRATEGY

The project must never depend on conversation history.

Knowledge belongs to documentation.

Every approved decision must become a document.

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

---

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

Project Brain Construction

Current Objective

Build the engineering knowledge before writing code.

---

END
