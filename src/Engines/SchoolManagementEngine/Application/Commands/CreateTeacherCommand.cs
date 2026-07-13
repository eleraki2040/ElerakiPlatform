using Eleraki.SchoolManagementEngine.Domain.Teachers;
using Eleraki.SchoolManagementEngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.SchoolManagementEngine.Application.Commands;

public record CreateTeacherCommand(string FirstName, string LastName, string Email, string PhoneNumber, string Specialization) : IRequest<Result<TeacherId>>;

public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, Result<TeacherId>>
{
    private readonly ITeacherRepository _repository;

    public CreateTeacherCommandHandler(ITeacherRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<TeacherId>> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = Teacher.Create(request.FirstName, request.LastName, request.Email, request.PhoneNumber, request.Specialization);
        await _repository.AddAsync(teacher, cancellationToken);
        return Result<TeacherId>.Success(teacher.Id);
    }
}
