namespace Eleraki.HREngine.Application.DTOs;

public class PositionDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string DepartmentId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
