using EventRegistration.Application.DTOs;
using EventRegistration.Application.Helpers;
using EventRegistration.Domain.Models;
using EventRegistration.Infra.Interfaces;

namespace EventRegistration.Application.Interfaces
{
    public interface IParticipantService
    {
        Task<ParticipantDto> GetParticipantById(Guid id);
        Task<ParticipantDto> GetParticipantByEventId(Guid id);
        Task AddParticipantAsync(ParticipantDto participantDto);
        Task UpdateParticipant(ParticipantDto participantDto);
        Task DeleteParticipant(Guid id);
    }
}
