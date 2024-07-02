using Microsoft.EntityFrameworkCore;
namespace EmployeeLeaveManagement.Infrastructure.Repositories.IRepositories
{
    public interface ILeaveAllocationRepository
    {
        Task<LeaveAllocation> GetByEmployeeIdAndTypeAsync(string employeeId, int leaveTypeId);
        Task<IEnumerable<LeaveAllocation>> GetByEmployeeIdAsync(string employeeId);
        Task UpdateAsync(LeaveAllocation leaveAllocation);
        Task AddAsync(LeaveAllocation leaveAllocation);
    }
}