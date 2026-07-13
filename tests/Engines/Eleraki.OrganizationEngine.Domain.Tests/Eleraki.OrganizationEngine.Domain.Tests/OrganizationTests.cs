using Eleraki.OrganizationEngine.Domain.Organizations;
using Eleraki.OrganizationEngine.Domain.ValueObjects;

namespace Eleraki.OrganizationEngine.Domain.Tests;

public class OrganizationTests
{
    [Fact]
    public void Create_Should_Return_Organization_With_Active_Status()
    {
        var name = OrganizationName.Create("Eleraki");
        var code = OrganizationCode.Create("ORG-001");

        var organization = Organization.Create(name.Value, code.Value);

        Assert.NotNull(organization);
        Assert.Equal(name.Value, organization.Name.Value);
        Assert.Equal(code.Value, organization.Code.Value);
        Assert.Equal(OrganizationStatus.Active, organization.Status);
        Assert.NotEqual(default, organization.Id);
    }

    [Fact]
    public void Create_Should_Raise_OrganizationCreatedDomainEvent()
    {
        var name = OrganizationName.Create("Eleraki");
        var code = OrganizationCode.Create("ORG-001");

        var organization = Organization.Create(name.Value, code.Value);

        Assert.Contains(organization.DomainEvents, e => e.GetType().Name == "OrganizationCreatedDomainEvent");
    }

    [Fact]
    public void Update_Should_Change_Name()
    {
        var name = OrganizationName.Create("Eleraki");
        var code = OrganizationCode.Create("ORG-001");
        var organization = Organization.Create(name.Value, code.Value);

        var newName = "Eleraki Platform";
        organization.Update(newName);

        Assert.Equal(newName, organization.Name.Value);
    }

    [Theory]
    [InlineData(OrganizationStatus.Active)]
    [InlineData(OrganizationStatus.Inactive)]
    [InlineData(OrganizationStatus.Archived)]
    public void Status_Transitions_Should_Work(OrganizationStatus status)
    {
        var name = OrganizationName.Create("Eleraki");
        var code = OrganizationCode.Create("ORG-001");
        var organization = Organization.Create(name.Value, code.Value);

        Assert.Equal(OrganizationStatus.Active, organization.Status);

        if (status == OrganizationStatus.Inactive)
            organization.Deactivate();
        else if (status == OrganizationStatus.Archived)
            organization.Archive();

        Assert.Equal(status, organization.Status);
    }

    [Fact]
    public void Create_Should_Throw_When_Name_Is_Null()
    {
        Assert.Throws<ArgumentException>(() => Organization.Create(null!, "ORG-001"));
    }

    [Fact]
    public void Create_Should_Throw_When_Code_Is_Null()
    {
        Assert.Throws<ArgumentException>(() => Organization.Create("Eleraki", null!));
    }

    [Fact]
    public void OrganizationCode_Should_Be_Case_Insensitive()
    {
        var code1 = OrganizationCode.Create("org-001");
        var code2 = OrganizationCode.Create("ORG-001");

        Assert.Equal(code1.Value, code2.Value);
    }

    [Fact]
    public void OrganizationCode_Should_Trim_Whitespace()
    {
        var code = OrganizationCode.Create("  ORG-001  ");

        Assert.Equal("ORG-001", code.Value);
    }
}
