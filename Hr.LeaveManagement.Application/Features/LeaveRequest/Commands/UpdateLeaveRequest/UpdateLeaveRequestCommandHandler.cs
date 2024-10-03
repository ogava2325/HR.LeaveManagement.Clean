using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Email;
using Hr.LeaveManagement.Application.Contracts.Logging;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Application.Exceptions;
using Hr.LeaveManagement.Application.Models.Email;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandHandler(
    IMapper mapper,
    IEmailSender emailSender,
    ILeaveTypeRepository leaveTypeRepository,
    ILeaveRequestRepository leaveRequestRepository,
    IAppLogger<UpdateLeaveRequestCommandHandler> appLogger
)
    : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
            throw new NotFoundException(nameof(LeaveRequest), request.Id);


        var validator = new UpdateLeaveRequestCommandValidator(leaveTypeRepository, leaveRequestRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid Leave Request", validationResult);

        mapper.Map(request, leaveRequest);

        await leaveRequestRepository.UpdateAsync(leaveRequest);

        try
        {
            // send confirmation email
            var email = new EmailMessage
            {
                To = string.Empty, /* Get email from employee record */
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " +
                       $"has been updated successfully.",
                Subject = "Leave Request Updated"
            };

            await emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            appLogger.LogWarning(ex.Message);
        }

        return Unit.Value;
    }
}