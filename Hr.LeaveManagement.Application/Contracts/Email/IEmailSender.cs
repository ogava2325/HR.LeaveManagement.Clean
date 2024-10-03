using Hr.LeaveManagement.Application.Models;
using Hr.LeaveManagement.Application.Models.Email;

namespace Hr.LeaveManagement.Application.Contracts.Email;

public interface IEmailSender
{
    Task<bool> SendEmail(EmailMessage email);
}