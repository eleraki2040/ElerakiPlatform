# Eleraki Platform — Business Language

هذا المجلد يوثق **لغة الأعمال** الأساسية للمنصة.
كل مصطلحاتها هي المصطلحات الرسمية التي ستستخدمها كل الـ Engines لاحقاً.

## الفهرس

1. [Enterprise](./Enterprise.md) — الكيان الأساسي الذي تنتمي إليه كل البيانات
2. [Organization Unit](./OrganizationUnit.md) — وحدة تنظيمية داخل Enterprise
3. [Position](./Position.md) — منصب/دور وظيفي داخل Organization Unit
4. [Person](./Person.md) — شخص حقيقي داخل Enterprise
5. [Role](./Role.md) — دور نظامي يحدد صلاحيات Person
6. [Permission](./Permission.md) — صلاحية واحدة داخل النظام
7. [Document](./Document.md) — مستند يُنتج أو يُستهلك داخل Enterprise
8. [Asset](./Asset.md) — أصل مملوك لـ Enterprise
9. [Workflow](./Workflow.md) — سلسلة خطوات تؤدي إلى نتيجة عمل

## المبادئ الأساسية

1. كل شيء يبدأ من **Enterprise**.
2. No Engine يعرف Enterprise آخر.
3. التواصل بين Engines عبر **Contracts** أو **Events** فقط.
4. كل Engine يمكن فصله لاحقاً إلى Microservice.
5. لا توجد مراجع مباشرة بين Engines.
6. SharedKernel يحتوي فقط على المفاهيم العامة التي لا تنتمي لأي Engine.

## القاعدة الذهبية

كل خاصية نضيفها يجب أن نجيب عن سؤال:
> هل تحتاجها جميع أنواع المؤسسات؟

إذا كانت الإجابة "لا"، فلا نضعها في النواة.

## الحالة

- هذه الوثيقة قابلة للتحديث عند اكتشاف مصطلحات جديدة أو تعديل قواعد عمل.
- بعد الموافقة على هذه المصطلحات، سنبدأ بتصميم Enterprise Aggregate Root ككود.
