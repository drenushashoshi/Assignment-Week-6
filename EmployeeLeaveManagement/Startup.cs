using Microsoft.EntityFrameworkCore;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 21))));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Configuration["Jwt:Issuer"],
                ValidAudience = Configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            };
        });

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
        services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<LeaveProcessingService>();

        services.AddControllers();
        services.AddHangfire(x => x.UseStorage(new MySqlStorage(Configuration.GetConnectionString("DefaultConnection"))));
        services.AddHangfireServer();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("EmployeePolicy", policy => policy.RequireRole("Employee"));
            options.AddPolicy("LeadPolicy", policy => policy.RequireRole("Lead"));
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHangfireDashboard();
        });

        RecurringJob.AddOrUpdate<LeaveProcessingService>(
            "ProcessMonthlyLeaveAllocations",
            service => service.ProcessMonthlyLeaveAllocations(),
            Cron.Monthly);

        RecurringJob.AddOrUpdate<LeaveProcessingService>(
            "ProcessAnnualLeaveReset",
            service => service.ProcessAnnualLeaveReset(),
            Cron.Yearly);
    }
}