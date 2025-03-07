using EventRegistration.Domain;

namespace EventRegistration.Infra.Interfaces
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
