using Microsoft.EntityFrameworkCore;
namespace EmployeeLeaveManagement.Entities
{
    public class Employee
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Position { get; set; }
        public string ReportsTo { get; set; } // the id of the user this user reports to
        public DateTime DateOfBirth { get; set; }
        public DateTime DateJoined { get; set; }
        public ICollection<LeaveAllocation> LeaveAllocations { get; set; }
        public ICollection<LeaveRequest> LeaveRequests { get; set; }
    }
}
