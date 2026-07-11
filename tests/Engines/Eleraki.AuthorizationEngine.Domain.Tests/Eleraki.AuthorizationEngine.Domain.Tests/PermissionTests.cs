using Eleraki.AuthorizationEngine.Domain;
using Xunit;

namespace Eleraki.AuthorizationEngine.Domain.Tests;

/// <summary>
/// Domain tests for Permission aggregate.
/// </summary>
public class PermissionTests
{
    [Fact]
    public void Create_Should_Return_Permission_With_Created_Date()
    {
        var permission = Permission.Create("Users.Read", "users:read", "Read users", "users", PermissionType.Read);

        Assert.NotNull(permission);
        Assert.Equal("Users.Read", permission.Name);
        Assert.Equal("users:read", permission.Code);
        Assert.Equal("Read users", permission.Description);
        Assert.Equal("users", permission.Resource);
        Assert.NotEqual(default, permission.CreatedOn);
    }

    [Fact]
    public void Update_Should_Change_Name_And_Description()
    {
        var permission = Permission.Create("Users.Read", "users:read", "Read users", "users", PermissionType.Read);

        permission.Update("Users.Write", "Write users");

        Assert.Equal("Users.Write", permission.Name);
        Assert.Equal("Write users", permission.Description);
    }
}
