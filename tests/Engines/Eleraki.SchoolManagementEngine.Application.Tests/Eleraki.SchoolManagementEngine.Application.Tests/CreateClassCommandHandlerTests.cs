using Eleraki.SchoolManagementEngine.Application;
using Eleraki.SchoolManagementEngine.Application.Commands;
using Eleraki.SchoolManagementEngine.Domain;
using Eleraki.SchoolManagementEngine.Domain.Classes;
using Eleraki.SchoolManagementEngine.Domain.Repositories;
using Eleraki.SchoolManagementEngine.Domain.Teachers;
using Eleraki.SharedKernel.Primitives;
using FluentAssertions;
using MediatR;
using Moq;

namespace Eleraki.SchoolManagementEngine.Application.Tests;

public class CreateClassCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_Class_And_Return_Success_Result()
    {
        var mockClassRepository = new Mock<IClassRepository>();
        var mockTeacherRepository = new Mock<ITeacherRepository>();
        var teacherId = TeacherId.New();
        mockTeacherRepository.Setup(r => r.GetByIdAsync(teacherId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Teacher.Create("Jane", "Smith", "jane@eleraki.com", "555-0200", "Mathematics"));

        var handler = new CreateClassCommandHandler(mockClassRepository.Object, mockTeacherRepository.Object);
        var command = new CreateClassCommand("Class A", "10th", teacherId.Value, 30);

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBe(Guid.Empty);
        mockClassRepository.Verify(r => r.AddAsync(It.IsAny<Class>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_Teacher_Not_Found()
    {
        var mockClassRepository = new Mock<IClassRepository>();
        var mockTeacherRepository = new Mock<ITeacherRepository>();
        var teacherId = TeacherId.New();
        mockTeacherRepository.Setup(r => r.GetByIdAsync(teacherId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Teacher?)null);

        var handler = new CreateClassCommandHandler(mockClassRepository.Object, mockTeacherRepository.Object);
        var command = new CreateClassCommand("Class A", "10th", teacherId.Value, 30);

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
    }
}
