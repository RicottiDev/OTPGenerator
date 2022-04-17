using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPGenerator.Core.DTOs.ValidateOTPCode
{
    public class ValidateOTPCodeRequest
    {
        public string TransactionId { get; set; }
        public string OTPCode { get; set; }
    }
}
