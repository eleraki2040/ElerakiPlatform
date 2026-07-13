# Business Discovery - Hospital Management

Version: 1.0
Status: In Progress
Last Updated: 2026-07-11

---

## PURPOSE

This document records the business discovery findings for the Hospital Management domain.
It serves as the foundation for building the Hospital Management application on the Eleraki Platform.

---

## EXECUTIVE SUMMARY

Hospital Management is a complex business domain involving patient care, medical staff, appointments, admissions, pharmacy, laboratory, radiology, billing, and insurance.

The system must support:
- Patient registration and medical records
- Doctor and staff management
- Appointment scheduling
- Inpatient and outpatient care
- Pharmacy and inventory
- Laboratory and radiology
- Billing and insurance claims
- Reporting and analytics

---

## STAKEHOLDERS

| Stakeholder | Role | Needs |
|-------------|------|-------|
| Patients | Service recipients | Quality care, clear communication, fair billing |
| Doctors | Medical staff | Patient records, scheduling, clinical tools |
| Nurses | Care staff | Patient monitoring, medication administration |
| Administrators | Management | Reporting, resource planning, compliance |
| Receptionists | Front desk | Registration, appointments, inquiries |
| Pharmacists | Medication management | Inventory, prescriptions, dispensing |
| Lab Technicians | Diagnostics | Sample tracking, results reporting |
| Insurance Companies | Payers | Claims, authorization, billing |
| Regulatory Bodies | Compliance | Reporting, audit trails, standards |

---

## CORE BUSINESS PROCESSES

### 1. Patient Registration & Admission
**Trigger:** Patient arrives at hospital
**Participants:** Receptionist, Patient, Doctor
**Flow:**
1. Patient registers personal information
2. Receptionist creates patient record
3. Patient categorized as outpatient or inpatient
4. If inpatient: bed assignment
5. Medical history collected
**Ending Document:** Patient Record, Admission Form

### 2. Appointment Scheduling
**Trigger:** Patient requests appointment
**Participants:** Patient, Receptionist, Doctor
**Flow:**
1. Receptionist checks doctor availability
2. Appointment scheduled
3. Confirmation sent to patient
4. Reminder sent before appointment
**Ending Document:** Appointment Record

### 3. Consultation & Examination
**Trigger:** Doctor meets patient
**Participants:** Doctor, Patient, Nurse
**Flow:**
1. Doctor reviews patient history
2. Examination performed
3. Diagnosis recorded
4. Prescription issued (if needed)
5. Follow-up scheduled if needed
**Ending Document:** Medical Record, Prescription

### 4. Pharmacy & Medication
**Trigger:** Prescription issued
**Participants:** Doctor, Pharmacist, Patient
**Flow:**
1. Prescription sent to pharmacy
2. Pharmacist verifies and dispenses medication
3. Inventory updated
4. Patient receives medication
**Ending Document:** Prescription Record, Dispensing Record

### 5. Laboratory & Diagnostics
**Trigger:** Doctor orders tests
**Participants:** Doctor, Lab Technician, Patient
**Flow:**
1. Test order created
2. Sample collected
3. Test performed
4. Results recorded
5. Doctor notified
**Ending Document:** Lab Report, Test Results

### 6. Radiology & Imaging
**Trigger:** Doctor orders imaging
**Participants:** Doctor, Radiologist, Patient
**Flow:**
1. Imaging order created
2. Procedure performed
3. Images captured and stored
4. Radiologist reviews and reports
5. Results sent to doctor
**Ending Document:** Radiology Report, Images

### 7. Inpatient Care
**Trigger:** Patient admitted
**Participants:** Doctor, Nurse, Patient
**Flow:**
1. Bed assigned and prepared
2. Treatment plan created
3. Daily monitoring and medication
4. Progress notes recorded
5. Discharge planning
**Ending Document:** Treatment Plan, Progress Notes, Discharge Summary

### 8. Billing & Insurance
**Trigger:** Service provided or discharge
**Participants:** Billing Staff, Patient, Insurance
**Flow:**
1. All services itemized
2. Bill generated
3. Insurance claim submitted (if applicable)
4. Patient pays balance
5. Receipt issued
**Ending Document:** Invoice, Insurance Claim, Receipt

### 9. Inventory Management
**Trigger:** Stock movement
**Participants:** Pharmacist, Store Manager
**Flow:**
1. Stock received from supplier
2. Quality checked
3. Stock stored
4. Stock issued to departments
5. Expired/damaged items removed
**Ending Document:** Stock Receipt, Stock Adjustment

### 10. Staff Management
**Trigger:** HR action
**Participants:** HR Manager, Staff
**Flow:**
1. Recruitment and onboarding
2. Schedule management
3. Attendance tracking
4. Performance evaluation
5. Payroll processing
**Ending Document:** Employment Contract, Schedule, Payroll

