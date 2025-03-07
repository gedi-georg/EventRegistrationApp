using EventRegistration.Domain;

namespace EventRegistration.Infra.Interfaces
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
