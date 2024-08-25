using Shared.Dtos.Emails;

namespace Reapet_Notification.Services;

public interface IMailService
{
    Task SendEmailAsync(EmailDtoBody email);
}
