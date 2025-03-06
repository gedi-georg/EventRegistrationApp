using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRegistration.Domain
{
    public class Participant : Entity
    {
        public int EventId { get; set; }
        public int? PersonId { get; set; }
        public int? CompanyId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string AdditionalInfo { get; set; }
        
        public Event Event { get; set; }
        public Person Person { get; set; }
        public Company Company { get; set; }
    }

    public enum PaymentMethod
    {
        BankTransfer = 1,
        Cash = 2
    }
}
