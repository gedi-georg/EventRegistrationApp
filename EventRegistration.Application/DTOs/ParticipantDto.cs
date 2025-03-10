using EventRegistration.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRegistration.Application.DTOs
{
    public class ParticipantDto
    {
        public Guid Id { get; set; }
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

        //public Participant ToEntity() => new Participant
        //{
        //    Id = new Guid(),
        //    EventId = EventId,
        //    PaymentMethod = PaymentMethod,
        //    AdditionalInfo = AdditionalInfo,
        //    IsPerson = IsPerson,
        //    FirstName = FirstName,
        //    LastName = LastName,
        //    PersonalIdCode = PersonalIdCode,
        //    LegalName = LegalName,
        //    RegistrationCode = RegistrationCode,
        //    ParticipantsCount = ParticipantsCount
        //};
    }
}
