using Microsoft.EntityFrameworkCore;
namespace EmployeeLeaveManagement.UseCases
{
    public class ApproveLeaveRequestCommand
    {
        public int LeaveRequestId { get; set; }
        public string ApprovedById { get; set; }
    }
}