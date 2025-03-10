using EventRegistration.Domain.Models;

namespace EventRegistration.Infra.Interfaces
{
    public interface IParticipantRepo
    {
        Task<Participant?> GetByIdAsync(Guid id);
        Task<IEnumerable<Participant?>> GetByEventIdAsync(Guid eventId);
        Task AddAsync(Participant? participant);
        Task DeleteAsync(Participant? participant);
        Task UpdateAsync(Participant? participant);
    }
}
