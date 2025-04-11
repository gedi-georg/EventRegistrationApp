using EventRegistration.Domain.Models;
using EventRegistration.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventRegistration.Infra.Repos;

public class ParticipantRepo : IParticipantRepo
{
    private readonly ApplicationDbContext _context;

    public ParticipantRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Participant?> GetByIdAsync(Guid id)
    {
        return await _context.Participants
            .Include(p => p.EventParticipants) // Include the EventParticipants relationship
                .ThenInclude(ep => ep.Event) // Include the Event navigation property through EventParticipants
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Participant>> GetByEventIdAsync(Guid eventId)
    {
        return await _context.EventParticipants
            .Where(ep => ep.EventId == eventId) // Filter by EventId
            .Include(ep => ep.Participant) // Include the Participant navigation property
            .Select(ep => ep.Participant) // Select only the Participant
            .ToListAsync();
    }

    public async Task AddAsync(Participant? participant)
    {
        if (participant == null)
            throw new ArgumentNullException(nameof(participant));

        await _context.Participants.AddAsync(participant);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Participant? participant)
    {
        if (participant == null)
            throw new ArgumentNullException(nameof(participant));

        _context.Participants.Remove(participant);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Participant? participant)
    {
        if (participant == null)
            throw new ArgumentNullException(nameof(participant));

        _context.Participants.Update(participant);
        await _context.SaveChangesAsync();
    }
}