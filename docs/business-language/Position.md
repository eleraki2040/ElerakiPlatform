# Position

منصب/دور وظيفي داخل Organization Unit.

## التعريف

Position هو منصب محدد داخل Organization Unit يمكن أن يشغله Person.

## أمثلة

- Manager
- Supervisor
- Accountant
- Developer
- Designer
- Sales Representative

## القواعد

- Position تنتمي إلى Organization Unit واحد.
- يمكن أن يشغلها Person واحد أو أكثر.
- لها Code و Title.
- Code يجب أن يكون فريداً داخل Organization Unit.

## الحقول الأساسية

| الحقل | النوع | الوصف |
|------|------|--------|
| Id | Guid | معرف فريد |
| OrganizationUnitId | Guid | معرف وحدة التنظيم |
| Code | string | كود المنصب |
| Title | string | عنوان المنصب |
| Description | string | وصف المنصب (اختياري) |
| Status | EnterpriseStatus | حالة المنصب |

## العلاقات

- Organization Unit ← many Positions
- Person ← many Positions (شغل)
