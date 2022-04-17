using OTPGenerator.Core.Interfaces.IHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPGenerator.Core.Helpers
{
    public class OTPCodeGenerator : IOTPCodeGenerator
    {
        public string CodeGenerate()
        {
            int min = 000001;
            int max = 999999;
            Random random = new Random();

            return random.Next(min, max).ToString("D6");
        }
    }
}
