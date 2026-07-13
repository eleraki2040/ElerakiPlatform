using Eleraki.AuthorizationEngine.Domain;
using Xunit;

namespace Eleraki.AuthorizationEngine.Domain.Tests;

public class RoleTests
{
    [Fact]
    public void Create_Should_Return_Role_With_Active_Status()
    {
        var role = Role.Create("Admin", "Administrator role");

        Assert.NotNull(role);
        Assert.Equal("Admin", role.Name);
        Assert.Equal("Administrator role", role.Description);
        Assert.True(role.IsActive);
        Assert.NotEqual(default, role.Id);
    }

    [Fact]
    public void Create_Should_Raise_RoleCreatedDomainEvent()
    {
        var role = Role.Create("Admin");

        Assert.Contains(role.DomainEvents, e => e.GetType().Name == "RoleCreatedDomainEvent");
    }

    [Fact]
    public void Activate_Should_Set_IsActive_To_True()
    {
        var role = Role.Create("Admin");
        role.Deactivate();

        role.Activate();

        Assert.True(role.IsActive);
    }

    [Fact]
    public void Deactivate_Should_Set_IsActive_To_False()
    {
        var role = Role.Create("Admin");

        role.Deactivate();

        Assert.False(role.IsActive);
    }

    [Fact]
    public void Update_Should_Change_Name_and_description()
    {
        var role = Role.Create("Admin", "Old description");

        role.Update("SuperAdmin", "New description");

        Assert.Equal("SuperAdmin", role.Name);
        Assert.Equal("New description", role.Description);
    }
}
