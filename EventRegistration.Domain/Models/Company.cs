using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRegistration.Domain.Models
{
    public class Company : Entity
    {
        public string LegalName { get; set; }
        public string RegistrationCode { get; set; }
        public int ParticipantsCount { get; set; }

        public ICollection<Participant> Participants { get; set; }
    }
}
