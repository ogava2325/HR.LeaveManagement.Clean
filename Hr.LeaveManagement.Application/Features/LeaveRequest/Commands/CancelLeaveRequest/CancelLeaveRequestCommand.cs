using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommand : IRequest<Unit>
{
    public int Id { get; set; }
}