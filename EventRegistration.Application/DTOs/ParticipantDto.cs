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
        public int EventId { get; set; }
        public PersonDto Person { get; set; }
        public CompanyDto Company { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string AdditionalInfo { get; set; }

        public Participant ToEntity() => new Participant
        {
            EventId = EventId,
            PaymentMethod = PaymentMethod,
            AdditionalInfo = AdditionalInfo
        };
    }
}
