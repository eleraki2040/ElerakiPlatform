namespace Eleraki.HREngine.Application.DTOs;

public class AttendanceDto
{
    public Guid Id { get; set; }
    public string EmployeeId { get; set; } = string.Empty;
    public DateTime AttendanceDate { get; set; }
    public DateTime? CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
}
