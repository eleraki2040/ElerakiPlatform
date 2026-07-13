using Eleraki.SchoolManagementEngine.Domain.Students;
using Eleraki.SchoolManagementEngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.SchoolManagementEngine.Application.Commands;

public record CreateStudentCommand(string FirstName, string LastName, string Email, DateTime DateOfBirth, string Address, string PhoneNumber) : IRequest<Result<StudentId>>;

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Result<StudentId>>
{
    private readonly IStudentRepository _repository;

    public CreateStudentCommandHandler(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<StudentId>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = Student.Create(request.FirstName, request.LastName, request.Email, request.DateOfBirth, request.Address, request.PhoneNumber);
        await _repository.AddAsync(student, cancellationToken);
        return Result<StudentId>.Success(student.Id);
    }
}
