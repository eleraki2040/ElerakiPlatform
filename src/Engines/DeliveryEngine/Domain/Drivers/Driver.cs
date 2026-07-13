using Eleraki.DeliveryEngine.Domain.Deliveries;
using Eleraki.DeliveryEngine.Domain.Events;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;

namespace Eleraki.DeliveryEngine.Domain.Drivers;

public sealed class Driver : AggregateRoot<DriverId>
{
    public string FullName { get; private set; } = string.Empty;
    public string LicenseNumber { get; private set; } = string.Empty;
    public PhoneNumber Phone { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public DriverStatus Status { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime ModifiedOn { get; private set; }

    private Driver(DriverId id) : base(id)
    {
        FullName = default!;
        LicenseNumber = default!;
        Phone = default!;
        Email = default!;
    }

    public static Driver Create(string fullName, string licenseNumber, PhoneNumber phone, Email email)
    {
        Guard.NotNullOrEmpty(fullName, nameof(fullName));
        Guard.NotNullOrEmpty(licenseNumber, nameof(licenseNumber));
        Guard.NotNull(phone, nameof(phone));
        Guard.NotNull(email, nameof(email));

        var driver = new Driver(DriverId.New())
        {
            FullName = fullName,
            LicenseNumber = licenseNumber,
            Phone = phone,
            Email = email,
            Status = DriverStatus.Active,
            CreatedOn = Clock.UtcNow,
            ModifiedOn = Clock.UtcNow
        };

        driver.RaiseDomainEvent(new DriverRegisteredDomainEvent(driver.Id, Guid.NewGuid(), Clock.UtcNow));

        return driver;
    }

    public void Deactivate()
    {
        if (Status == DriverStatus.Inactive)
            return;

        Status = DriverStatus.Inactive;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new DriverDeactivatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Activate()
    {
        if (Status == DriverStatus.Active)
            return;

        Status = DriverStatus.Active;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new DriverActivatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }
}

public enum DriverStatus
{
    Active = 1,
    Inactive = 2,
    OnLeave = 3
}
