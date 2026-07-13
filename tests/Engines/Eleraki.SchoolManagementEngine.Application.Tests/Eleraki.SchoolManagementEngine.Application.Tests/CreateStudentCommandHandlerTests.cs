using Eleraki.SchoolManagementEngine.Application;
using Eleraki.SchoolManagementEngine.Application.Commands;
using Eleraki.SchoolManagementEngine.Domain;
using Eleraki.SchoolManagementEngine.Domain.Repositories;
using Eleraki.SchoolManagementEngine.Domain.Students;
using Eleraki.SharedKernel.Primitives;
using FluentAssertions;
using MediatR;
using Moq;

namespace Eleraki.SchoolManagementEngine.Application.Tests;

public class CreateStudentCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_Student_And_Return_Success_Result()
    {
        var mockRepository = new Mock<IStudentRepository>();
        var handler = new CreateStudentCommandHandler(mockRepository.Object);
        var command = new CreateStudentCommand("John", "Doe", "john@eleraki.com", new DateTime(2000, 1, 1), "123 Main St", "555-0100");

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBe(Guid.Empty);
        mockRepository.Verify(r => r.AddAsync(It.IsAny<Student>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Persist_Student_With_Correct_Properties()
    {
        Student? capturedStudent = null;
        var mockRepository = new Mock<IStudentRepository>();
        mockRepository.Setup(r => r.AddAsync(It.IsAny<Student>(), It.IsAny<CancellationToken>()))
            .Callback<Student, CancellationToken>((s, _) => capturedStudent = s);
        var handler = new CreateStudentCommandHandler(mockRepository.Object);
        var command = new CreateStudentCommand("John", "Doe", "john@eleraki.com", new DateTime(2000, 1, 1), "123 Main St", "555-0100");

        await handler.Handle(command, CancellationToken.None);

        capturedStudent.Should().NotBeNull();
        capturedStudent!.FirstName.Should().Be("John");
        capturedStudent.LastName.Should().Be("Doe");
        capturedStudent.Email.Should().Be("john@eleraki.com");
        capturedStudent.IsActive.Should().BeTrue();
    }
}
