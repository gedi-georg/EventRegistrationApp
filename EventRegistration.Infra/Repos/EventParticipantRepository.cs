using EventRegistration.Domain.Models;
using EventRegistration.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventRegistration.Infra.Repos;

public class EventParticipantRepository : IEventParticipantRepository
{
    private readonly ApplicationDbContext _context;

    public EventParticipantRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(EventParticipant eventParticipant)
    {
        await _context.EventParticipants.AddAsync(eventParticipant);
        await _context.SaveChangesAsync();
    }

    public async Task<List<EventParticipant>> GetByEventIdAsync(Guid eventId)
    {
        return await _context.EventParticipants
            .Include(ep => ep.Participant)
            .Include(ep => ep.PaymentMethod)
            .Where(ep => ep.EventId == eventId)
            .ToListAsync();
    }

    public async Task<EventParticipant?> GetByParticipantIdAsync(Guid participantId)
    {
        return await _context.EventParticipants
            .FirstOrDefaultAsync(ep => ep.ParticipantId == participantId);
    }

    public async Task DeleteAsync(Guid eventId, Guid participantId)
    {
        var ep = await _context.EventParticipants
            .FirstOrDefaultAsync(e => e.EventId == eventId && e.ParticipantId == participantId);

        if (ep != null)
        {
            _context.EventParticipants.Remove(ep);
            await _context.SaveChangesAsync();
        }
    }
}