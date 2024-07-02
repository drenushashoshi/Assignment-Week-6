using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveManagement.BackgroundJobs
{
    public class LeaveProcessingService
{
	private readonly IEmployeeRepository _employeeRepository;
	private readonly ILeaveAllocationRepository _leaveAllocationRepository;

	public LeaveProcessingService(IEmployeeRepository employeeRepository, ILeaveAllocationRepository leaveAllocationRepository)
	{
		_employeeRepository = employeeRepository;
		_leaveAllocationRepository = leaveAllocationRepository;
	}

	public async Task ProcessMonthlyLeaveAllocations()
	{
		var employees = await _employeeRepository.GetAllAsync();
		foreach (var employee in employees)
		{
			var allocation = await _leaveAllocationRepository.GetByEmployeeIdAndTypeAsync(employee.Id, 1); // Annual Leave Type
			if (allocation != null)
			{
				allocation.NumberOfDays += 2; // Add 2 days each month
				await _leaveAllocationRepository.UpdateAsync(allocation);
			}
			else
			{

				allocation = new LeaveAllocation
				{
					EmployeeId = employee.Id,
					LeaveTypeId = 1, // Annual Leave Type
					NumberOfDays = 2, // Initial 2 days for the first month
					DateCreated = DateTime.UtcNow,
					Period = DateTime.UtcNow.Year
				};
				await _leaveAllocationRepository.AddAsync(allocation);
			}
		}
	}

	public async Task ProcessAnnualLeaveReset()
	{
		var employees = await _employeeRepository.GetAllAsync();
		foreach (var employee in employees)
		{
			var allocations = await _leaveAllocationRepository.GetByEmployeeIdAsync(employee.Id);

			foreach (var allocation in allocations)
			{
				if (allocation.LeaveTypeId == 1) // Annual Leave Type
				{
					allocation.NumberOfDays = 0; // Reset annual leave
				}

				await _leaveAllocationRepository.UpdateAsync(allocation);
			}
		}
	}
}
}