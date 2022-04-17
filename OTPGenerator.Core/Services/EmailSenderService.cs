using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OTPGenerator.Core.Interfaces.IServices;
using OTPGenerator.Core.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OTPGenerator.Core.Services
{
    public class EmailSenderOTPService 
    {
        private readonly EmailConfigurationOTP EmailConfigurationOTP;
        private readonly ILogger<EmailConfigurationOTP> logger;
        public EmailSenderOTPService(IOptions<EmailConfigurationOTP> EmailConfigurationOTP,
                                  ILogger<EmailConfigurationOTP> logger)
        {
            this.EmailConfigurationOTP = EmailConfigurationOTP.Value;
            this.logger = logger;
        }

        public async Task SendEmail(string to, string subjet, string body)
        {
            var email = new MailMessage(EmailConfigurationOTP.From, to)
            {
                Subject = subjet,
                IsBodyHtml = true,
                Body = body
            };

            logger.LogInformation("Sending email");
            logger.LogInformation("To: {to}", to);
            logger.LogInformation("Subject: {subject}", subjet);
            logger.LogDebug("Body: {body}", email.Body);

            using var smtp = new SmtpClient(EmailConfigurationOTP.SmtpHost, EmailConfigurationOTP.SmtpPort);
            if (EmailConfigurationOTP.DefaultCredentials)
            {
                smtp.UseDefaultCredentials = EmailConfigurationOTP.DefaultCredentials;
            }
            else
            {
                smtp.Credentials = new NetworkCredential(EmailConfigurationOTP.SmtpUser, EmailConfigurationOTP.SmtpPass);
            }
            smtp.EnableSsl = EmailConfigurationOTP.UseSsl;

            await smtp.SendMailAsync(email);
            logger.LogDebug("Successful mail delivery");
        }

        //    public async Task SendEmail(string to, string subjet, string body, IList<IFormFile> files)
        //    {
        //        var email = new MailMessage(EmailConfigurationOTP.From, to)
        //        {
        //            Subject = subjet,
        //            IsBodyHtml = true,
        //            Body = body
        //        };

        //        logger.LogInformation("Sending email");
        //        logger.LogInformation("To: {to}", to);
        //        logger.LogInformation("Subject: {subject}", subjet);
        //        logger.LogDebug("Body: {body}", email.Body);

        //        AddAttachments(email, files);

        //        using var smtp = new SmtpClient(EmailConfigurationOTP.SmtpHost, EmailConfigurationOTP.SmtpPort);
        //        if (EmailConfigurationOTP.DefaultCredentials)
        //        {
        //            smtp.UseDefaultCredentials = EmailConfigurationOTP.DefaultCredentials;
        //        }
        //        else
        //        {
        //            smtp.Credentials = new NetworkCredential(EmailConfigurationOTP.SmtpUser, EmailConfigurationOTP.SmtpPass);
        //        }
        //        smtp.EnableSsl = EmailConfigurationOTP.UseSsl;

        //        await smtp.SendMailAsync(email);
        //        logger.LogDebug("Successful mail delivery");

        //    }

        //    private void AddAttachments(MailMessage mailMessage, IList<IFormFile> files)
        //    {
        //        if (files != null)
        //        {
        //            foreach (var file in files)
        //            {
        //                if (file.Length > 0)
        //                {
        //                    using var ms = new MemoryStream();
        //                    ms.Seek(0, SeekOrigin.Begin);
        //                    file.CopyTo(ms);
        //                    var fileBytes = ms.ToArray();
        //                    Attachment att = new Attachment(new MemoryStream(fileBytes), file.FileName);
        //                    mailMessage.Attachments.Add(att);
        //                }
        //            }
        //        }

        //    }
    }

}