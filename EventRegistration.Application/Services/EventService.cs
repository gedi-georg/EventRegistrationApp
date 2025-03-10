using EventRegistration.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventRegistration.Infra.Interfaces;
using EventRegistration.Application.Helpers;
using EventRegistration.Domain.Models;

namespace EventRegistration.Application.Services
{
    public class EventService
    {
        private readonly IEventRepo _eventRepo;
        private readonly IParticipantRepo _participantRepo;
        private readonly MappingHelper _mappingHelper;

        public EventService(IEventRepo eventRepo, MappingHelper mappingHelper, IParticipantRepo participantRepo)
        {
            _eventRepo = eventRepo;
            _mappingHelper = mappingHelper;
            _participantRepo = participantRepo;
        }

        public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
        {
            var events = await _eventRepo.GetAllAsync();
            var eventDtos = new List<EventDto>();

            foreach (var eventEntity in events)
            {
                eventDtos.Add(_mappingHelper.GetEventDto(eventEntity));
            }
            return eventDtos;
        }

        public async Task<EventDto> GetEventByIdAsync(Guid id)
        {
            var eventEntity = await _eventRepo.GetByIdAsync(id);
            return eventEntity == null ? null : _mappingHelper.GetEventDto(eventEntity);
        }

        public async Task AddEventAsync(EventDto eventDto)
        {
            if (eventDto.Date <= DateTime.Now)
                throw new ArgumentException("Event date must be in the future.");

            var eventEntity = _mappingHelper.CreateEventFromDto(eventDto);
            await _eventRepo.AddAsync(eventEntity);
        }

        public async Task DeleteEventAsync(Guid id)
        {
            var eventEntity = await _eventRepo.GetByIdAsync(id);
            if (eventEntity == null) throw new KeyNotFoundException("Event not found");

            await _eventRepo.DeleteAsync(eventEntity);
        }

        public async Task<IEnumerable<ParticipantDto>> GetParticipantsByEventIdAsync(Guid eventId)
        {
            var participants = await _participantRepo.GetByEventIdAsync(eventId);
            var participantDtos = new List<ParticipantDto>();

            foreach (var participant in participants)
            {
                participantDtos.Add(_mappingHelper.GetParticipantDto(participant));
            }
            return participantDtos;
        }
    }
}
