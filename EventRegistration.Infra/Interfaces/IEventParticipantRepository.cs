using EventRegistration.Domain.Models;

namespace EventRegistration.Infra.Interfaces;

public interface IEventParticipantRepository
{
    Task AddAsync(EventParticipant eventParticipant);
    Task<List<EventParticipant>> GetByEventIdAsync(Guid eventId);
    Task<EventParticipant?> GetByParticipantIdAsync(Guid participantId);
    Task DeleteAsync(Guid eventId, Guid participantId);
}
