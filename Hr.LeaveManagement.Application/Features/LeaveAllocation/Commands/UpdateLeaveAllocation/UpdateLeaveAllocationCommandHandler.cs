using AutoMapper;
using FluentValidation;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Application.Exceptions;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandHandler(
    IMapper mapper,
    ILeaveTypeRepository leaveTypeRepository,
    ILeaveAllocationRepository leaveAllocationRepository) : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveAllocationCommandValidator(leaveTypeRepository, leaveAllocationRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid Leave Allocation", validationResult);

        var leaveAllocation = await leaveAllocationRepository.GetByIdAsync(request.Id);

        if (leaveAllocation is null)
            throw new NotFoundException(nameof(LeaveAllocation), request.Id);

        mapper.Map(request, leaveAllocation);

        await leaveAllocationRepository.UpdateAsync(leaveAllocation);
        return Unit.Value;
    }
}