using Eleraki.SchoolManagementEngine.Application;
using Eleraki.SchoolManagementEngine.Application.Commands;
using Eleraki.SchoolManagementEngine.Domain;
using Eleraki.SchoolManagementEngine.Domain.Courses;
using Eleraki.SchoolManagementEngine.Domain.Repositories;
using Eleraki.SchoolManagementEngine.Domain.Teachers;
using Eleraki.SharedKernel.Primitives;
using FluentAssertions;
using MediatR;
using Moq;

namespace Eleraki.SchoolManagementEngine.Application.Tests;

public class CreateCourseCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_Course_And_Return_Success_Result()
    {
        var mockCourseRepository = new Mock<ICourseRepository>();
        var mockTeacherRepository = new Mock<ITeacherRepository>();
        var teacherId = TeacherId.New();
        mockTeacherRepository.Setup(r => r.GetByIdAsync(teacherId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Teacher.Create("Jane", "Smith", "jane@eleraki.com", "555-0200", "Mathematics"));

        var handler = new CreateCourseCommandHandler(mockCourseRepository.Object, mockTeacherRepository.Object);
        var command = new CreateCourseCommand("Mathematics", "MATH101", "Advanced Mathematics", 4, teacherId.Value);

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBe(Guid.Empty);
        mockCourseRepository.Verify(r => r.AddAsync(It.IsAny<Course>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_Teacher_Not_Found()
    {
        var mockCourseRepository = new Mock<ICourseRepository>();
        var mockTeacherRepository = new Mock<ITeacherRepository>();
        var teacherId = TeacherId.New();
        mockTeacherRepository.Setup(r => r.GetByIdAsync(teacherId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Teacher?)null);

        var handler = new CreateCourseCommandHandler(mockCourseRepository.Object, mockTeacherRepository.Object);
        var command = new CreateCourseCommand("Mathematics", "MATH101", "Advanced Mathematics", 4, teacherId.Value);

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
    }
}
