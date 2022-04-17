using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPGenerator.Core.Models
{
    public class EmailConfigurationOTP
    {
        public const string ConfigurationSettingName = "EmailConfigurationOTP";
        public string From { get; set; }
        public string SubjectRequestResumeOffline { get; set; }
        public string BodyTemplate { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }
        public bool DefaultCredentials { get; set; }
        public bool UseSsl { get; set; }
    }
}
