using Microsoft.EntityFrameworkCore;
namespace EmployeeLeaveManagement.Services
{

    public class LeaveProcessingService
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public LeaveProcessingService(ILeaveAllocationRepository leaveAllocationRepository, IEmployeeRepository employeeRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task ProcessMonthlyLeaveAllocations()
        {
            var employees = await _employeeRepository.GetAllAsync();
            foreach (var employee in employees)
            {
                // Allocate 2 days of annual leave each month
                var leaveAllocation = await _leaveAllocationRepository.GetByEmployeeAndType(employee.Id, 1); // 1 is the Id for Annual leave type
                if (leaveAllocation != null)
                {
                    leaveAllocation.NumberOfDays += 2;
                    await _leaveAllocationRepository.UpdateAsync(leaveAllocation);
                }
                else
                {
                    await _leaveAllocationRepository.CreateAsync(new LeaveAllocation
                    {
                        EmployeeId = employee.Id,
                        LeaveTypeId = 1, // Annual leave type
                        NumberOfDays = 2,
                        DateCreated = DateTime.UtcNow,
                        Period = DateTime.UtcNow.Year
                    });
                }
            }
        }

        public async Task ProcessAnnualLeaveReset()
        {
            var employees = await _employeeRepository.GetAllAsync();
            foreach (var employee in employees)
            {
                var leaveAllocation = await _leaveAllocationRepository.GetByEmployeeAndType(employee.Id, 1); // 1 is the Id for Annual leave type
                if (leaveAllocation != null)
                {
                    leaveAllocation.NumberOfDays = 0;
                    await _leaveAllocationRepository.UpdateAsync(leaveAllocation);
                }
            }
        }
    }
}