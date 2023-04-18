using GPTServer.Common.Core.Utils.GeneralUtils.Email.DataObjects;

namespace GPTServer.Common.Core.Utils.GeneralUtils.Email.Interfaces;

public interface IEmailSender
{
    Task SendEmailAsync(EmailReceiver emailReceiver, MailOptions mailOptions, SmtpHost smtpHost);
}
