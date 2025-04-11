using EventRegistration.Application.DTOs;

namespace EventRegistration.Application.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDto>> GetAllAsync(bool upcoming = false, bool past = false);
        Task<EventDto> GetByIdAsync(Guid id);
        Task AddAsync(EventDto eventDto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<ParticipantDto>> GetParticipantsByEventIdAsync(Guid eventId);
    }
}
