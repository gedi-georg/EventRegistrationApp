using EventRegistration.Application.DTOs;

namespace EventRegistration.Application.Interfaces;

public interface IParticipantService
{
    Task<ParticipantDto> GetParticipantById(Guid id);
    Task<List<ParticipantDto>> GetParticipantsByEventId(Guid eventId);
    Task AddParticipantAsync(ParticipantDto participantDto);
    Task UpdateParticipant(ParticipantDto participantDto);
    Task DeleteParticipant(Guid id);
}