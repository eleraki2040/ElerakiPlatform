using Eleraki.SalesEngine.Domain.Customers;
using Eleraki.SalesEngine.Domain.Identity;
using FluentAssertions;

namespace Eleraki.SalesEngine.Domain.Tests;

public class CustomerTests
{
    [Fact]
    public void Create_Should_Return_Customer_With_Active_Status()
    {
        var customer = Customer.Create("Eleraki", "info@eleraki.com", "+254700000000");

        customer.Should().NotBeNull();
        customer.Name.Should().Be("Eleraki");
        customer.Email.Should().Be("info@eleraki.com");
        customer.Phone.Should().Be("+254700000000");
        customer.Status.Should().Be(CustomerStatus.Active);
        customer.Address.Should().BeNull();
        customer.Id.Should().NotBe(default(CustomerId));
    }

    [Fact]
    public void Create_Should_Raise_CustomerCreatedDomainEvent()
    {
        var customer = Customer.Create("Eleraki", "info@eleraki.com", "+254700000000");

        customer.DomainEvents.Should().Contain(e => e.GetType().Name == "CustomerCreatedDomainEvent");
    }

    [Fact]
    public void Create_Should_Set_Address_When_Provided()
    {
        var customer = Customer.Create("Eleraki", "info@eleraki.com", "+254700000000", "Nairobi, Kenya");

        customer.Address.Should().Be("Nairobi, Kenya");
    }

    [Fact]
    public void Deactivate_Should_Set_Status_To_Inactive()
    {
        var customer = Customer.Create("Eleraki", "info@eleraki.com", "+254700000000");

        customer.Deactivate();

        customer.Status.Should().Be(CustomerStatus.Inactive);
    }

    [Fact]
    public void Deactivate_Should_Raise_CustomerDeactivatedDomainEvent()
    {
        var customer = Customer.Create("Eleraki", "info@eleraki.com", "+254700000000");

        customer.Deactivate();

        customer.DomainEvents.Should().Contain(e => e.GetType().Name == "CustomerDeactivatedDomainEvent");
    }

    [Fact]
    public void Deactivate_Should_Be_Idempotent()
    {
        var customer = Customer.Create("Eleraki", "info@eleraki.com", "+254700000000");

        customer.Deactivate();
        customer.ClearDomainEvents();
        customer.Deactivate();

        customer.Status.Should().Be(CustomerStatus.Inactive);
        customer.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public void Activate_Should_Set_Status_To_Active()
    {
        var customer = Customer.Create("Eleraki", "info@eleraki.com", "+254700000000");
        customer.Deactivate();

        customer.Activate();

        customer.Status.Should().Be(CustomerStatus.Active);
    }

    [Fact]
    public void Activate_Should_Raise_CustomerActivatedDomainEvent()
    {
        var customer = Customer.Create("Eleraki", "info@eleraki.com", "+254700000000");
        customer.Deactivate();
        customer.ClearDomainEvents();

        customer.Activate();

        customer.DomainEvents.Should().Contain(e => e.GetType().Name == "CustomerActivatedDomainEvent");
    }

    [Fact]
    public void Activate_Should_Be_Idempotent()
    {
        var customer = Customer.Create("Eleraki", "info@eleraki.com", "+254700000000");
        customer.Deactivate();
        customer.ClearDomainEvents();

        customer.Activate();
        customer.ClearDomainEvents();
        customer.Activate();

        customer.Status.Should().Be(CustomerStatus.Active);
        customer.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public void Create_Should_Throw_When_Name_Is_Null()
    {
        Action act = () => Customer.Create(null!, "info@eleraki.com", "+254700000000");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_Should_Throw_When_Email_Is_Null()
    {
        Action act = () => Customer.Create("Eleraki", null!, "+254700000000");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_Should_Throw_When_Phone_Is_Null()
    {
        Action act = () => Customer.Create("Eleraki", "info@eleraki.com", null!);

        act.Should().Throw<ArgumentException>();
    }
}
