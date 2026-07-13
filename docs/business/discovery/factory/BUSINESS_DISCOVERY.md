# Factory Management - Business Discovery

Version: 1.0
Status: In Progress
Last Updated: 2026-07-11

---

## PURPOSE

This document records the business discovery findings for the Factory Management domain.
It serves as the foundation for building the Factory Management application on the Eleraki Platform.

---

## EXECUTIVE SUMMARY

Factory Management is a complex business domain involving production planning, manufacturing operations, quality control, inventory management, and supply chain operations.

The system must support:
- Production planning and scheduling
- Bill of Materials (BOM) management
- Work order management
- Shop floor operations
- Quality control
- Inventory management
- Equipment maintenance
- Reporting and analytics

---

## STAKEHOLDERS

| Stakeholder | Role | Needs |
|-------------|------|-------|
| Production Manager | Planning | Production schedules, capacity planning |
| Workers | Operations | Work orders, instructions, time tracking |
| Quality Inspector | QC | Inspection plans, defect tracking |
| Store Manager | Inventory | Stock levels, requisitions |
| Maintenance Staff | Equipment | Maintenance schedules, repairs |
| Procurement | Supply Chain | Purchase orders, supplier management |
| Sales | Orders | Order tracking, delivery promises |
| Finance | Costing | Production costs, profitability |
| Management | Reporting | KPIs, efficiency metrics |

---

## CORE BUSINESS PROCESSES

### 1. Production Planning
**Trigger:** Sales forecast or customer order
**Participants:** Production Manager, Sales, Procurement
**Flow:**
1. Demand forecast reviewed
2. Production capacity analyzed
3. Production schedule created
4. Materials requisitioned
5. Work orders released
**Ending Document:** Production Plan, Work Order

### 2. Bill of Materials (BOM) Management
**Trigger:** New product introduction or change
**Participants:** Engineer, Production Manager
**Flow:**
1. Product structure defined
2. Components listed
3. Quantities calculated
4. Routings defined
5. BOM approved and released
**Ending Document:** BOM Document, Routing Sheet

### 3. Work Order Execution
**Trigger:** Production schedule release
**Participants:** Supervisor, Worker, Machine
**Flow:**
1. Work order created from schedule
2. Materials issued from store
3. Operations performed in sequence
4. Time and quantity recorded
5. Completed goods moved to inventory
**Ending Document:** Work Order, Production Record

### 4. Quality Control
**Trigger:** Material receipt, in-process, final inspection
**Participants:** Quality Inspector, Operator
**Flow:**
1. Inspection plan defined
2. Samples taken
3. Measurements recorded
4. Defects identified and classified
5. Corrective actions initiated
6. Final approval given
**Ending Document:** Inspection Report, Defect Record

### 5. Inventory Management
**Trigger:** Stock movement
**Participants:** Store Manager, Worker, Supplier
**Flow:**
1. Raw materials received
2. Quality checked
3. Stock stored
4. Materials issued to production
5. Finished goods received
6. Stock levels monitored
**Ending Document:** Stock Receipt, Stock Issue, Stock Balance

### 6. Equipment Maintenance
**Trigger:** Scheduled maintenance or breakdown
**Participants:** Maintenance Staff, Operator
**Flow:**
1. Maintenance schedule created
2. Preventive maintenance performed
3. Breakdowns reported
4. Repairs executed
5. Maintenance history recorded
**Ending Document:** Maintenance Schedule, Work Order

### 7. Purchase & Procurement
**Trigger:** Material shortage or scheduled purchase
**Participants:** Procurement, Supplier, Store
**Flow:**
1. Material requirement identified
2. Supplier selected
3. Purchase order created
4. Goods received and inspected
5. Invoice processed for payment
**Ending Document:** Purchase Order, Goods Receipt Note

