using Eleraki.SchoolManagementEngine.Domain.Classes;
using Eleraki.SchoolManagementEngine.Domain.Repositories;
using Eleraki.SchoolManagementEngine.Domain.Teachers;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.SchoolManagementEngine.Application.Commands;

public record CreateClassCommand(string Name, string Grade, Guid HomeroomTeacherId, int MaxCapacity) : IRequest<Result<ClassId>>;

public class CreateClassCommandHandler : IRequestHandler<CreateClassCommand, Result<ClassId>>
{
    private readonly IClassRepository _repository;
    private readonly ITeacherRepository _teacherRepository;

    public CreateClassCommandHandler(IClassRepository repository, ITeacherRepository teacherRepository)
    {
        _repository = repository;
        _teacherRepository = teacherRepository;
    }

    public async Task<Result<ClassId>> Handle(CreateClassCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetByIdAsync(TeacherId.From(request.HomeroomTeacherId), cancellationToken);
        if (teacher is null)
            return Result<ClassId>.Failure(Error.NotFound("Teacher not found."));

        var classEntity = Class.Create(request.Name, request.Grade, TeacherId.From(request.HomeroomTeacherId), request.MaxCapacity);
        await _repository.AddAsync(classEntity, cancellationToken);
        return Result<ClassId>.Success(classEntity.Id);
    }
}
