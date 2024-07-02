using Microsoft.EntityFrameworkCore;
namespace EmployeeLeaveManagement.Infrastructure.Repositories.IRepositories
{
	public interface IEmployeeRepository
	{
		Task<Employee> GetByIdAsync(string id);
		Task<IEnumerable<Employee>> GetAllAsync();
		Task AddAsync(Employee employee);
		Task UpdateAsync(Employee employee);
		Task DeleteAsync(string id);
	}
}