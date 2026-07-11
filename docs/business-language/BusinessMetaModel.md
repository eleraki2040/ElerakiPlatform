# Eleraki Platform — Business Meta Model

هذا الملف يوثق **النموذج العام للأعمال** للمنصة.
هذا النموذج هو المرجع لكل تصميمات الـ Engines لاحقاً.

---

## الفكرة الأساسية

Eleraki هي **منصة تشغيل للمؤسسات** (Enterprise Operating Platform)،
تتكون من محركات أعمال مستقلة، لكل محرك مسؤولية أعمال واضحة،
وتتعاون جميعها لبناء أي نظام أعمال.

هذه ليست ERP، وليست مجموعة Modules، بل هي Business Platform.

---

## المفاهيم السبعة (Business Foundations)

### 1. Enterprise

الكيان الأساسي الذي تمثله المؤسسة.

- شركة
- مستشفى
- جامعة
- مصنع
- وزارة
- جمعية
- مؤسسة خيرية

### 2. Structure

كيف تتكون المؤسسة داخلياً.

- Organization Units
- Departments
- Branches
- Teams
- Positions

### 3. People

من يعمل داخل المؤسسة.

- Employee
- Contractor
- User
- Customer
- Supplier
- Partner

### 4. Resources

كل ما تملكه المؤسسة.

- Inventory/Stocks
- Vehicles
- Devices/Machines
- Buildings
- Licenses
- Subscriptions

### 5. Processes

كيف تعمل المؤسسة.

- Purchase Request
- Approval
- Leave Request
- Sales Process
- Production Process
- Hiring Process

### 6. Documents

النتائج/المدخلات المهمة في المؤسسة.

- Invoice
- Contract
- Purchase Order
- Notice
- Letter
- Policy

### 7. Transactions

أي حدث يغير حالة النظام.

- Sale
- Purchase
- Transfer
- Accounting Entry
- Payment
- Receipt

### 8. Rules

قواعد الأعمال التي تحكم المؤسسة.

- No deletion of Enterprise with active Units
- No approval without proper Permission
- No invoice recording in closed period
- No sale below minimum price

---

## القاعدة الذهبية

كل خاصية نضيفها يجب أن نجيب عن سؤال:
> هل تحتاجها جميع أنواع المؤسسات؟

إذا كانت "لا"، فلا نضعها في النواة.

---

## المبادئ الأساسية

1. كل شيء يبدأ من **Enterprise**.
2. No Engine يعرف Enterprise آخر.
3. التواصل بين Engines عبر **Contracts** أو **Events** فقط.
4. كل Engine يمكن فصله لاحقاً إلى Microservice.
5. لا توجد مراجع مباشرة بين Engines.
6. SharedKernel يحتوي فقط على المفاهيم العامة التي لا تنتمي لأي Engine.

---

## الحالة

هذه الوثيقة قابلة للتحديث عند اكتشاف مفاهيم جديدة أو تعديل قواعد عمل.
