using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveManagement.Infrastructure.Repositories
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _context;

        public LeaveAllocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LeaveAllocation> GetByEmployeeIdAndTypeAsync(string employeeId, int leaveTypeId)
        {
            return await _context.LeaveAllocations
                .FirstOrDefaultAsync(la => la.EmployeeId == employeeId && la.LeaveTypeId == leaveTypeId);
        }

        public async Task<IEnumerable<LeaveAllocation>> GetByEmployeeIdAsync(string employeeId)
        {
            return await _context.LeaveAllocations
                .Where(la => la.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task AddAsync(LeaveAllocation leaveAllocation)
        {
            await _context.LeaveAllocations.AddAsync(leaveAllocation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LeaveAllocation leaveAllocation)
        {
            _context.LeaveAllocations.Update(leaveAllocation);
            await _context.SaveChangesAsync();
        }
    }
}