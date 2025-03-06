using System.Collections.Generic;
using System.Threading.Tasks;
using EventRegistrationApp.Domain;

namespace EventRegistration.Infrastructure.Interfaces
{
    public interface ICompanyService
    {
        Task<Company> GetByIdAsync(int id);
        Task<IEnumerable<Company>> GetAllAsync();
        Task AddAsync(Company company);
        Task DeleteAsync(Company company);
        Task UpdateAsync(Company company);
    }
}
