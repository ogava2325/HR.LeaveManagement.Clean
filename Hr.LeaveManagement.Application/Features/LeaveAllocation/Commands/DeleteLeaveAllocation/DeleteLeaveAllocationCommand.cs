using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommand : IRequest
{
    public int Id { get; set; }
}