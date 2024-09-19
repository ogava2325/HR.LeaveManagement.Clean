using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Application.Exceptions;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new UpdateLeaveTypeCommandValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid Leave type", validationResult);
        }

        // convert to domain entity object
        var leaveTypeToUpdate = mapper.Map<Domain.Entities.LeaveType>(request);

        // add to database
        await leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);

        // return Unit value
        return Unit.Value;
    }
}