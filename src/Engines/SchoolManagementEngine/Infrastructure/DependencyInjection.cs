using Eleraki.SchoolManagementEngine.Domain.Repositories;
using Eleraki.SchoolManagementEngine.Domain.Students;
using Eleraki.SchoolManagementEngine.Domain.Teachers;
using Eleraki.SchoolManagementEngine.Domain.Classes;
using Eleraki.SchoolManagementEngine.Domain.Courses;
using Eleraki.SchoolManagementEngine.Infrastructure.Persistence;
using Eleraki.SchoolManagementEngine.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eleraki.SchoolManagementEngine.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddSchoolManagementEngineInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SchoolDbContext>(options =>
            options.UseInMemoryDatabase("SchoolManagementEngine"));

        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<IClassRepository, ClassRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();

        return services;
    }
}
