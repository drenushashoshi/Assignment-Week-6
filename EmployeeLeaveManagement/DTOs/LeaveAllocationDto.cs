using Microsoft.EntityFrameworkCore;
namespace EmployeeLeaveManagement.DTOs
{
    public class LeaveAllocationDto
{
    public string EmployeeId { get; set; }
    public int LeaveTypeId { get; set; }
    public int NumberOfDays { get; set; }
}
}