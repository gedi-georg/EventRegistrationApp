using EventRegistration.Domain.Models;

namespace EventRegistration.Infra.Interfaces
{
    public interface IParticipantService
    {
        Task<Participant> GetByIdAsync(int id);
        Task<IEnumerable<Participant>> GetByEventIdAsync(int eventId);
        Task AddAsync(Participant participant);
        Task DeleteAsync(Participant participant);
        Task UpdateAsync(Participant participant);
    }
}
