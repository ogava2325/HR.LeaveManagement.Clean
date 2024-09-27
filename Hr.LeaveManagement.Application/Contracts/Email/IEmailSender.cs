using Hr.LeaveManagement.Application.Models;

namespace Hr.LeaveManagement.Application.Contracts.Email;

public interface IEmailSender
{
    Task<bool> SendEmail(EmailMessage email);
}