---

## KEY BUSINESS EVENTS

| Event | Description | Affected Entities |
|-------|-------------|-------------------|
| Patient Registered | New patient record created | Patient, Person |
| Appointment Scheduled | Doctor appointment booked | Appointment, Doctor, Patient |
| Consultation Completed | Doctor finished examination | MedicalRecord, Doctor, Patient |
| Prescription Issued | Medication prescribed | Prescription, Medication |
| Lab Order Created | Diagnostic test ordered | LabOrder, Test |
| Test Result Recorded | Lab result available | TestResult, LabOrder |
| Radiology Order Created | Imaging ordered | RadiologyOrder, Imaging |
| Radiology Report Issued | Imaging result available | RadiologyReport, RadiologyOrder |
| Admission Created | Patient admitted | Admission, Bed |
| Discharge Processed | Patient discharged | Discharge, Admission |
| Bill Generated | Invoice created | Invoice, Patient |
| Insurance Claim Submitted | Claim sent to insurer | InsuranceClaim, Invoice |
| Payment Received | Payment collected | Payment, Invoice |
| Stock Received | Inventory received | StockReceipt, Product |
| Medication Dispensed | Prescription filled | Dispensing, Prescription |
| Staff Hired | New employee joined | Employee, Person |

---

## BUSINESS ENTITIES

### Core Entities
1. **Patient** — Person receiving care
2. **Doctor** — Medical practitioner
3. **Nurse** — Care provider
4. **Appointment** — Scheduled consultation
5. **MedicalRecord** — Patient medical history
6. **Prescription** — Medication order
7. **LabOrder** — Diagnostic test request
8. **TestResult** — Lab findings
9. **RadiologyOrder** — Imaging request
10. **RadiologyReport** — Imaging findings
11. **Admission** — Inpatient record
12. **Discharge** — Patient release
13. **Invoice** — Billing document
14. **InsuranceClaim** — Insurance billing
15. **Payment** — Financial transaction
16. **Medication** — Pharmaceutical product
17. **Product** — Inventory item
18. **StockReceipt** — Inventory received
19. **Employee** — Staff member
20. **Schedule** — Work schedule

---

## BUSINESS RULES

### Patient Rules
- **PAT-001:** Every patient must have a unique identifier.
- **PAT-002:** Patient records must include emergency contact.
- **PAT-003:** Patient age must be calculated from date of birth.

### Appointment Rules
- **APT-001:** Appointments cannot overlap for the same doctor.
- **APT-002:** Appointments must be scheduled within doctor's working hours.
- **APT-003:** Cancelled appointments must be reschedulable.

### Clinical Rules
- **CLI-001:** Prescriptions must reference valid doctor.
- **CLI-002:** Prescriptions must include dosage and frequency.
- **CLI-003:** Lab results must be reviewed by authorized personnel.
- **CLI-004:** Radiology reports must be signed by radiologist.

### Admission Rules
- **ADM-001:** Admissions require available bed.
- **ADM-002:** Discharge requires doctor approval.
- **ADM-003:** Inpatient stay must have treatment plan.

### Billing Rules
- **BIL-001:** Invoices must itemize all services.
- **BIL-002:** Insurance claims require valid policy number.
- **BIL-003:** Payments must match invoice amount or be partially applied.

### Inventory Rules
- **INV-001:** Medications must track expiry dates.
- **INV-002:** Stock cannot go negative.
- **INV-003:** Controlled substances require special handling.

---

## USER ROLES

| Role | Permissions | Description |
|------|-------------|-------------|
| Administrator | Full Access | System configuration |
| Receptionist | Registration, Appointments | Front desk operations |
| Doctor | Clinical records, Prescriptions | Medical care |
| Nurse | Patient monitoring, Medication | Care delivery |
| Pharmacist | Pharmacy, Inventory | Medication management |
| Lab Technician | Lab orders, Results | Diagnostics |
| Radiologist | Radiology orders, Reports | Imaging |
| Billing Staff | Invoices, Payments, Claims | Financial operations |
| HR Manager | Staff management | Human resources |
| Inventory Manager | Stock management | Supply chain |

---

## DOCUMENTS

| Document | Purpose |
|----------|---------|
| Patient Record | Patient demographics and history |
| Medical Record | Clinical notes and diagnoses |
| Prescription | Medication order |
| Appointment Record | Scheduled consultation |
| Lab Report | Test results |
| Radiology Report | Imaging findings |
| Admission Form | Inpatient registration |
| Discharge Summary | Patient release summary |
| Invoice | Billing document |
| Insurance Claim | Insurance billing |
| Payment Receipt | Payment confirmation |
| Stock Receipt | Inventory received |
| Dispensing Record | Medication dispensed |
| Employment Contract | Staff agreement |
| Work Schedule | Staff timetable |

---

## END OF DOCUMENT
