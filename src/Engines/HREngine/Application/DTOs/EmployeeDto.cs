namespace Eleraki.HREngine.Application.DTOs;

public class EmployeeDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string DepartmentId { get; set; } = string.Empty;
    public string PositionId { get; set; } = string.Empty;
    public string? SalaryId { get; set; }
    public string Status { get; set; } = string.Empty;
}
