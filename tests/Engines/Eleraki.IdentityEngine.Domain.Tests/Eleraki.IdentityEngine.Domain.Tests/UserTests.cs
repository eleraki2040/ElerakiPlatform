using Eleraki.IdentityEngine.Domain;

using Xunit;

namespace Eleraki.IdentityEngine.Domain.Tests;

public class UserTests
{
    [Fact]
    public void Create_Should_Return_User_With_Created_Date()
    {
        var name = UserName.Create("Test User");
        var email = global::Eleraki.SharedKernel.ValueObjects.Email.Create("test@example.com");
        var password = UserPassword.Create("Password123");

        var user = User.Create(name, email, password);

        Assert.NotNull(user);
        Assert.Equal("Test User", user.Name.Value);
        Assert.Equal("test@example.com", user.Email.Value);
        Assert.True(user.IsActive);
        Assert.NotEqual(default, user.CreatedOn);
    }

    [Fact]
    public void Activate_Should_Set_IsActive_To_True()
    {
        var name = UserName.Create("Test User");
        var email = global::Eleraki.SharedKernel.ValueObjects.Email.Create("test@example.com");
        var password = UserPassword.Create("Password123");

        var user = User.Create(name, email, password);
        user.Deactivate();
        user.Activate();

        Assert.True(user.IsActive);
    }

    [Fact]
    public void Deactivate_Should_Set_IsActive_To_False()
    {
        var name = UserName.Create("Test User");
        var email = global::Eleraki.SharedKernel.ValueObjects.Email.Create("test@example.com");
        var password = UserPassword.Create("Password123");

        var user = User.Create(name, email, password);
        user.Deactivate();

        Assert.False(user.IsActive);
    }

    [Fact]
    public void ChangeRole_Should_Update_Role()
    {
        var name = UserName.Create("Test User");
        var email = global::Eleraki.SharedKernel.ValueObjects.Email.Create("test@example.com");
        var password = UserPassword.Create("Password123");

        var user = User.Create(name, email, password);
        user.ChangeRole(UserRole.Admin);

        Assert.Equal(UserRole.Admin, user.Role);
    }
}
