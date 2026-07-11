using Eleraki.AuthorizationEngine.Domain;
using Xunit;

namespace Eleraki.AuthorizationEngine.Domain.Tests;

/// <summary>
/// Domain tests for Role aggregate.
/// </summary>
public class RoleTests
{
    [Fact]
    public void Create_Should_Return_Active_Role()
    {
        var role = Role.Create("Admin", "Administrator role");

        Assert.NotNull(role);
        Assert.Equal("Admin", role.Name);
        Assert.Equal("Administrator role", role.Description);
        Assert.True(role.IsActive);
        Assert.NotEqual(default, role.CreatedOn);
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
    public void Update_Should_Change_Name_And_Description()
    {
        var role = Role.Create("Admin", "Administrator role");

        role.Update("SuperAdmin", "Super administrator role");

        Assert.Equal("SuperAdmin", role.Name);
        Assert.Equal("Super administrator role", role.Description);
    }
}
