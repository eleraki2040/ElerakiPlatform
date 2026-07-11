# Document

مستند/ملف يُنتج أو يُستهلك داخل Enterprise.

## التعريف

Document هو مستند يُمثل نتيجة عمل أو مدخل مهم في المنصة.

## القواعد

- Document مرتبط بـ Enterprise.
- يمكن أن ينشأ من Workflow أو من Action في أي Engine.
- له Type, Status, CreatedBy, CreatedOn.

## الحقول الأساسية

| الحقل | النوع | الوصف |
|------|------|--------|
| Id | Guid | معرف فريد |
| EnterpriseId | Guid | معرف المؤسسة |
| Type | string | نوع المستند |
| Status | DocumentStatus | حالة المستند |
| CreatedBy | Guid | معرف منشئ المستند |
| CreatedOn | DateTime | تاريخ الإنشاء |
| FileUrl | string | رابط الملف (اختياري) |

## الحالات (Status)

- Draft
- Published
- Archived
- Deleted

## أمثلة

- Invoice
- Contract
- Report
- Policy
- Manual
