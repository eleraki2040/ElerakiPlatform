using Eleraki.SalesEngine.Domain.Events;
using Eleraki.SalesEngine.Domain.Identity;
using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.SalesEngine.Domain.Customers;

public sealed class Customer : AggregateRoot<CustomerId>
{
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Phone { get; private set; } = null!;
    public string? Address { get; private set; }
    public CustomerStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Customer(CustomerId id) : base(id)
    {
    }

    public static Customer Create(string name, string email, string phone, string? address = null)
    {
        Guard.NotNullOrEmpty(name, nameof(name));
        Guard.NotNullOrEmpty(email, nameof(email));
        Guard.NotNullOrEmpty(phone, nameof(phone));

        var customer = new Customer(CustomerId.New())
        {
            Name = name,
            Email = email,
            Phone = phone,
            Address = address,
            Status = CustomerStatus.Active,
            CreatedAt = Clock.UtcNow,
            UpdatedAt = Clock.UtcNow
        };

        customer.RaiseDomainEvent(new CustomerCreatedDomainEvent(customer.Id, Guid.NewGuid(), Clock.UtcNow));

        return customer;
    }

    public void Update(string name, string email, string phone, string? address = null)
    {
        Guard.NotNullOrEmpty(name, nameof(name));
        Guard.NotNullOrEmpty(email, nameof(email));
        Guard.NotNullOrEmpty(phone, nameof(phone));

        Name = name;
        Email = email;
        Phone = phone;
        Address = address;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new CustomerUpdatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Deactivate()
    {
        if (Status == CustomerStatus.Inactive)
            return;

        Status = CustomerStatus.Inactive;
        UpdatedAt = Clock.UtcNow;
        RaiseDomainEvent(new CustomerDeactivatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Activate()
    {
        if (Status == CustomerStatus.Active)
            return;

        Status = CustomerStatus.Active;
        UpdatedAt = Clock.UtcNow;
        RaiseDomainEvent(new CustomerActivatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }
}

public enum CustomerStatus
{
    Active = 1,
    Inactive = 2
}
