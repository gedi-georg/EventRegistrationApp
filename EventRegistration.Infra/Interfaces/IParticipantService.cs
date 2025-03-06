using System.Collections.Generic;
using System.Threading.Tasks;
using EventRegistrationApp.Domain;

namespace EventRegistration.Infrastructure.Interfaces
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
