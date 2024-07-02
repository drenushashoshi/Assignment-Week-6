using Microsoft.EntityFrameworkCore;
namespace EmployeeLeaveManagement.UseCases
{
    public class GetLeaveRequestsQuery
    {
        public string EmployeeId { get; set; }
    }
}