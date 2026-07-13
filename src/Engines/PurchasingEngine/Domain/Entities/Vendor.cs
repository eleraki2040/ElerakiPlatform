using Eleraki.PurchasingEngine.Domain.Events;
using Eleraki.PurchasingEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.PurchasingEngine.Domain.Entities;

public sealed class Vendor : AggregateRoot<VendorId>
{
    public string Name { get; private set; } = string.Empty;
    public string? ContactEmail { get; private set; }
    public string? ContactPhone { get; private set; }
    public string? Address { get; private set; }
    public VendorStatus Status { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime ModifiedOn { get; private set; }

    private Vendor(VendorId id) : base(id)
    {
    }

    public static Vendor Create(string name, string? contactEmail = null, string? contactPhone = null, string? address = null)
    {
        Guard.NotNullOrEmpty(name, nameof(name));

        var vendor = new Vendor(VendorId.New())
        {
            Name = name,
            ContactEmail = contactEmail,
            ContactPhone = contactPhone,
            Address = address,
            Status = VendorStatus.Active,
            CreatedOn = Clock.UtcNow,
            ModifiedOn = Clock.UtcNow
        };

        vendor.RaiseDomainEvent(new VendorCreatedDomainEvent(vendor.Id, Guid.NewGuid(), Clock.UtcNow));

        return vendor;
    }

    public void Update(string name, string? contactEmail = null, string? contactPhone = null, string? address = null)
    {
        Guard.NotNullOrEmpty(name, nameof(name));

        Name = name;
        ContactEmail = contactEmail;
        ContactPhone = contactPhone;
        Address = address;
        ModifiedOn = Clock.UtcNow;
    }

    public void Activate()
    {
        if (Status == VendorStatus.Active)
            return;

        Status = VendorStatus.Active;
        ModifiedOn = Clock.UtcNow;
    }

    public void Deactivate()
    {
        if (Status == VendorStatus.Inactive)
            return;

        Status = VendorStatus.Inactive;
        ModifiedOn = Clock.UtcNow;
    }

    public void Blacklist()
    {
        Status = VendorStatus.Blacklisted;
        ModifiedOn = Clock.UtcNow;
    }
}

public enum VendorStatus
{
    Active = 1,
    Inactive = 2,
    Blacklisted = 3
}
