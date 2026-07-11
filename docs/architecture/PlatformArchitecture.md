# Eleraki Platform — Architecture

هذا الملف يوثق **البنية المعمارية** للمنصة.

---

## الطبقات الرئيسية

```
Eleraki Platform

├── Foundation Engines
│   ├── Enterprise Engine
│   ├── Identity Engine
│   ├── Authorization Engine
│   ├── Workflow Engine
│   ├── Notification Engine
│   └── Audit Engine
│
├── Business Engines
│   ├── Accounting Engine
│   ├── Inventory Engine
│   ├── Sales Engine
│   ├── Purchasing Engine
│   ├── HR Engine
│   ├── CRM Engine
│   └── Manufacturing Engine
│
└── Integration Engines
    ├── Reporting Engine
    ├── BI Engine
    ├── API Gateway
    └── External Connectors
```

---

## Foundation Engines

المحركات الأساسية التي يعتمد عليها كل الـ Business Engines.

### Enterprise Engine

حجر الأساس للمنصة. يدير:
- Enterprise
- Organization Structure
- Settings
- Business Calendar
- Business Identity

### Identity Engine

يدير الهويات لكل الأشخاص داخل Enterprise:
- Person
- Credentials
- Authentication
- Profile

### Authorization Engine

يدير الصلاحيات:
- Role
- Permission
- Access Control

### Workflow Engine

يدير سير العمل:
- Workflow Definition
- Workflow Instance
- Steps
- Transitions

### Notification Engine

يدير الإشعارات:
- Email
- SMS
- Push
- In-App

### Audit Engine

يدير سجلات المراجعة:
- Audit Log
- Change History
- Compliance

---

## Business Engines

المحركات التي تحوي منطق الأعمال الفعلي.

كل Business Engine:
- يبدأ بـ Enterprise
- له Domain Model خاص
- يمكن فصله إلى Microservice
- يتواصل عبر Events/Contracts فقط

### Accounting Engine

يدير المحاسبة:
- Chart of Accounts
- Journal Entries
- Ledger
- Trial Balance
- Financial Statements
- Tax

### Inventory Engine

يدير المخزون:
- Items
- Warehouses
- Stock Movements
- Valuation
- Replenishment

### Sales Engine

يدير المبيعات:
- Quotes
- Orders
- Invoices
- Payments
- Returns

### Purchasing Engine

يدير المشتريات:
- Purchase Requests
- Purchase Orders
- Receipts
- Vendor Invoices
- Payments

### HR Engine

يدير الموارد البشرية:
- Employees
- Attendance
- Leaves
- Payroll
- Performance

### CRM Engine

يدير علاقات العملاء:
- Contacts
- Opportunities
- Campaigns
- Support Tickets

### Manufacturing Engine

يدير الإنتاج:
- Bills of Material
- Work Orders
- Production Planning
- Quality Control

---

## Integration Engines

المحركات التي تربط المنصة بالعالم الخارجي.

### Reporting Engine

- Reports
- Dashboards
- Schedules

### BI Engine

- Data Warehousing
- Analytics
- Data Mining

### API Gateway

- External APIs
- Rate Limiting
- Authentication
- Routing

### External Connectors

- Email Gateways
- Payment Gateways
- SMS Gateways
- File Transfer
- ERP Connectors

---

## القواعد المعمارية

1. **لا توجد مراجع مباشرة بين Engines**
2. **التواصل عبر Events أو Contracts فقط**
3. **كل Engine يمكن فصله إلى Microservice**
4. **SharedKernel يحتوي فقط على المفاهيم العامة**
5. **كل Engine يملك بياناته**
6. **لا Engine يعدل بيانات Engine آخر مباشرة**

---

## الحالة

هذه الوثيقة قابلة للتحديث عند إضافة أو تعديل Engines.
