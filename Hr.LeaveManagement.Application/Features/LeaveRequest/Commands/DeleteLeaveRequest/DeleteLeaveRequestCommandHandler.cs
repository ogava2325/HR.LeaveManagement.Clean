using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Application.Exceptions;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommandHandler(
    IMapper mapper,
    ILeaveRequestRepository leaveRequestRepository)
    : IRequestHandler<DeleteLeaveRequestCommand>
{
    public async Task Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        await leaveRequestRepository.DeleteAsync(leaveRequest);
    }
}