using EventRegistration.Application.Interfaces;
using EventRegistration.Domain.Models;
using EventRegistration.Infra.Interfaces;

namespace EventRegistration.Application.Services;

public class PaymentMethodService : IPaymentMethodService
{
    private readonly IPaymentMethodRepository _paymentMethodRepository;

    public PaymentMethodService(IPaymentMethodRepository paymentMethodRepository)
    {
        _paymentMethodRepository = paymentMethodRepository;
    }

    public async Task<PaymentMethod?> GetByIdAsync(Guid id)
    {
        return await _paymentMethodRepository.GetByIdAsync(id);
    }

    public async Task<List<PaymentMethod>> GetAllAsync()
    {
        return await _paymentMethodRepository.GetAllAsync();
    }
}
