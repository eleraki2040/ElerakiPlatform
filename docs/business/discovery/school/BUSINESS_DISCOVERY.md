# School Management - Business Discovery

Version: 1.0
Status: In Progress
Last Updated: 2026-07-11

---

## PURPOSE

This document records the business discovery findings for the School Management domain.
It serves as the foundation for building the School Management application on the Eleraki Platform.

---

## EXECUTIVE SUMMARY

School Management is a complex business domain involving students, teachers, academic programs, scheduling, grading, attendance, and administrative operations.

The system must support:
- Student registration and academic records
- Teacher and staff management
- Course and curriculum management
- Class scheduling
- Attendance tracking
- Grading and examination
- Fee management
- Reporting and analytics

---

## STAKEHOLDERS

| Stakeholder | Role | Needs |
|-------------|------|-------|
| Students | Learners | Enrollment, grades, schedule, communication |
| Teachers | Instructors | Class management, grading, attendance |
| Parents | Guardians | Student progress, fees, communication |
| Administrators | Management | Reporting, resource planning, compliance |
| Receptionists | Front desk | Registration, inquiries, documents |
| Accountants | Finance | Fee collection, payroll, reporting |
| Librarians | Resources | Book management, borrowing |
| Transport Staff | Logistics | Route management, tracking |
| Regulatory Bodies | Compliance | Reporting, standards |

---

## CORE BUSINESS PROCESSES

### 1. Student Registration & Admission
**Trigger:** Student applies for admission
**Participants:** Student, Parent, Receptionist, Administrator
**Flow:**
1. Application submitted
2. Documents verified
3. Entrance assessment (if applicable)
4. Admission decision
5. Student record created
6. Class assigned
**Ending Document:** Student Record, Admission Form

### 2. Course & Curriculum Management
**Trigger:** Academic year planning
**Participants:** Academic Head, Teachers, Administrator
**Flow:**
1. Curriculum defined
2. Courses created
3. Teachers assigned
4. Materials prepared
5. Schedule published
**Ending Document:** Course Catalog, Schedule

### 3. Class Scheduling
**Trigger:** Academic year start
**Participants:** Academic Head, Teachers, Students
**Flow:**
1. Time slots defined
2. Rooms assigned
3. Teacher availability checked
4. Student conflicts resolved
5. Final schedule published
**Ending Document:** Class Schedule, Room Assignment

### 4. Attendance Tracking
**Trigger:** Daily class sessions
**Participants:** Teacher, Student, Parent
**Flow:**
1. Teacher marks attendance
2. System records presence/absence
3. Absences flagged
4. Parents notified (if needed)
**Ending Document:** Attendance Record

### 5. Examination & Grading
**Trigger:** Assessment period
**Participants:** Teacher, Student, Administrator
**Flow:**
1. Exam schedule created
2. Exams conducted
3. Papers graded
4. Grades recorded
5. Report cards generated
**Ending Document:** Grade Report, Report Card

### 6. Fee Management
**Trigger:** Academic year or term
**Participants:** Accountant, Student, Parent
**Flow:**
1. Fee structure defined
2. Invoice generated
3. Payment collected
4. Receipt issued
5. Outstanding balances tracked
**Ending Document:** Fee Invoice, Payment Receipt

### 7. Library Management
**Trigger:** Book request or return
**Participants:** Librarian, Student, Teacher
**Flow:**
1. Book catalog maintained
2. Borrowing recorded
3. Returns processed
4. Overdue fines applied
**Ending Document:** Borrowing Record, Fine Receipt

### 8. Transport Management
**Trigger:** Route assignment or daily transport
**Participants:** Transport Staff, Student, Parent
**Flow:**
1. Routes defined
2. Students assigned to routes
3. Daily transport tracked
4. Issues reported
**Ending Document:** Route Assignment, Transport Log

### 9. Staff Management
**Trigger:** HR action
**Participants:** HR Manager, Staff, Administrator
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
| Student Registered | New student enrolled | Student, Person |
| Student Promoted | Student moved to next grade | Student, AcademicRecord |
| Attendance Marked | Student attendance recorded | Attendance, Student, Class |
| Exam Conducted | Assessment completed | Exam, Student, Teacher |
| Grade Recorded | Result entered | Grade, Student, Exam |
| Fee Invoice Generated | Bill created | Invoice, Student |
| Payment Received | Fee payment collected | Payment, Invoice |
| Book Borrowed | Library book issued | Borrowing, Book, Student |
| Book Returned | Library book returned | Borrowing, Book |
| Transport Assigned | Student assigned to route | TransportAssignment, Student |

---

## BUSINESS ENTITIES

### Core Entities
1. **Student** — Enrolled learner
2. **Teacher** — Instructional staff
3. **Parent** — Student guardian
4. **Class** — Student group
5. **Course** — Academic subject
6. **Schedule** — Timetable entry
7. **Attendance** — Daily presence record
8. **Exam** — Assessment event
9. **Grade** — Student result
10. **Fee** — Financial charge
11. **Invoice** — Billing document
12. **Payment** — Financial transaction
13. **Book** — Library resource
14. **Borrowing** — Library transaction
15. **Route** — Transport path
16. **TransportAssignment** — Student route assignment
17. **Employee** — Staff member
18. **EmployeeRole** — Staff position type

---

## BUSINESS RULES

### Student Rules
- **STU-001:** Every student must have a unique enrollment number.
- **STU-002:** Student age must meet grade requirements.
- **STU-003:** Student records cannot be deleted, only archived.

### Attendance Rules
- **ATT-001:** Attendance must be recorded daily for all enrolled students.
- **ATT-002:** Absences above threshold require parent notification.
- **ATT-003:** Attendance records cannot be modified after 7 days.

### Grading Rules
- **GRD-001:** Grades must follow standardized grading scale.
- **GRD-002:** Final grades require academic head approval.
- **GRD-003:** Grade changes must be documented with reason.

### Fee Rules
- **FEE-001:** Invoices must itemize all charges.
- **FEE-002:** Late payments incur penalties.
- **FEE-003:** Fee waivers require manager approval.

### Library Rules
- **LIB-001:** Books can be borrowed only by registered students/staff.
- **LIB-002:** Maximum borrowing limit per person.
- **LIB-003:** Overdue books incur fines.

---

## USER ROLES

| Role | Permissions | Description |
|------|-------------|-------------|
| Administrator | Full Access | System configuration |
| Academic Head | Academic management | Curriculum, exams, grading |
| Teacher | Class management, grading | Instructional staff |
| Student | View own records | Learner |
| Parent | View child's records | Guardian |
| Receptionist | Registration, documents | Front desk |
| Accountant | Fees, payroll | Financial operations |
| Librarian | Library management | Resource management |
| Transport Staff | Route management | Logistics |

---

## DOCUMENTS

| Document | Purpose |
|----------|---------|
| Student Record | Student demographics and academic history |
| Admission Form | Enrollment application |
| Report Card | Academic performance summary |
| Attendance Record | Daily presence tracking |
| Grade Report | Exam results |
| Fee Invoice | Billing document |
| Payment Receipt | Payment confirmation |
| Borrowing Record | Library transaction |
| Employment Contract | Staff agreement |
| Work Schedule | Staff timetable |

---

## END OF DOCUMENT
