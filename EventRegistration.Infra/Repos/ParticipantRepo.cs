using EventRegistration.Domain.Models;
using EventRegistration.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventRegistration.Infra.Repos
{
    public class ParticipantRepo : IParticipantService
    {
        private readonly ApplicationDbContext _context;

        public ParticipantRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Participant> GetByIdAsync(int id)
        {
            return await _context.Participants
                .Include(p => p.Event)
                .Include(p => p.Person)
                .Include(p => p.Company)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Participant>> GetByEventIdAsync(int eventId)
        {
            return await _context.Participants
                .Where(p => p.EventId == eventId)
                .Include(p => p.Person)
                .Include(p => p.Company)
                .ToListAsync();
        }

        public async Task AddAsync(Participant participant)
        {
            await _context.Participants.AddAsync(participant);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Participant participant)
        {
            _context.Participants.Remove(participant);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Participant participant)
        {
            _context.Participants.Update(participant);
            await _context.SaveChangesAsync();
        }
    }

}
