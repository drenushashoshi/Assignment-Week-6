using Microsoft.EntityFrameworkCore;
namespace EmployeeLeaveManagement.DTOs
{
    public class LeaveTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DefaultDays { get; set; }
    }
}