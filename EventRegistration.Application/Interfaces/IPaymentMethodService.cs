using EventRegistration.Domain.Models;

namespace EventRegistration.Application.Interfaces;

public interface IPaymentMethodService
{
    Task<PaymentMethod?> GetByIdAsync(Guid id);
    Task<List<PaymentMethod>> GetAllAsync();
}