using System.Collections.Generic;
using System.Threading.Tasks;
using EventRegistrationApp.Domain;

namespace EventRegistration.Infrastructure.Interfaces
{
    public interface IEventService
    {
        Task<Event> GetByIdAsync(int id);
        Task<IEnumerable<Event>> GetAllAsync();
        Task AddAsync(Event eventEntity);
        Task DeleteAsync(Event eventEntity);
        Task UpdateAsync(Event eventEntity);
    }
}
