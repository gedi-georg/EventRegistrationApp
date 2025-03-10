using System.Collections.Generic;
using System.Threading.Tasks;
using EventRegistration.Application.DTOs;
using EventRegistration.Application.Helpers;
using EventRegistration.Application.Interfaces;
using EventRegistration.Domain.Models;
using EventRegistration.Infra.Interfaces;

namespace EventRegistration.Application.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantRepo _participantRepo;
        private readonly MappingHelper _mappingHelper;

        public ParticipantService(IParticipantRepo participantRepo, MappingHelper mappingHelper)
        {
            _participantRepo = participantRepo;
            _mappingHelper = mappingHelper;
        }

        public async Task<ParticipantDto> GetParticipantById(Guid id)
        {
            var participant = await _participantRepo.GetByIdAsync(id);
            if (participant == null)
                return null;

            return _mappingHelper.GetParticipantDto(participant);
        }

        public async Task<ParticipantDto> GetParticipantByEventId(Guid id)
        {
            var participant = await _participantRepo.GetByEventIdAsync(id);
            if (participant == null)
                return null;

            return _mappingHelper.GetParticipantDto(participant.First());
        }

        public async Task AddParticipantAsync(ParticipantDto participantDto)
        {
            var participant = _mappingHelper.CreateParticipantFromDto(participantDto);
            await _participantRepo.AddAsync(participant);
        }

        public async Task UpdateParticipant(ParticipantDto participantDto)
        {
            var participant = await _participantRepo.GetByIdAsync(participantDto.Id);
            if (participant == null)
                throw new KeyNotFoundException("Participant not found");

            participant.Id = participantDto.Id;
            participant.EventId = participantDto.EventId;
            participant.PaymentMethod = participantDto.PaymentMethod;
            participant.AdditionalInfo = participantDto.AdditionalInfo;
            participant.IsPerson = participantDto.IsPerson;
            participant.FirstName = participantDto.FirstName;
            participant.LastName = participantDto.LastName;
            participant.PersonalIdCode = participantDto.PersonalIdCode;
            participant.LegalName = participantDto.LegalName;
            participant.RegistrationCode = participantDto.RegistrationCode;
            participant.ParticipantsCount = participantDto.ParticipantsCount;

            await _participantRepo.UpdateAsync(participant);
        }

        public async Task DeleteParticipant(Guid id)
        {
            var participant = await _participantRepo.GetByIdAsync(id);
            if (participant == null)
                throw new KeyNotFoundException("Participant not found");

            await _participantRepo.DeleteAsync(participant);
        }
    }
}
