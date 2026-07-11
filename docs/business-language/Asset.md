# Asset

أصل مملوك لـ Enterprise.

## التعريف

Asset هو مورد مادي أو غير مادي مملوك لـ Enterprise.

## القواعد

- Asset مرتبط بـ Enterprise.
- يمكن أن يكون مادي أو غير مادي (مثل License, Subscription).
- له Code, Name, Status, Value.

## الحقول الأساسية

| الحقل | النوع | الوصف |
|------|------|--------|
| Id | Guid | معرف فريد |
| EnterpriseId | Guid | معرف المؤسسة |
| Code | string | كود الأصل |
| Name | string | اسم الأصل |
| Type | string | نوع الأصل (مادي/غير مادي) |
| Status | AssetStatus | حالة الأصل |
| Value | decimal? | القيمة المالية (اختياري) |

## الحالات (Status)

- Active
- Inactive
- Disposed
- UnderMaintenance

## أمثلة

- مبنى
- سيارة
- جهاز كمبيوتر
- برنامج (License)
- اشتراك (Subscription)
- علامة تجارية
