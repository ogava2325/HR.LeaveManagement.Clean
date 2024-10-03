using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Email;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Application.Exceptions;
using Hr.LeaveManagement.Application.Models.Email;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler(
    IMapper mapper,
    IEmailSender emailSender,
    ILeaveTypeRepository leaveTypeRepository,
    ILeaveRequestRepository leaveRequestRepository,
    ILeaveAllocationRepository leaveAllocationRepository)
    : IRequestHandler<CreateLeaveRequestCommand, Unit>
{
    public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestCommandValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid Leave Request", validationResult);
        }

        // Create leave request
        var leaveRequest = mapper.Map<Domain.Entities.LeaveRequest>(request);

        await leaveRequestRepository.CreateAsync(leaveRequest);

        // send confirmation email
        try
        {
            var email = new EmailMessage
            {
                To = string.Empty, /* Get email from employee record */
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " +
                       $"has been submitted successfully.",
                Subject = "Leave Request Submitted"
            };

            await emailSender.SendEmail(email);
        }
        catch (Exception)
        {
            //// Log or handle error,
        }

        return Unit.Value;
    }
}