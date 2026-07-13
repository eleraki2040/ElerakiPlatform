using Eleraki.HREngine.Application.DTOs;
using Eleraki.HREngine.Domain;
using Eleraki.HREngine.Domain.Repositories;
using MediatR;

namespace Eleraki.HREngine.Application.Queries;

public record GetDepartmentByIdQuery(Guid Id) : IRequest<DepartmentDto?>;

public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, DepartmentDto?>
{
    private readonly IDepartmentRepository _repository;

    public GetDepartmentByIdQueryHandler(IDepartmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<DepartmentDto?> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        var department = await _repository.GetByIdAsync(DepartmentId.From(request.Id), cancellationToken);

        if (department is null)
            return null;

        return new DepartmentDto
        {
            Id = department.Id.Value,
            Name = department.Name,
            Description = department.Description,
            Status = department.Status.ToString()
        };
    }
}
