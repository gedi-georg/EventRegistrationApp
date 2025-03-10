using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRegistration.Domain.Models
{
    public class Participant : Entity
    {
        public Guid EventId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string AdditionalInfo { get; set; }
        public bool IsPerson { get; set; }

        // person
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalIdCode { get; set; }

        // company
        public string LegalName { get; set; }
        public string RegistrationCode { get; set; }
        public int ParticipantsCount { get; set; }

        public Event Event { get; set; }
    }

    public enum PaymentMethod
    {
        BankTransfer = 1,
        Cash = 2
    }
}
