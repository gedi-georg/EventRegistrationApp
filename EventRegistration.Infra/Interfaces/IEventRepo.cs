using EventRegistration.Domain.Models;

namespace EventRegistration.Infra.Interfaces
{
    public interface IEventRepo
    {
        Task<Event?> GetByIdAsync(Guid id);
        Task<IEnumerable<Event>> GetAllAsync();
        Task AddAsync(Event? eventEntity);
        Task DeleteAsync(Event? eventEntity);
        Task UpdateAsync(Event? eventEntity);
    }
}
