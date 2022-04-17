using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OTPGenerator.Core.Interfaces.IServices
{
    public interface IEmailSenderService
    {
        Task SendEmail(string to, string subject, string body, IList<IFormFile> files);
        Task SendEmail(string to, string subject, string body);
    }
}
