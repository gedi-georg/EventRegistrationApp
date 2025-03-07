using EventRegistration.Domain;

namespace EventRegistration.Infra.Interfaces
{
    public interface IPersonService
    {
        Task<Person> GetByIdAsync(int id);
        Task<IEnumerable<Person>> GetAllAsync();
        Task AddAsync(Person person);
        Task DeleteAsync(Person person);
        Task UpdateAsync(Person person);
    }
}
