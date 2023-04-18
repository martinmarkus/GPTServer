using GPTServer.Common.Core.Utils.GeneralUtils.Email.DataObjects;
using GPTServer.Common.Core.Utils.GeneralUtils.Email.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace GPTServer.Common.Core.Utils.GeneralUtils.Email;

public class EmailSender : IEmailSender
{
    public async Task SendEmailAsync(
        EmailReceiver emailReceiver,
        MailOptions mailOptions,
        SmtpHost smtpHost)
    {
        MimeMessage mailMessage = new();

        mailMessage.From.Add(new MailboxAddress(
            mailOptions.SenderName,
            mailOptions.SenderEmail));

        mailMessage.To.Add(new MailboxAddress(
            emailReceiver.ReceiverName,
            emailReceiver.ReceiverEmail));

        mailMessage.Subject = mailOptions.Subject;
        mailMessage.Body = new TextPart("html")
        {
            Text = mailOptions.BodyWithHtml
        };

        using SmtpClient smtpClient = new();

        try
        {
            smtpClient.Connect(
                smtpHost.Host,
                int.Parse(smtpHost.Port),
                bool.Parse(smtpHost.EnableSsl));

            smtpClient.Authenticate(
                mailOptions.SenderEmail,
                mailOptions.SenderPassword);

            await smtpClient.SendAsync(mailMessage);
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            smtpClient.Disconnect(true);
        }
    }
}
