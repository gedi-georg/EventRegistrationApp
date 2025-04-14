using EventRegistration.Application.Interfaces;
using EventRegistration.Domain.Models;
using EventRegistration.Infra.Interfaces;

namespace EventRegistration.Application.Services;

public class EventParticipantService : IEventParticipantService
{
    private readonly IEventParticipantRepo _eventParticipantRepo;

    public EventParticipantService(IEventParticipantRepo eventParticipantRepo)
    {
        _eventParticipantRepo = eventParticipantRepo;
    }

    public async Task AddEventParticipantAsync(EventParticipant eventParticipant)
    {
        // Save the EventParticipant to the database
        await _eventParticipantRepo.AddAsync(eventParticipant);
    }
}