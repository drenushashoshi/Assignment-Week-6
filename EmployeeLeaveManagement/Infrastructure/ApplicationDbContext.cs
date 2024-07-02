using Microsoft.EntityFrameworkCore;
namespace EmployeeLeaveManagement.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Seed leave types
            modelBuilder.Entity<LeaveType>().HasData(
                new LeaveType { Id = 1, Name = "Annual", DefaultDays = 0, DateCreated = DateTime.UtcNow },
                new LeaveType { Id = 2, Name = "Sick", DefaultDays = 20, DateCreated = DateTime.UtcNow },
                new LeaveType { Id = 3, Name = "Replacement", DefaultDays = 0, DateCreated = DateTime.UtcNow },
                new LeaveType { Id = 4, Name = "Unpaid", DefaultDays = 10, DateCreated = DateTime.UtcNow }
            );
        }
    }
}
