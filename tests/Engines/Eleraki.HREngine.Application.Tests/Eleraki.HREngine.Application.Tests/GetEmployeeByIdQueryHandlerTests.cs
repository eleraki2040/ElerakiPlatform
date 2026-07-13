using Eleraki.HREngine.Application;
using Eleraki.HREngine.Application.Queries;
using Eleraki.HREngine.Domain;
using Eleraki.HREngine.Domain.Repositories;
using FluentAssertions;
using MediatR;
using Moq;

namespace Eleraki.HREngine.Application.Tests;

public class GetEmployeeByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_Should_Return_EmployeeDto_When_Employee_Exists()
    {
        var employee = Employee.Create(
            "John", "Doe", "john@eleraki.com", "555-0100",
            new DateTime(1990, 5, 15), "DEPT-001", "POS-001");

        var mockRepository = new Mock<IEmployeeRepository>();
        mockRepository.Setup(r => r.GetByIdAsync(employee.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(employee);
        var handler = new GetEmployeeByIdQueryHandler(mockRepository.Object);
        var query = new GetEmployeeByIdQuery(employee.Id.Value);

        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result!.Id.Should().Be(employee.Id.Value);
        result.FirstName.Should().Be("John");
        result.LastName.Should().Be("Doe");
        result.Email.Should().Be("john@eleraki.com");
        result.Phone.Should().Be("555-0100");
        result.DepartmentId.Should().Be("DEPT-001");
        result.PositionId.Should().Be("POS-001");
        result.Status.Should().Be("Active");
    }

    [Fact]
    public async Task Handle_Should_Return_Null_When_Employee_Not_Found()
    {
        var mockRepository = new Mock<IEmployeeRepository>();
        mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<EmployeeId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Employee?)null);
        var handler = new GetEmployeeByIdQueryHandler(mockRepository.Object);
        var query = new GetEmployeeByIdQuery(Guid.NewGuid());

        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().BeNull();
    }

    [Fact]
    public async Task Handle_Should_Call_Repository_With_Correct_EmployeeId()
    {
        var employeeId = EmployeeId.New();
        var mockRepository = new Mock<IEmployeeRepository>();
        mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<EmployeeId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Employee?)null);
        var handler = new GetEmployeeByIdQueryHandler(mockRepository.Object);
        var query = new GetEmployeeByIdQuery(employeeId.Value);

        await handler.Handle(query, CancellationToken.None);

        mockRepository.Verify(r => r.GetByIdAsync(employeeId, It.IsAny<CancellationToken>()), Times.Once);
    }
}
