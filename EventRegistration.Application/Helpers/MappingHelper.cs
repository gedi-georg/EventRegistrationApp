using EventRegistration.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EventRegistration.Domain.Models;

namespace EventRegistration.Application.Helpers
{
    public class MappingHelper
    {
        private readonly IMapper _mapper;

        public MappingHelper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public EventDto GetEventDto(Event eventEntity)
        {
            return _mapper.Map<EventDto>(eventEntity);
        }

        public Event CreateEventFromDto(EventDto eventDto)
        {
            return _mapper.Map<Event>(eventDto);
        }

        public ParticipantDto GetParticipantDto(Participant participantEntity)
        {
            return _mapper.Map<ParticipantDto>(participantEntity);
        }

        public Participant CreateParticipantFromDto(ParticipantDto participantDto)
        {
            return _mapper.Map<Participant>(participantDto);
        }

        public PersonDto GetPersonDto(Person personEntity)
        {
            return _mapper.Map<PersonDto>(personEntity);
        }

        public Person CreatePersonFromDto(PersonDto personDto)
        {
            return _mapper.Map<Person>(personDto);
        }

        public CompanyDto GetCompanyDto(Company companyEntity)
        {
            return _mapper.Map<CompanyDto>(companyEntity);
        }

        public Company CreateCompanyFromDto(CompanyDto companyDto)
        {
            return _mapper.Map<Company>(companyDto);
        }

    }
}
