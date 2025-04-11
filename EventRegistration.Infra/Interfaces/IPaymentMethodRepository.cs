using EventRegistration.Domain.Models;

namespace EventRegistration.Infra.Interfaces;

public interface IPaymentMethodRepository
{
    Task<PaymentMethod?> GetByIdAsync(Guid id);
    Task<List<PaymentMethod?>> GetAllAsync();
}