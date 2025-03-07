using EventRegistration.Application.DTOs;
using EventRegistration.Application.Helpers;
using EventRegistration.Domain;
using EventRegistration.Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRegistration.Application.Services
{
    public class ParticipantService
    {
        private readonly IParticipantService _participantService;
        private readonly IPersonService _personService;
        private readonly ICompanyService _companyService;
        private readonly MappingHelper _mappingHelper;

        public ParticipantService(IParticipantService participantService, IPersonService personService, ICompanyService companyService, MappingHelper mappingHelper)
        {
            _participantService = participantService;
            _personService = personService;
            _companyService = companyService;
            _mappingHelper = mappingHelper;
        }

        public async Task AddParticipantAsync(ParticipantDto participantDto)
        {
            var eventEntity = await _participantService.GetByIdAsync(participantDto.EventId);
            if (eventEntity == null) throw new KeyNotFoundException("Event not found");

            var participant = _mappingHelper.CreateParticipantFromDto(participantDto);

            if (participantDto.Person != null)
            {
                if (!ValidationHelper.IsValidPersonalId(participantDto.Person.PersonalIdCode))
                    throw new ArgumentException("Invalid personal ID code");

                var person = _mappingHelper.CreatePersonFromDto(participantDto.Person);
                await _personService.AddAsync(person);
                participant.PersonId = person.Id;
            }

            if (participantDto.Company != null)
            {
                var company = _mappingHelper.CreateCompanyFromDto(participantDto.Company);
                await _companyService.AddAsync(company);
                participant.CompanyId = company.Id;
            }

            await _participantService.AddAsync(participant);
        }
    }
}
