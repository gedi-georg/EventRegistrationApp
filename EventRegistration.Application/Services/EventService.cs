using AutoMapper;
using EventRegistration.Application.DTOs;
using EventRegistration.Application.Interfaces;
using EventRegistration.Domain.Models;
using EventRegistration.Infra.Interfaces;

namespace EventRegistration.Application.Services;

public class EventService : IEventService
{
    private readonly IEventRepo _eventRepo;
    private readonly IParticipantRepo _participantRepo;
    private readonly IEventParticipantRepo _eventParticipantRepo;
    private readonly IMapper _mapper;

    public EventService(IEventRepo eventRepo, IParticipantRepo participantRepo, IMapper mapper, IEventParticipantRepo eventParticipantRepo)
    {
        _eventRepo = eventRepo;
        _participantRepo = participantRepo;
        _mapper = mapper;
        _eventParticipantRepo = eventParticipantRepo;
    }

    public async Task<IEnumerable<EventDto>> GetAllAsync(bool upcoming = false, bool past = false)
    {
        var events = await _eventRepo.GetAllAsync();

        var now = DateTime.Now;
        if (upcoming)
            events = events.Where(e => e.Date > now);
        else if (past)
            events = events.Where(e => e.Date < now);

        return _mapper.Map<IEnumerable<EventDto>>(events.OrderByDescending(e => e.Date));
    }

    public async Task<EventDto> GetByIdAsync(Guid id)
    {
        var eventEntity = await _eventRepo.GetByIdAsync(id);
        return eventEntity == null ? null : _mapper.Map<EventDto>(eventEntity);
    }

    public async Task AddAsync(EventDto eventDto)
    {
        if (eventDto.Date <= DateTime.Now)
            throw new ArgumentException("Event date must be in the future.");

        var eventEntity = _mapper.Map<Event>(eventDto);
        await _eventRepo.AddAsync(eventEntity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var eventEntity = await _eventRepo.GetByIdAsync(id);
        if (eventEntity == null)
            throw new KeyNotFoundException("Event not found");

        // Kustuta seotud osalejad (EventParticipant kirjed)
        var participants = await _eventParticipantRepo.GetByEventIdAsync(id);
        foreach (var participant in participants)
        {
            await _eventParticipantRepo.DeleteAsync(participant.EventId);
        }

        await _eventRepo.DeleteAsync(eventEntity);
    }


    public async Task<IEnumerable<ParticipantDto>> GetParticipantsByEventIdAsync(Guid eventId)
    {
        var participants = await _participantRepo.GetByEventIdAsync(eventId);
        return _mapper.Map<IEnumerable<ParticipantDto>>(participants);
    }
}