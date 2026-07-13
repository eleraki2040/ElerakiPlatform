using Eleraki.PurchasingEngine.Domain.Entities;
using FluentAssertions;

namespace Eleraki.PurchasingEngine.Domain.Tests;

public class VendorTests
{
    [Fact]
    public void Create_ShouldSetActiveStatus()
    {
        var vendor = Vendor.Create("Acme Corp");

        vendor.Name.Should().Be("Acme Corp");
        vendor.Status.Should().Be(VendorStatus.Active);
    }

    [Fact]
    public void Activate_ShouldSetStatusToActive()
    {
        var vendor = Vendor.Create("Acme Corp");
        vendor.Deactivate();

        vendor.Activate();

        vendor.Status.Should().Be(VendorStatus.Active);
    }

    [Fact]
    public void Deactivate_ShouldSetStatusToInactive()
    {
        var vendor = Vendor.Create("Acme Corp");

        vendor.Deactivate();

        vendor.Status.Should().Be(VendorStatus.Inactive);
    }
}
