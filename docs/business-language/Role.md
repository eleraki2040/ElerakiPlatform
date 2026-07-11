# Role

دور نظامي يحدد صلاحيات Person داخل Enterprise.

## التعريف

Role هو مجموعة صلاحيات يمكن تعيينها لشخص داخل Enterprise.

## القواعد

- Role مرتبط بـ Enterprise.
- Role يحتوي على مجموعة Permissions.
- Person يمكن أن يكون لديه العديد من Roles.
- Role لا يرتبط بـ Organization Unit مباشرة (الربط يأتي من Permission لاحقاً).

## الحقول الأساسية

| الحقل | النوع | الوصف |
|------|------|--------|
| Id | Guid | معرف فريد |
| EnterpriseId | Guid | معرف المؤسسة |
| Code | string | كود الدور |
| Name | string | اسم الدور |
| Description | string | وصف الدور (اختياري) |
| Status | EnterpriseStatus | حالة الدور |

## أمثلة

- Admin
- Manager
- Employee
- Viewer

## ملاحظة

- Role يحدد ما يمكن للشخص فعله، أما Organization Unit فيحدد مكان عمله.
