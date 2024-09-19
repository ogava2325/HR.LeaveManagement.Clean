using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Application.Exceptions;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    : IRequestHandler<CreateLeaveTypeCommand, int>
{
    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        //validate incoming data
        var validator = new CreateLeaveTypeCommandValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid Leave type", validationResult);
        }

        //convert to domain entity object
        var leaveTypeToCreate = mapper.Map<Domain.Entities.LeaveType>(request);

        //add to database
        await leaveTypeRepository.CreateAsync(leaveTypeToCreate);

        //return record id
        return leaveTypeToCreate.Id;
    }
}