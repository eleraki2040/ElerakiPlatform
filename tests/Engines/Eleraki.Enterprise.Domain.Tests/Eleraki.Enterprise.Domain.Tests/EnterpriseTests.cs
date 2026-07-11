using Eleraki.Enterprise.Domain;

namespace Eleraki.Enterprise.Domain.Tests;

public class EnterpriseTests
{
    [Fact]
    public void Create_Should_Return_Enterprise_With_Active_Status()
    {
        var code = EnterpriseCode.Create("ENT-001");
        var name = EnterpriseName.Create("Eleraki");

        var enterprise = Enterprise.Create(code, name);

        Assert.NotNull(enterprise);
        Assert.Equal(code, enterprise.Code);
        Assert.Equal(name, enterprise.Name);
        Assert.Equal(EnterpriseStatus.Active, enterprise.Status);
        Assert.NotEqual(default, enterprise.Id);
    }

    [Fact]
    public void Update_Should_Change_Name()
    {
        var code = EnterpriseCode.Create("ENT-001");
        var name = EnterpriseName.Create("Eleraki");
        var enterprise = Enterprise.Create(code, name);

        var newName = EnterpriseName.Create("Eleraki Platform");
        enterprise.Update(newName);

        Assert.Equal(newName, enterprise.Name);
    }

    [Theory]
    [InlineData(EnterpriseStatus.Active)]
    [InlineData(EnterpriseStatus.Inactive)]
    [InlineData(EnterpriseStatus.Suspended)]
    [InlineData(EnterpriseStatus.Archived)]
    public void Status_Transitions_Should_Work(EnterpriseStatus status)
    {
        var code = EnterpriseCode.Create("ENT-001");
        var name = EnterpriseName.Create("Eleraki");
        var enterprise = Enterprise.Create(code, name);

        Assert.Equal(EnterpriseStatus.Active, enterprise.Status);

        if (status == EnterpriseStatus.Inactive)
            enterprise.Deactivate();
        else if (status == EnterpriseStatus.Suspended)
            enterprise.Suspend();
        else if (status == EnterpriseStatus.Archived)
            enterprise.Archive();

        Assert.Equal(status, enterprise.Status);
    }
}
