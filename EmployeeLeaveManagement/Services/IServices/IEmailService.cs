using Microsoft.EntityFrameworkCore;

public interface IEmailServicenamespace EmployeeLeaveManagement.Services.IServices
{
    {
        Task SendEmailAsync(string leadId, LeaveRequest leaveRequest);
    }
}