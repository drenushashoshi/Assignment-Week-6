using Microsoft.EntityFrameworkCore;
namespace EmployeeLeaveManagement.UseCases
{
    public class GetLeaveAllocationsQuery
    {
        public string EmployeeId { get; set; }
    }
}