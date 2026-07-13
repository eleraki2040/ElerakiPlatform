using Eleraki.SchoolManagementEngine.Application.DTOs;
using Eleraki.SchoolManagementEngine.Domain.Classes;
using Eleraki.SchoolManagementEngine.Domain.Repositories;
using MediatR;

namespace Eleraki.SchoolManagementEngine.Application.Queries;

public record GetClassByIdQuery(Guid Id) : IRequest<ClassDto?>;

public class GetClassByIdQueryHandler : IRequestHandler<GetClassByIdQuery, ClassDto?>
{
    private readonly IClassRepository _repository;

    public GetClassByIdQueryHandler(IClassRepository repository)
    {
        _repository = repository;
    }

    public async Task<ClassDto?> Handle(GetClassByIdQuery request, CancellationToken cancellationToken)
    {
        var classEntity = await _repository.GetByIdAsync(ClassId.From(request.Id), cancellationToken);
        if (classEntity is null) return null;

        return new ClassDto
        {
            Id = classEntity.Id.Value,
            Name = classEntity.Name,
            Grade = classEntity.Grade,
            HomeroomTeacherId = classEntity.HomeroomTeacherId.Value,
            MaxCapacity = classEntity.MaxCapacity,
            IsActive = classEntity.IsActive
        };
    }
}
