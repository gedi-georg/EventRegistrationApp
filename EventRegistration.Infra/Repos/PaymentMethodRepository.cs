using EventRegistration.Domain.Models;
using EventRegistration.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventRegistration.Infra.Repos;

public class PaymentMethodRepository : IPaymentMethodRepository
{
    private readonly ApplicationDbContext _context;

    public PaymentMethodRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaymentMethod?> GetByIdAsync(Guid id)
    {
        return await _context.PaymentMethods.FirstAsync(pm => pm.Id == id);
    }

    public async Task<List<PaymentMethod?>> GetAllAsync()
    {
        return await _context.PaymentMethods.ToListAsync();
    }
}
