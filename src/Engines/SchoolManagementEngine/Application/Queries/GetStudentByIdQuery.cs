using Eleraki.SchoolManagementEngine.Application.DTOs;
using Eleraki.SchoolManagementEngine.Domain.Students;
using Eleraki.SchoolManagementEngine.Domain.Repositories;
using MediatR;

namespace Eleraki.SchoolManagementEngine.Application.Queries;

public record GetStudentByIdQuery(Guid Id) : IRequest<StudentDto?>;

public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentDto?>
{
    private readonly IStudentRepository _repository;

    public GetStudentByIdQueryHandler(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<StudentDto?> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await _repository.GetByIdAsync(StudentId.From(request.Id), cancellationToken);
        if (student is null) return null;

        return new StudentDto
        {
            Id = student.Id.Value,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            DateOfBirth = student.DateOfBirth,
            Address = student.Address,
            PhoneNumber = student.PhoneNumber,
            EnrollmentDate = student.EnrollmentDate,
            IsActive = student.IsActive
        };
    }
}
