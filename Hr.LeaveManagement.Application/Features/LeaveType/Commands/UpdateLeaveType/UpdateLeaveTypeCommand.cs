using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommand : IRequest<int>, IRequest, IRequest<Unit>
{
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}