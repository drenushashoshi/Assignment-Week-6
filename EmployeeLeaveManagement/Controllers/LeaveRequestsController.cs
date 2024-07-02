using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeLeaveManagement.Controllers
{

    [ApiController]
[Route("api/[controller]")]
public class LeaveRequestsController : ControllerBase
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmailService _emailService;

    public LeaveRequestsController(ILeaveRequestRepository leaveRequestRepository, IEmployeeRepository employeeRepository, IEmailService emailService)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _employeeRepository = employeeRepository;
        _emailService = emailService;
    }

    [HttpPost]
    [Authorize(Roles = "Employee")]
    public async Task<IActionResult> CreateLeaveRequest([FromBody] LeaveRequestDto leaveRequestDto)
    {
        var employee = await _employeeRepository.GetByIdAsync(leaveRequestDto.RequestingEmployeeId);
        if (employee == null) return NotFound("Employee not found");

        var leaveRequest = new LeaveRequest
        {
            RequestingEmployeeId = leaveRequestDto.RequestingEmployeeId,
            StartDate = leaveRequestDto.StartDate,
            EndDate = leaveRequestDto.EndDate,
            LeaveTypeId = leaveRequestDto.LeaveTypeId,
            DateRequested = DateTime.UtcNow,
            RequestComments = leaveRequestDto.RequestComments
        };

        await _leaveRequestRepository.AddAsync(leaveRequest);
        await _emailService.SendLeaveRequestNotificationAsync(employee.ReportsTo, leaveRequest);

        return Ok(leaveRequest);
    }

    [HttpPost("{id}/approve")]
    [Authorize(Roles = "Lead")]
    public async Task<IActionResult> ApproveLeaveRequest(int id, [FromBody] ApproveLeaveRequestCommand command)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(id);
        if (leaveRequest == null) return NotFound("Leave request not found");

        leaveRequest.Approved = true;
        leaveRequest.DateActioned = DateTime.UtcNow;
        leaveRequest.ApprovedById = command.ApprovedById;

        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        return Ok(leaveRequest);
    }

}
}
