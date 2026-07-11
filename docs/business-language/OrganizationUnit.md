# Organization Unit

وحدة تنظيمية داخل Enterprise.

## التعريف

Organization Unit هي أي وحدة داخل الهيكل التنظيمي لـ Enterprise.

## أمثلة

- Department
- Branch
- Division
- Team
- Sector

## القواعد

- Organization Unit تنتمي إلى Enterprise واحد.
- يمكن أن يكون لها parent Organization Unit (هيكل شجري).
- لها Code و Name.
- ليس لها LegalName.
- Code يجب أن يكون فريداً داخل نفس المستوى within Enterprise.

## الحقول الأساسية

| الحقل | النوع | الوصف |
|------|------|--------|
| Id | Guid | معرف فريد |
| EnterpriseId | Guid | معرف المؤسسة |
| ParentId | Guid? | معرف الوحدة الأب (اختياري) |
| Code | string | كود الوحدة |
| Name | string | اسم الوحدة |
| Type | string | نوع الوحدة (Department, Branch, Team...) |
| Status | EnterpriseStatus | حالة الوحدة |

## الهيكل الشجري

```
Enterprise
└── Organization Unit (HQ)
    ├── Organization Unit (IT Department)
    │   ├── Organization Unit (Development Team)
    │   └── Organization Unit (QA Team)
    └── Organization Unit (HR Department)
```
