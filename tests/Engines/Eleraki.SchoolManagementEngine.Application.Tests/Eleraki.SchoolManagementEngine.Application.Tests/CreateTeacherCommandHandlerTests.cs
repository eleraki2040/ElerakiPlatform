using Eleraki.SchoolManagementEngine.Application;
using Eleraki.SchoolManagementEngine.Application.Commands;
using Eleraki.SchoolManagementEngine.Domain;
using Eleraki.SchoolManagementEngine.Domain.Repositories;
using Eleraki.SchoolManagementEngine.Domain.Teachers;
using Eleraki.SharedKernel.Primitives;
using FluentAssertions;
using MediatR;
using Moq;

namespace Eleraki.SchoolManagementEngine.Application.Tests;

public class CreateTeacherCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_Teacher_And_Return_Success_Result()
    {
        var mockRepository = new Mock<ITeacherRepository>();
        var handler = new CreateTeacherCommandHandler(mockRepository.Object);
        var command = new CreateTeacherCommand("Jane", "Smith", "jane@eleraki.com", "555-0200", "Mathematics");

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBe(Guid.Empty);
        mockRepository.Verify(r => r.AddAsync(It.IsAny<Teacher>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Persist_Teacher_With_Correct_Properties()
    {
        Teacher? capturedTeacher = null;
        var mockRepository = new Mock<ITeacherRepository>();
        mockRepository.Setup(r => r.AddAsync(It.IsAny<Teacher>(), It.IsAny<CancellationToken>()))
            .Callback<Teacher, CancellationToken>((t, _) => capturedTeacher = t);
        var handler = new CreateTeacherCommandHandler(mockRepository.Object);
        var command = new CreateTeacherCommand("Jane", "Smith", "jane@eleraki.com", "555-0200", "Mathematics");

        await handler.Handle(command, CancellationToken.None);

        capturedTeacher.Should().NotBeNull();
        capturedTeacher!.FirstName.Should().Be("Jane");
        capturedTeacher.LastName.Should().Be("Smith");
        capturedTeacher.Email.Should().Be("jane@eleraki.com");
        capturedTeacher.Specialization.Should().Be("Mathematics");
        capturedTeacher.IsActive.Should().BeTrue();
    }
}
