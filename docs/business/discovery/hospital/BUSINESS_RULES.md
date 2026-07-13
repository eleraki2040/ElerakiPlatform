# Hospital Management - Business Rules

Version: 1.0
Status: In Progress
Last Updated: 2026-07-11

---

## Patient Rules
- **PAT-001:** Every patient must have a unique identifier.
- **PAT-002:** Patient records must include emergency contact.
- **PAT-003:** Patient age must be calculated from date of birth.
- **PAT-004:** Patient records cannot be physically deleted; only deactivated.

## Appointment Rules
- **APT-001:** Appointments cannot overlap for the same doctor.
- **APT-002:** Appointments must be scheduled within doctor's working hours.
- **APT-003:** Cancelled appointments must be reschedulable.
- **APT-004:** Appointment duration must respect appointment type.

## Clinical Rules
- **CLI-001:** Prescriptions must reference a valid doctor.
- **CLI-002:** Prescriptions must include dosage and frequency.
- **CLI-003:** Prescriptions must have expiration date.
- **CLI-004:** Lab results must be reviewed by authorized personnel before release.
- **CLI-005:** Radiology reports must be signed by a radiologist.
- **CLI-006:** Medical records are immutable after signing.

## Admission Rules
- **ADM-001:** Admissions require an available bed.
- **ADM-002:** Discharge requires doctor approval.
- **ADM-003:** Inpatient stay must have a treatment plan.
- **ADM-004:** Patient cannot be admitted to multiple beds simultaneously.

## Billing Rules
- **BIL-001:** Invoices must itemize all services provided.
- **BIL-002:** Insurance claims require valid policy number.
- **BIL-003:** Payments must match invoice amount or be partially applied.
- **BIL-004:** Invoices cannot be modified after payment.

## Inventory Rules
- **INV-001:** Medications must track expiry dates.
- **INV-002:** Stock cannot go negative.
- **INV-003:** Controlled substances require special handling and tracking.
- **INV-004:** Stock adjustments require authorization.

## Staff Rules
- **STF-001:** Employees must have valid employment contract.
- **STF-002:** Schedules must respect labor regulations (max hours, rest periods).
- **STF-003:** Payroll must respect contract terms.

---

## END OF DOCUMENT
