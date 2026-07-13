# Hospital Management - Processes

Version: 1.0
Status: In Progress
Last Updated: 2026-07-11

---

## 1. Patient Registration & Admission Process
**Trigger:** Patient arrives at hospital (walk-in, referral, or emergency)
**Participants:** Receptionist, Patient, Doctor, Nurse
**Flow:**
1. Patient provides personal information
2. Receptionist creates or retrieves patient record
3. Patient categorized: Outpatient or Inpatient
4. If Inpatient: bed assigned based on availability and patient needs
5. Medical history collected from patient or previous records
6. Insurance information verified (if applicable)
**Ending Document:** Patient Record, Admission Form

---

## 2. Appointment Scheduling Process
**Trigger:** Patient requests appointment
**Participants:** Patient, Receptionist, Doctor
**Flow:**
1. Receptionist checks doctor availability
2. Appointment type determined (consultation, follow-up, procedure)
3. Time slot allocated
4. Appointment confirmed
5. Reminder sent (SMS/Email) before appointment
**Ending Document:** Appointment Record

---

## 3. Consultation & Examination Process
**Trigger:** Doctor meets patient
**Participants:** Doctor, Patient, Nurse
**Flow:**
1. Doctor reviews patient history and vital signs
2. Physical examination performed
3. Diagnosis recorded in medical record
4. Prescription issued if needed
5. Follow-up appointment scheduled if needed
6. Patient discharged (outpatient) or admitted (inpatient)
**Ending Document:** Medical Record, Prescription

---

## 4. Prescription & Pharmacy Process
**Trigger:** Prescription issued by doctor
**Participants:** Doctor, Pharmacist, Patient
**Flow:**
1. Prescription sent to pharmacy electronically or physically
2. Pharmacist verifies prescription and checks for interactions
3. Medication dispensed with instructions
4. Inventory updated
5. Patient receives medication
**Ending Document:** Prescription Record, Dispensing Record

---

## 5. Laboratory & Diagnostics Process
**Trigger:** Doctor orders tests
**Participants:** Doctor, Lab Technician, Patient
**Flow:**
1. Test order created with instructions
2. Sample collected (blood, urine, tissue)
3. Sample labeled and tracked
4. Test performed in laboratory
5. Results validated and recorded
6. Doctor notified of results
**Ending Document:** Lab Report, Test Results

---

## 6. Radiology & Imaging Process
**Trigger:** Doctor orders imaging
**Participants:** Doctor, Radiologist, Patient, Technician
**Flow:**
1. Imaging order created (X-ray, CT, MRI, Ultrasound)
2. Patient prepared for procedure
3. Images captured
4. Radiologist reviews and writes report
5. Results sent to referring doctor
6. Images stored in patient record
**Ending Document:** Radiology Report, Images

---

## 7. Inpatient Care Process
**Trigger:** Patient admitted
**Participants:** Doctor, Nurse, Patient, Pharmacist
**Flow:**
1. Bed assigned and prepared
2. Admission assessment completed
3. Treatment plan created by doctor
4. Daily monitoring and medication administration
5. Progress notes recorded daily
6. Discharge planning initiated when appropriate
**Ending Document:** Treatment Plan, Progress Notes, Discharge Summary

---

## 8. Billing & Insurance Process
**Trigger:** Service provided or patient discharged
**Participants:** Billing Staff, Patient, Insurance Company
**Flow:**
1. All services during encounter itemized
2. Bill generated with charges
3. Insurance claim submitted with required documents
4. Insurance response received (approval/denial)
5. Patient billed for remaining balance
6. Payment collected
7. Receipt issued
**Ending Document:** Invoice, Insurance Claim, Receipt

---

## 9. Inventory Management Process
**Trigger:** Stock movement (receipt, issue, adjustment)
**Participants:** Pharmacist, Store Manager, Supplier
**Flow:**
1. Stock received from supplier
2. Quality and expiry checked
3. Stock stored in designated location
4. Stock issued to departments on request
5. Expired or damaged items identified and removed
6. Stock levels monitored for reordering
**Ending Document:** Stock Receipt, Stock Adjustment, Requisition

---

## 10. Staff Management Process
**Trigger:** HR action (recruitment, scheduling, payroll)
**Participants:** HR Manager, Staff, Department Head
**Flow:**
1. Recruitment and onboarding
2. Schedule created and published
3. Attendance tracked daily
4. Performance evaluated periodically
5. Payroll processed monthly
**Ending Document:** Employment Contract, Schedule, Payroll Record

---

## END OF DOCUMENT
