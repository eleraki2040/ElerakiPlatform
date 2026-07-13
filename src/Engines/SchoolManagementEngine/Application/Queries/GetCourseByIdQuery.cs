using Eleraki.SchoolManagementEngine.Application.DTOs;
using Eleraki.SchoolManagementEngine.Domain.Courses;
using Eleraki.SchoolManagementEngine.Domain.Repositories;
using MediatR;

namespace Eleraki.SchoolManagementEngine.Application.Queries;

public record GetCourseByIdQuery(Guid Id) : IRequest<CourseDto?>;

public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, CourseDto?>
{
    private readonly ICourseRepository _repository;

    public GetCourseByIdQueryHandler(ICourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<CourseDto?> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetByIdAsync(CourseId.From(request.Id), cancellationToken);
        if (course is null) return null;

        return new CourseDto
        {
            Id = course.Id.Value,
            Name = course.Name,
            Code = course.Code,
            Description = course.Description,
            Credits = course.Credits,
            TeacherId = course.TeacherId.Value,
            IsActive = course.IsActive
        };
    }
}
