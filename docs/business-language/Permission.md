# Permission

صلاحية واحدة داخل النظام.

## التعريف

Permission هي أصغر وحدة صلاحية يمكن منحها لـ Role.

## القواعد

- Permission كائن عالمي (لا يرتبط بـ Enterprise).
- Role يربط بين Enterprise و Permissions.
- Permission له Code و Description.

## الحقول الأساسية

| الحقل | النوع | الوصف |
|------|------|--------|
| Id | Guid | معرف فريد |
| Code | string | كود الصلاحية |
| Description | string | وصف الصلاحية |
| Category | string | فئة الصلاحية |

## أمثلة

- `enterprise:read` - قراءة بيانات المؤسسة
- `enterprise:write` - تعديل بيانات المؤسسة
- `user:read` - قراءة المستخدمين
- `user:write` - تعديل المستخدمين
- `workflow:execute` - تنفيذ workflows

## ملاحظة

- Permissions ثابتة ومحددة من قبل النظام.
- Role يحدد أي Permissions متاحة لـ Enterprise.
