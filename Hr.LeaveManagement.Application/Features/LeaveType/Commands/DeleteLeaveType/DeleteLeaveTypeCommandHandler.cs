using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Application.Exceptions;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
    : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // retrieve domain entity object
        var leaveTypeToDelete = await leaveTypeRepository.GetByIdAsync(request.Id);

        // verify that record exists
        if (leaveTypeToDelete == null)
        {
            throw new NotFoundException(nameof(LeaveType), request.Id);
        }

        // remove from database
        await leaveTypeRepository.DeleteAsync(leaveTypeToDelete);

        // return record id
        return Unit.Value;
    }
}