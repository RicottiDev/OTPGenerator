using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPGenerator.Core.DTOs.SendOTPCode
{
    public class SendOTPCodeRequest
    {
        public string TenantName { get; set; }
        public string DNI { get; set; }
        public string Email { get; set; }
    }
}
