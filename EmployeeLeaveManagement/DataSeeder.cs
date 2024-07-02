using Microsoft.EntityFrameworkCore;

public static class DataSeeder
{
    public static async Task SeedData(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();

            // Ensure the database is created.
            await context.Database.MigrateAsync();

            // Seed initial data.
            if (!context.LeaveTypes.Any())
            {
                context.LeaveTypes.AddRange(
                    new LeaveType { Id = 1, Name = "Annual", DefaultDays = 0, DateCreated = DateTime.UtcNow },
                    new LeaveType { Id = 2, Name = "Sick", DefaultDays = 20, DateCreated = DateTime.UtcNow },
                    new LeaveType { Id = 3, Name = "Replacement", DefaultDays = 0, DateCreated = DateTime.UtcNow },
                    new LeaveType { Id = 4, Name = "Unpaid", DefaultDays = 10, DateCreated = DateTime.UtcNow }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}

