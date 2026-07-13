using Eleraki.PurchasingEngine.Domain.Entities;
using Eleraki.PurchasingEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Events;

namespace Eleraki.PurchasingEngine.Domain.Events;

public sealed record PurchaseOrderCreatedDomainEvent(PurchaseOrderId PurchaseOrderId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record PurchaseOrderSubmittedDomainEvent(PurchaseOrderId PurchaseOrderId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record PurchaseOrderApprovedDomainEvent(PurchaseOrderId PurchaseOrderId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record PurchaseOrderReceivedDomainEvent(PurchaseOrderId PurchaseOrderId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record PurchaseOrderLineAddedDomainEvent(PurchaseOrderId PurchaseOrderId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record VendorCreatedDomainEvent(VendorId VendorId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
