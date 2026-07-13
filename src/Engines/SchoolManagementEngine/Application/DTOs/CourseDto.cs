namespace Eleraki.SchoolManagementEngine.Application.DTOs;

public class CourseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Credits { get; set; }
    public Guid TeacherId { get; set; }
    public bool IsActive { get; set; }
}
