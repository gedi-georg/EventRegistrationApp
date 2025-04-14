using EventRegistration.Domain.Models;

namespace EventRegistration.Application.Interfaces;

public interface IEventParticipantService
{
    Task AddEventParticipantAsync(EventParticipant eventParticipant);
}