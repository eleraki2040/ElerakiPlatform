namespace Eleraki.SchoolManagementEngine.Application.DTOs;

public class ClassDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Grade { get; set; } = string.Empty;
    public Guid HomeroomTeacherId { get; set; }
    public int MaxCapacity { get; set; }
    public bool IsActive { get; set; }
}
