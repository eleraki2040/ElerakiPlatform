using Eleraki.DeliveryEngine.Domain.Deliveries;
using Eleraki.DeliveryEngine.Domain.Drivers;
using Eleraki.DeliveryEngine.Domain.Vehicles;
using Eleraki.SharedKernel.Events;

namespace Eleraki.DeliveryEngine.Domain.Events;

public sealed record DeliveryCreatedDomainEvent(DeliveryId DeliveryId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record DriverAssignedDomainEvent(DeliveryId DeliveryId, DriverId DriverId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record DeliveryStartedDomainEvent(DeliveryId DeliveryId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record DeliveryCompletedDomainEvent(DeliveryId DeliveryId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record DeliveryCancelledDomainEvent(DeliveryId DeliveryId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record DriverRegisteredDomainEvent(DriverId DriverId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record DriverDeactivatedDomainEvent(DriverId DriverId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record DriverActivatedDomainEvent(DriverId DriverId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record VehicleRegisteredDomainEvent(VehicleId VehicleId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
