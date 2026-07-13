using Eleraki.HREngine.Application;
using Eleraki.HREngine.Application.Commands;
using Eleraki.HREngine.Domain;
using Eleraki.HREngine.Domain.Repositories;
using FluentAssertions;
using MediatR;
using Moq;

namespace Eleraki.HREngine.Application.Tests;

public class CreateDepartmentCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_Department_And_Return_Success_Result()
    {
        var mockRepository = new Mock<IDepartmentRepository>();
        var handler = new CreateDepartmentCommandHandler(mockRepository.Object);
        var command = new CreateDepartmentCommand("Engineering", "Technology division");

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBe(Guid.Empty);
        mockRepository.Verify(r => r.AddAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Create_Department_Without_Description()
    {
        var mockRepository = new Mock<IDepartmentRepository>();
        var handler = new CreateDepartmentCommandHandler(mockRepository.Object);
        var command = new CreateDepartmentCommand("Engineering");

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        mockRepository.Verify(r => r.AddAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Persist_Department_With_Correct_Properties()
    {
        Department? capturedDepartment = null;
        var mockRepository = new Mock<IDepartmentRepository>();
        mockRepository.Setup(r => r.AddAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>()))
            .Callback<Department, CancellationToken>((d, _) => capturedDepartment = d);
        var handler = new CreateDepartmentCommandHandler(mockRepository.Object);
        var command = new CreateDepartmentCommand("Engineering", "Technology division");

        await handler.Handle(command, CancellationToken.None);

        capturedDepartment.Should().NotBeNull();
        capturedDepartment!.Name.Should().Be("Engineering");
        capturedDepartment.Description.Should().Be("Technology division");
        capturedDepartment.Status.Should().Be(DepartmentStatus.Active);
    }
}