### 8. Sales Order Fulfillment
**Trigger:** Customer order received
**Participants:** Sales, Production, Warehouse
**Flow:**
1. Order received and validated
2. Production scheduled
3. Goods manufactured
4. Quality inspected
5. Goods shipped to customer
**Ending Document:** Sales Order, Delivery Note

---

## KEY BUSINESS EVENTS

| Event | Description | Affected Entities |
|-------|-------------|-------------------|
| Production Planned | Schedule created | ProductionPlan |
| Work Order Created | Manufacturing task initiated | WorkOrder |
| Material Issued | Raw material dispatched | StockMovement |
| Quality Inspection Completed | Inspection finished | InspectionReport |
| Defect Found | Quality issue identified | Defect |
| Production Completed | Manufacturing finished | WorkOrder |
| Stock Received | Inventory received | StockMovement |
| Stock Issued | Inventory dispatched | StockMovement |
| Maintenance Performed | Equipment serviced | MaintenanceRecord |
| Purchase Order Created | Procurement initiated | PurchaseOrder |
| Sales Order Received | Customer order arrived | SalesOrder |
| Delivery Completed | Goods shipped | Delivery |

---

## BUSINESS ENTITIES

### Core Entities
1. **Product** — Manufactured item
2. **BillOfMaterials** — Product structure
3. **WorkOrder** — Manufacturing task
4. **ProductionOrder** — Production schedule
5. **Machine** — Manufacturing equipment
6. **Worker** — Production staff
7. **QualityInspection** — QC record
8. **Defect** — Quality issue
9. **RawMaterial** — Input material
10. **FinishedGood** — Output product
11. **StockMovement** — Inventory transaction
12. **MaintenanceRecord** — Equipment service
13. **PurchaseOrder** — Procurement request
14. **Supplier** — External vendor
15. **SalesOrder** — Customer order
16. **Customer** — External buyer
17. **BOMLine** — Component item
18. **Routing** — Manufacturing steps

---

## BUSINESS RULES

### Production Rules
- **PRD-001:** Work orders must have valid BOM before release.
- **PRD-002:** Materials must be available before work order starts.
- **PRD-003:** Production quantities must match work order specifications.

### Quality Rules
- **QC-001:** All incoming materials must be inspected.
- **QC-002:** Defects must be recorded and traced to batch/lot.
- **QC-003:** Final inspection required before goods can be shipped.

### Inventory Rules
- **INV-001:** Stock cannot go negative.
- **INV-002:** Materials must be issued against valid work order.
- **INV-003:** Expired materials cannot be used in production.

### Maintenance Rules
- **MNT-001:** Preventive maintenance must follow schedule.
- **MNT-002:** Breakdowns must be recorded with root cause.
- **MNT-003:** Maintenance history must be kept for all equipment.

---

## USER ROLES

| Role | Permissions | Description |
|------|-------------|-------------|
| Production Manager | Planning, scheduling | Manufacturing oversight |
| Supervisor | Work orders, operations | Shop floor management |
| Worker | Task execution, time tracking | Production operations |
| Quality Inspector | Inspections, defect recording | Quality assurance |
| Store Manager | Inventory management | Warehouse operations |
| Maintenance Technician | Equipment maintenance | Equipment care |
| Procurement Officer | Purchasing | Supply chain |
| Sales Representative | Order management | Customer orders |
| Accountant | Costing, invoicing | Financial operations |

---

## DOCUMENTS

| Document | Purpose |
|----------|---------|
| Production Plan | Schedule of manufacturing activities |
| Work Order | Manufacturing task instruction |
| BOM Document | Product structure and components |
| Routing Sheet | Manufacturing steps and sequences |
| Inspection Report | Quality check results |
| Defect Record | Quality issue documentation |
| Stock Receipt | Inventory received |
| Stock Issue | Inventory dispatched |
| Maintenance Schedule | Equipment service plan |
| Purchase Order | Procurement request |
| Sales Order | Customer order |
| Delivery Note | Shipment confirmation |

---

## END OF DOCUMENT
