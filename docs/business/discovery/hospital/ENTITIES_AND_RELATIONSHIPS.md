# Hospital Management - Entities

Version: 1.0
Status: In Progress
Last Updated: 2026-07-11

---

## 1. Patient
- **Role:** The person receiving medical care
- **Relationships:**
  - Has many Appointments
  - Has many MedicalRecords
  - Has many Admissions
  - Has many Invoices
  - Has one Person (demographics)

## 2. Doctor
- **Role:** Medical practitioner providing care
- **Relationships:**
  - Has many Appointments
  - Has many MedicalRecords
  - Has many Prescriptions
  - Has many LabOrders
  - Has many RadiologyOrders
  - Belongs to one Department
  - Has one Person

## 3. Nurse
- **Role:** Care provider assisting doctors and monitoring patients
- **Relationships:**
  - Assigned to many Admissions
  - Administers many Medications
  - Belongs to one Department
  - Has one Person

## 4. Department
- **Role:** Clinical or administrative unit (Emergency, Cardiology, Radiology, etc.)
- **Relationships:**
  - Has many Doctors
  - Has many Nurses
  - Belongs to one Organization

## 5. Appointment
- **Role:** Scheduled consultation between patient and doctor
- **Relationships:**
  - Belongs to one Patient
  - Belongs to one Doctor
  - May result in one MedicalRecord
  - May result in one Prescription

## 6. MedicalRecord
- **Role:** Patient's clinical history and current condition
- **Relationships:**
  - Belongs to one Patient
  - Created by one Doctor
  - May have many Prescriptions
  - May have many LabOrders
  - May have many RadiologyOrders

## 7. Prescription
- **Role:** Doctor's order for medication
- **Relationships:**
  - Belongs to one MedicalRecord
  - Prescribed by one Doctor
  - For one Patient
  - May result in one Dispensing

## 8. Medication
- **Role:** Pharmaceutical product available in pharmacy
- **Relationships:**
  - Has many Dispensings
  - Has many StockMovements
  - Belongs to one Category

## 9. LabOrder
- **Role:** Request for laboratory testing
- **Relationships:**
  - Belongs to one MedicalRecord
  - Ordered by one Doctor
  - For one Patient
  - Has many TestResults

## 10. TestResult
- **Role:** Outcome of laboratory test
- **Relationships:**
  - Belongs to one LabOrder
  - Recorded by Lab Technician

## 11. RadiologyOrder
- **Role:** Request for imaging
- **Relationships:**
  - Belongs to one MedicalRecord
  - Ordered by one Doctor
  - For one Patient
  - Has one RadiologyReport

## 12. RadiologyReport
- **Role:** Radiologist's interpretation of imaging
- **Relationships:**
  - Belongs to one RadiologyOrder
  - Written by one Radiologist

## 13. Admission
- **Role:** Inpatient record
- **Relationships:**
  - Belongs to one Patient
  - Assigned to one Bed
  - Has many ProgressNotes
  - May have many Prescriptions
  - Results in one Discharge

## 14. Bed
- **Role:** Physical bed in ward
- **Relationships:**
  - Belongs to one Ward
  - May be assigned to one Admission

## 15. Ward
- **Role:** Patient room area
- **Relationships:**
  - Belongs to one Department
  - Has many Beds

## 16. Invoice
- **Role:** Billing document for services
- **Relationships:**
  - Belongs to one Patient
  - May have many InvoiceLines
  - May have one InsuranceClaim
  - May have many Payments

## 17. InvoiceLine
- **Role:** Individual charge on invoice
- **Relationships:**
  - Belongs to one Invoice
  - References service or item

## 18. InsuranceClaim
- **Role:** Claim submitted to insurance company
- **Relationships:**
  - Belongs to one Invoice
  - Belongs to one InsurancePolicy

## 19. InsurancePolicy
- **Role:** Patient's insurance coverage
- **Relationships:**
  - Belongs to one Patient
  - Belongs to one InsuranceCompany
  - May have many InsuranceClaims

## 20. InsuranceCompany
- **Role:** Third-party payer
- **Relationships:**
  - Has many InsurancePolicies
  - Receives many InsuranceClaims

## 21. Payment
- **Role:** Financial transaction
- **Relationships:**
  - Belongs to one Invoice
  - Made by one Patient

## 22. Employee
- **Role:** Hospital staff member
- **Relationships:**
  - Has one Person
  - Has one EmployeeRole
  - Has many Schedules
  - Has many AttendanceRecords

## 23. EmployeeRole
- **Role:** Staff role (Doctor, Nurse, Receptionist, etc.)
- **Relationships:**
  - Has many Employees

## 24. Schedule
- **Role:** Work schedule for employee
- **Relationships:**
  - Belongs to one Employee
  - Covers specific date range

---

## END OF DOCUMENT
