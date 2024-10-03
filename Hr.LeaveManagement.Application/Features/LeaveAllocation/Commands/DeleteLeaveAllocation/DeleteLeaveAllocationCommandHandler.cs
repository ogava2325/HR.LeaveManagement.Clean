using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Application.Exceptions;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommandHandler(
    ILeaveAllocationRepository leaveAllocationRepository,
    IMapper mapper) : IRequestHandler<DeleteLeaveAllocationCommand>
{
    public async Task Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var leaveAllocation = await leaveAllocationRepository.GetByIdAsync(request.Id);

        if (leaveAllocation == null)
            throw new NotFoundException(nameof(LeaveAllocation), request.Id);

        await leaveAllocationRepository.DeleteAsync(leaveAllocation);
    }
}