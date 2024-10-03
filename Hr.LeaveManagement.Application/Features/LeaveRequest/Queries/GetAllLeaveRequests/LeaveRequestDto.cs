using Hr.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using Hr.LeaveManagement.Application.Models.Identity;

namespace Hr.LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;

public class LeaveRequestDto
{
    public string RequestingEmployeeId { get; set; }
    public LeaveTypeDto LeaveType { get; set; }
    public DateTime DateRequested { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool? Approved { get; set; }
}