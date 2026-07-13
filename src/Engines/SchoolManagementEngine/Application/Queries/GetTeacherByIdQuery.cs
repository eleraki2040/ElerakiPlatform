using Eleraki.SchoolManagementEngine.Application.DTOs;
using Eleraki.SchoolManagementEngine.Domain.Teachers;
using Eleraki.SchoolManagementEngine.Domain.Repositories;
using MediatR;

namespace Eleraki.SchoolManagementEngine.Application.Queries;

public record GetTeacherByIdQuery(Guid Id) : IRequest<TeacherDto?>;

public class GetTeacherByIdQueryHandler : IRequestHandler<GetTeacherByIdQuery, TeacherDto?>
{
    private readonly ITeacherRepository _repository;

    public GetTeacherByIdQueryHandler(ITeacherRepository repository)
    {
        _repository = repository;
    }

    public async Task<TeacherDto?> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _repository.GetByIdAsync(TeacherId.From(request.Id), cancellationToken);
        if (teacher is null) return null;

        return new TeacherDto
        {
            Id = teacher.Id.Value,
            FirstName = teacher.FirstName,
            LastName = teacher.LastName,
            Email = teacher.Email,
            PhoneNumber = teacher.PhoneNumber,
            Specialization = teacher.Specialization,
            HireDate = teacher.HireDate,
            IsActive = teacher.IsActive
        };
    }
}
