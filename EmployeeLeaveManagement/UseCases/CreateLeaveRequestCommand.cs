
using Microsoft.EntityFrameworkCore;
namespace EmployeeLeaveManagement.UseCases
{
    public class CreateLeaveRequestCommand
    {
        public LeaveRequestDto LeaveRequest { get; set; }
    }
}