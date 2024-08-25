using MailKit.Security;
using MimeKit;
using NotificationServer.Templates;
using Reapet_Notification.Configurations;
using Shared.Dtos.Emails;

namespace Reapet_Notification.Services;

public class MailService : IMailService
{
    private readonly IConfiguration _configuration;

    public MailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(EmailDtoBody email)
    {
        var configuration = _configuration
            .GetSection("EmailConfiguration")
            .Get<EmailConfiguration>();//GetSection daxilində olan string appsetingsdə olan başlığın adidir,Digər get daxilində olan Email klassinin adidir 

        #region Description
        /*
			var emailMessage = new MimeMessage();
			bu hissədən aşağı olan emailMessage olan hissələr EmailBody klasi üçün olan Proplar üçün yazılıb 
			MimeMessage klassinin verdiyi proplari oz yaratdigimiz klasin proplari beraberlesdirirk
			
			public class EmailBody 
			{
				public string To { get; set; } = null!;
				public string From { get; set; } = null!;
				public string? Bcc { get; set; }
				public string? Cc { get; set; }
				public string Subject { get; set; } = null!;
				public string? Body { get; set; }
				public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
			}

		 */
        #endregion

        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(configuration.DisplayName, configuration.From)); // 
        emailMessage.To.Add(new MailboxAddress("Rifki", email.To));
        if (!string.IsNullOrEmpty(email.Cc))//şərtlər Cc üçün yazilan is nullable üçün yoxlanılır
        {
            emailMessage.Cc.Add(new MailboxAddress("Rifki Cc", email.Cc));
        }
        if (!string.IsNullOrEmpty(email.Bcc))//şərtlər Bcc üçün yazilan is nullable üçün yoxlanılır
        {
            emailMessage.Bcc.Add(new MailboxAddress("Rifki Bcc", email.Bcc));
        }

        emailMessage.Subject = email.Subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = email.Body.Info()
        };

        if (email.Attachments.Count > 0)
        {
            foreach (var attachment in email.Attachments)
            {
                bodyBuilder.Attachments.Add(attachment.FileName, attachment.FileContent);
            }
        }
        emailMessage.Body = bodyBuilder.ToMessageBody();





        using (var client = new MailKit.Net.Smtp.SmtpClient())
        {
            try
            {
                //await client.ConnectAsync(configuration.SmtpServer, configuration.Port, true);//Connect yaradır 
                await client.ConnectAsync(configuration.SmtpServer, configuration.Port, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(configuration.Username, configuration.Password);//Yoxlanış aparır
                await client.SendAsync(emailMessage);//Mesajı göndərir
                await client.DisconnectAsync(true);//disconnect atır
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while sending the email: {ex.Message}");
            }
        }
    }
}
