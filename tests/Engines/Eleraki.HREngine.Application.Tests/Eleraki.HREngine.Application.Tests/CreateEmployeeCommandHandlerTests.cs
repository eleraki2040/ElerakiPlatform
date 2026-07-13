using Eleraki.HREngine.Application;
using Eleraki.HREngine.Application.Commands;
using Eleraki.HREngine.Domain;
using Eleraki.HREngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using FluentAssertions;
using MediatR;
using Moq;

namespace Eleraki.HREngine.Application.Tests;

public class CreateEmployeeCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_Employee_And_Return_Success_Result()
    {
        var mockRepository = new Mock<IEmployeeRepository>();
        var handler = new CreateEmployeeCommandHandler(mockRepository.Object);
        var command = new CreateEmployeeCommand(
            "John", "Doe", "john@eleraki.com", "555-0100",
            new DateTime(1990, 5, 15), "DEPT-001", "POS-001");

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBe(Guid.Empty);
        mockRepository.Verify(r => r.AddAsync(It.IsAny<Employee>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Persist_Employee_With_Correct_Properties()
    {
        Employee? capturedEmployee = null;
        var mockRepository = new Mock<IEmployeeRepository>();
        mockRepository.Setup(r => r.AddAsync(It.IsAny<Employee>(), It.IsAny<CancellationToken>()))
            .Callback<Employee, CancellationToken>((e, _) => capturedEmployee = e);
        var handler = new CreateEmployeeCommandHandler(mockRepository.Object);
        var command = new CreateEmployeeCommand(
            "John", "Doe", "john@eleraki.com", "555-0100",
            new DateTime(1990, 5, 15), "DEPT-001", "POS-001");

        await handler.Handle(command, CancellationToken.None);

        capturedEmployee.Should().NotBeNull();
        capturedEmployee!.FirstName.Should().Be("John");
        capturedEmployee.LastName.Should().Be("Doe");
        capturedEmployee.Email.Should().Be("john@eleraki.com");
        capturedEmployee.Status.Should().Be(EmployeeStatus.Active);
    }
}
