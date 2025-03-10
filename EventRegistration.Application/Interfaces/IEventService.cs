using EventRegistration.Application.DTOs;
using EventRegistration.Application.Helpers;

namespace EventRegistration.Application.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDto>> GetAllEventsAsync();
        Task<EventDto> GetEventByIdAsync(Guid id);
        Task AddEventAsync(EventDto eventDto);
        Task DeleteEventAsync(Guid id);
        Task<IEnumerable<ParticipantDto>> GetParticipantsByEventIdAsync(Guid eventId);
    }
}
