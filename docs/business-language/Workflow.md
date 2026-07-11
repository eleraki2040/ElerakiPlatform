# Workflow

سلسلة من الخطوات/الأنشطة التي تؤدي إلى نتيجة عمل داخل Enterprise.

## التعريف

Workflow هو عملية منظمة تحتوي على خطوات متسلسلة تُنفذ داخل Enterprise.

## القواعد

- Workflow مرتبط بـ Enterprise.
- يمكن أن يُشغل من أي Engine.
- له Trigger, Steps, Actors, Status.

## الحقول الأساسية

| الحقل | النوع | الوصف |
|------|------|--------|
| Id | Guid | معرف فريد |
| EnterpriseId | Guid | معرف المؤسسة |
| Code | string | كود الـ Workflow |
| Name | string | اسم الـ Workflow |
| Version | string | إصدار الـ Workflow |
| Status | WorkflowStatus | حالة الـ Workflow |
| CreatedBy | Guid | معرف المنشئ |
| CreatedOn | DateTime | تاريخ الإنشاء |

## الحالات (Status)

- Draft
- Active
- Paused
- Completed
- Cancelled

## أمثلة

- طلب إجازة
- طلب مصروف
- موافقة على مستند
- عملية تعيين
- سير عمل procurement

## ملاحظة

- Workflow يمكن أن يُشغل من أي Engine (Identity, Accounting, HR...).
- Step يحدد من مسؤول (Actor) عن التنفيذ.
