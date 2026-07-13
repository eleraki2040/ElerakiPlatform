using Eleraki.SchoolManagementEngine.Domain.Courses;
using Eleraki.SchoolManagementEngine.Domain.Repositories;
using Eleraki.SchoolManagementEngine.Domain.Teachers;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.SchoolManagementEngine.Application.Commands;

public record CreateCourseCommand(string Name, string Code, string Description, int Credits, Guid TeacherId) : IRequest<Result<CourseId>>;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Result<CourseId>>
{
    private readonly ICourseRepository _repository;
    private readonly ITeacherRepository _teacherRepository;

    public CreateCourseCommandHandler(ICourseRepository repository, ITeacherRepository teacherRepository)
    {
        _repository = repository;
        _teacherRepository = teacherRepository;
    }

    public async Task<Result<CourseId>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetByIdAsync(TeacherId.From(request.TeacherId), cancellationToken);
        if (teacher is null)
            return Result<CourseId>.Failure(Error.NotFound("Teacher not found."));

        var course = Course.Create(request.Name, request.Code, request.Description, request.Credits, TeacherId.From(request.TeacherId));
        await _repository.AddAsync(course, cancellationToken);
        return Result<CourseId>.Success(course.Id);
    }
}
