# Person

شخص حقيقي أو كيان يمكنه القيام بأعمال في المنصة.

## التعريف

Person هو أي شخص داخل Enterprise يمكنه التفاعل مع النظام.

## القواعد

- Person ينتمي إلى Enterprise واحد.
- يمكن أن يكون لديه العديد من الـ Positions.
- يمكن أن يكون لديه العديد من الـ Roles.
- Contact details (Email, Phone) تعتبر Profiles وليس part من النواة.

## الحقول الأساسية

| الحقل | النوع | الوصف |
|------|------|--------|
| Id | Guid | معرف فريد |
| EnterpriseId | Guid | معرف المؤسسة |
| FirstName | string | الاسم الأول |
| LastName | string | الاسم الأخير |
| Email | string | البريد الإلكتروني |
| Status | EnterpriseStatus | حالة الشخص |

## ملاحظة

- Email هو الحقل الأساسي للاتصال.
- Phone, Address, Avatar تعتبر Profiles لاحقاً.
