using System.Collections.Generic;
using System.Threading.Tasks;
using EventRegistrationApp.Domain;

namespace EventRegistration.Infrastructure.Interfaces
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
