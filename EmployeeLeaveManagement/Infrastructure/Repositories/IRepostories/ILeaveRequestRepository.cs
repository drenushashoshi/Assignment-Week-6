using Microsoft.EntityFrameworkCore;
namespace EmployeeLeaveManagement.Infrastructure.Repositories.IRepositories
{
    public interface ILeaveRequestRepository
    {
        Task<LeaveRequest> GetByIdAsync(int id);
        Task<IEnumerable<LeaveRequest>> GetByEmployeeIdAsync(string employeeId);
        Task AddAsync(LeaveRequest leaveRequest);
        Task UpdateAsync(LeaveRequest leaveRequest);
        Task DeleteAsync(int id);
    }
}