using EventRegistration.Domain.Models;
using EventRegistration.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventRegistration.Infra.Repos;

public class EventRepo : IEventRepo
{
    private readonly ApplicationDbContext _context;

    public EventRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Event?> GetByIdAsync(Guid id)
    {
        return await _context.Events
            .Include(e => e.EventParticipants) // Use EventParticipants instead of Participants
            .ThenInclude(ep => ep.Participant) // Include the Participant navigation property
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        return await _context.Events
            .Include(e => e.EventParticipants)
            .ThenInclude(ep => ep.Participant) // Include Participants through the EventParticipants table
            .ToListAsync();
    }

    public async Task AddAsync(Event? eventEntity)
    {
        if (eventEntity == null)
            throw new ArgumentNullException(nameof(eventEntity));

        await _context.Events.AddAsync(eventEntity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Event? eventEntity)
    {
        if (eventEntity == null)
            throw new ArgumentNullException(nameof(eventEntity));

        _context.Events.Remove(eventEntity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Event? eventEntity)
    {
        if (eventEntity == null)
            throw new ArgumentNullException(nameof(eventEntity));

        _context.Events.Update(eventEntity);
        await _context.SaveChangesAsync();
    }
}