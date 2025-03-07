using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRegistration.Application.DTOs
{
    public class CompanyDto
    {
        public string LegalName { get; set; }
        public string RegistrationCode { get; set; }
        public int ParticipantsCount { get; set; }
    }
}
