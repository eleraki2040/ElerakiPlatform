using Eleraki.SalesEngine.Domain.Customers;
using Eleraki.SalesEngine.Domain.Identity;
using Eleraki.SalesEngine.Domain.SalesOrders;
using Eleraki.SalesEngine.Domain.SalesOrderLines;
using Eleraki.SharedKernel.Events;

namespace Eleraki.SalesEngine.Domain.Events;

public sealed record SalesOrderCreatedDomainEvent(SalesOrderId SalesOrderId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record SalesOrderLineAddedDomainEvent(SalesOrderId SalesOrderId, SalesOrderLineId SalesOrderLineId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record SalesOrderApprovedDomainEvent(SalesOrderId SalesOrderId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record SalesOrderFulfilledDomainEvent(SalesOrderId SalesOrderId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record SalesOrderCancelledDomainEvent(SalesOrderId SalesOrderId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record CustomerCreatedDomainEvent(CustomerId CustomerId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record CustomerUpdatedDomainEvent(CustomerId CustomerId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record CustomerActivatedDomainEvent(CustomerId CustomerId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record CustomerDeactivatedDomainEvent(CustomerId CustomerId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
