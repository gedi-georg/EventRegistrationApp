using EventRegistration.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventRegistration.Infra.Interfaces;
using EventRegistration.Application.Helpers;

namespace EventRegistration.Application.Services
{
    public class EventService
    {
        private readonly IEventService _eventService;
        private readonly IParticipantService _participantService;
        private readonly MappingHelper _mappingHelper;

        public EventService(IEventService eventService, MappingHelper mappingHelper, IParticipantService participantService)
        {
            _eventService = eventService;
            _mappingHelper = mappingHelper;
            _participantService = participantService;
        }

        public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
        {
            var events = await _eventService.GetAllAsync();
            var eventDtos = new List<EventDto>();

            foreach (var eventEntity in events)
            {
                eventDtos.Add(_mappingHelper.GetEventDto(eventEntity));
            }
            return eventDtos;
        }

        public async Task<EventDto> GetEventByIdAsync(int id)
        {
            var eventEntity = await _eventService.GetByIdAsync(id);
            return eventEntity == null ? null : _mappingHelper.GetEventDto(eventEntity);
        }

        public async Task AddEventAsync(EventDto eventDto)
        {
            if (eventDto.Date <= DateTime.Now)
                throw new ArgumentException("Event date must be in the future.");

            var eventEntity = _mappingHelper.CreateEventFromDto(eventDto);
            await _eventService.AddAsync(eventEntity);
        }

        public async Task DeleteEventAsync(int id)
        {
            var eventEntity = await _eventService.GetByIdAsync(id);
            if (eventEntity == null) throw new KeyNotFoundException("Event not found");

            await _eventService.DeleteAsync(eventEntity);
        }

        public async Task<IEnumerable<ParticipantDto>> GetParticipantsByEventIdAsync(int eventId)
        {
            var participants = await _participantService.GetByEventIdAsync(eventId);
            var participantDtos = new List<ParticipantDto>();

            foreach (var participant in participants)
            {
                participantDtos.Add(_mappingHelper.GetParticipantDto(participant));
            }
            return participantDtos;
        }
    }
}
