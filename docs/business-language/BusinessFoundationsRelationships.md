# Eleraki Platform — Business Foundations Relationships

هذا الملف يوثق **العلاقات** بين المفاهيم السبعة (Business Foundations).

---

## العلاقة الأساسية

```
Enterprise
│
├── Structure
│   ├── Organization Unit
│   ├── Department
│   ├── Branch
│   ├── Team
│   └── Position
│
├── People
│   ├── Person
│   ├── Employee
│   ├── Customer
│   └── Supplier
│
├── Resources
│   ├── Inventory
│   ├── Asset
│   └── Equipment
│
├── Processes
│   ├── Workflow
│   ├── Task
│   └── Approval
│
├── Documents
│   ├── Invoice
│   ├── Contract
│   ├── Purchase Order
│   └── Report
│
├── Transactions
│   ├── Sale
│   ├── Purchase
│   ├── Payment
│   └── Transfer
│
└── Rules
    ├── Business Rule
    ├── Validation Rule
    └── Policy
```

---

## كيف تعمل المفاهيم معاً

### Scenario 1: بيع منتج

1. **Enterprise** تمتلك العملية
2. **People** (Customer) يطلب الشراء
3. **Process** (Sales Process) يُشغل
4. **Document** (Invoice) يُنشأ
5. **Transaction** (Sale) يُسجل
6. **Resource** (Inventory) يُحدّث
7. **Rules** تُطبق (الحد الأدنى للسعر، رصيد العميل...)

### Scenario 2: طلب إجازة

1. **Enterprise** تمتلك العملية
2. **People** (Employee) يقدم الطلب
3. **Process** (Leave Approval) يُشغل
4. **Document** (Leave Request) يُنشأ
5. **Transaction** (Leave Approval) يُسجل
6. **Rules** تُطبق (الرصيد المتبقي، السياسة...)

---

## المبدأ الأساسي

كل مفهوم من المفاهيم السبعة:
- **منفصل** عن الآخرين
- **مرتبط** بـ Enterprise
- **يتواصل** مع المفاهيم الأخرى عبر Events/Contracts فقط

هذا يسمح لنا ببناء Business Engines بشكل مستقل.

---

## الحالة

هذا الملف يوثق العلاقات فقط. التطبيق الفعلي comes later.
