using EventRegistration.Domain.Models;
using EventRegistration.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventRegistration.Infra.Repos
{
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
                .Include(e => e.Participants)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _context.Events
                .Include(e => e.Participants)
                .ToListAsync();
        }

        public async Task AddAsync(Event? eventEntity)
        {
            await _context.Events.AddAsync(eventEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Event? eventEntity)
        {
            _context.Events.Remove(eventEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Event? eventEntity)
        {
            _context.Events.Update(eventEntity);
            await _context.SaveChangesAsync();
        }
    }

}
