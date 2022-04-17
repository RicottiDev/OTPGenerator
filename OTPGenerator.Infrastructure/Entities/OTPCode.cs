using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPGenerator.Infrastructure.Entities
{
    public class OTPCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string TransactionId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Dni { get; set; }
        public DateTime CreateDate { get; set; }
        public int Attempts { get; set; }
        public Tenant Tenant { get; set; }
    }
}
