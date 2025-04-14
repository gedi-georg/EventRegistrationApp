using EventRegistration.Application.DTOs;
using EventRegistration.Application.Interfaces;
using EventRegistration.Domain.Models;
using EventRegistration.Infra.Interfaces;
using AutoMapper;
using EventRegistration.Infra;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace EventRegistration.Application.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantRepo _participantRepo;
        private readonly IEventParticipantRepo _eventParticipantRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ParticipantService> _logger;

        public ParticipantService(
            IParticipantRepo participantRepo,
            IMapper mapper,
            IEventParticipantRepo eventParticipantRepository,
            ApplicationDbContext context,
            ILogger<ParticipantService> logger)
        {
            _participantRepo = participantRepo;
            _mapper = mapper;
            _eventParticipantRepository = eventParticipantRepository;
            _context = context;
            _logger = logger;
        }

        public async Task<ParticipantDto> GetParticipantById(Guid id)
        {
            var participant = await _participantRepo.GetByIdAsync(id);
            if (participant == null)
            {
                throw new KeyNotFoundException($"Participant with ID {id} not found.");
            }

            // Map and return the correct DTO (PersonDto or CompanyDto)
            return _mapper.Map<ParticipantDto>(participant);
        }

        public async Task<List<ParticipantDto>> GetParticipantsByEventId(Guid eventId)
        {
            var participants = await _participantRepo.GetByEventIdAsync(eventId);
            return _mapper.Map<List<ParticipantDto>>(participants);
        }

        public async Task AddParticipantAsync(ParticipantDto participantDto)
        {
            participantDto.Id = Guid.NewGuid();
            if (participantDto is PersonDto personDto)
            {
                await AddPersonAsync(personDto);
            }
            else if (participantDto is CompanyDto companyDto)
            {
                await AddCompanyAsync(companyDto);
            }
            else
            {
                throw new ArgumentException("Invalid participant type.");
            }
        }

        public async Task UpdateParticipant(ParticipantDto participantDto)
        {
            // You need to implement logic to update a participant (if required)
            // Make sure to fetch the participant by ID and apply the necessary updates
            throw new NotImplementedException();
        }

        public async Task DeleteParticipant(Guid id)
        {
            var participant = await _participantRepo.GetByIdAsync(id);
            if (participant == null)
            {
                throw new KeyNotFoundException($"Participant with ID {id} not found.");
            }

            // You can also delete related records, such as the EventParticipant entry
            var eventParticipant = await _eventParticipantRepository.GetByParticipantIdAsync(id);
            if (eventParticipant != null)
            {
                await _eventParticipantRepository.DeleteAsync(eventParticipant.EventId);
            }

            // Proceed to delete the participant from the Participant table
            await _participantRepo.DeleteAsync(participant);
            await _context.SaveChangesAsync();
        }

        private async Task AddPersonAsync(PersonDto personDto)
        {
            if (!IsValidEstonianIdCode(personDto.PersonalIdCode))
                throw new ArgumentException("Invalid Estonian Personal ID code.");

            var person = _mapper.Map<Person>(personDto);
            person.FirstName = personDto.FirstName;
            person.LastName = personDto.LastName;
            person.PersonalIdCode = personDto.PersonalIdCode;

            var eventParticipant = new EventParticipant
            {
                EventId = personDto.EventId,
                ParticipantId = person.Id,
                PaymentMethodId = personDto.PaymentMethodId
            };

            // Start transaction to ensure both operations succeed or fail together
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Add person to Participant table
                await _participantRepo.AddAsync(person);

                // Add the relationship to EventParticipant table
                await _eventParticipantRepository.AddAsync(eventParticipant);

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Commit transaction if everything is successful
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                // Rollback transaction in case of an error
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while adding participant.");
                throw;
            }
        }

        private async Task AddCompanyAsync(CompanyDto companyDto)
        {
            var company = _mapper.Map<Company>(companyDto);
            var eventParticipant = new EventParticipant
            {
                EventId = companyDto.EventId,
                ParticipantId = company.Id,
                PaymentMethodId = companyDto.PaymentMethodId
            };

            // Start transaction to ensure both operations succeed or fail together
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Add company to Participant table
                await _participantRepo.AddAsync(company);

                // Add the relationship to EventParticipant table
                await _eventParticipantRepository.AddAsync(eventParticipant);

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Commit transaction if everything is successful
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                // Rollback transaction in case of an error
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while adding company participant.");
                throw;
            }
        }

        // Validate Estonian ID code format (length and numeric check)
        private bool IsValidEstonianIdCode(string code)
        {
            return !string.IsNullOrWhiteSpace(code) && Regex.IsMatch(code, @"^\d{11}$");
        }
    }
}
