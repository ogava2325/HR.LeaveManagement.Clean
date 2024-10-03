using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Application.Exceptions;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler(
    ILeaveAllocationRepository leaveAllocationRepository,
    ILeaveTypeRepository leaveTypeRepository,
    IMapper mapper) :
    IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationCommandValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid LeaveAllocation Request", validationResult);
        }

        var leaveAllocations = mapper.Map<Domain.Entities.LeaveAllocation>(request);
        await leaveAllocationRepository.CreateAsync(leaveAllocations);
        return Unit.Value;
    }
}