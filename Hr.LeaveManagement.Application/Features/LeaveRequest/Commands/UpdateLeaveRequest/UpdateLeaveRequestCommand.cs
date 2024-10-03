using Hr.LeaveManagement.Application.Features.LeaveRequest.Shared;
using Hr.LeaveManagement.Domain.Entities;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommand : BaseLeaveRequest, IRequest<Unit>
{
    public int Id { get; set; }
    public string RequestComments { get; set; } = string.Empty;
    public bool Cancelled { get; set; }
}