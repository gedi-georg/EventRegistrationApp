using System.Text.RegularExpressions;
using AutoMapper;
using EventRegistration.Application.DTOs;
using EventRegistration.Application.Interfaces;
using EventRegistration.Domain.Models;
using EventRegistration.Infra.Interfaces;
using EventRegistration.Infra.Repos;

namespace EventRegistration.Application.Services;

public class ParticipantService : IParticipantService
{
    private readonly IParticipantRepo _participantRepo;
    private readonly IParticipantRepo _eventParticipantRepository;
    private readonly IMapper _mapper;

    public ParticipantService(IParticipantRepo participantRepo, IMapper mapper, IParticipantRepo eventParticipantRepository)
    {
        _participantRepo = participantRepo;
        _mapper = mapper;
        _eventParticipantRepository = eventParticipantRepository;
    }

    public async Task<ParticipantDto> GetParticipantById(Guid id)
    {
        var participant = await _participantRepo.GetByIdAsync(id);
        if (participant == null) return null;

        // Using a switch expression to map different participant types
        return participant switch
        {
            Person person => _mapper.Map<ParticipantDto>(person),
            Company company => _mapper.Map<ParticipantDto>(company),
            _ => throw new NotSupportedException("Unsupported participant type")
        };
    }

    public async Task<List<ParticipantDto>> GetParticipantsByEventId(Guid eventId)
    {
        var participants = await _participantRepo.GetByEventIdAsync(eventId);
        var result = new List<ParticipantDto>();

        foreach (var participant in participants)
        {
            // Using switch expression for polymorphic mapping
            var dto = participant switch
            {
                Person person => _mapper.Map<ParticipantDto>(person),
                Company company => _mapper.Map<ParticipantDto>(company),
                _ => null
            };

            if (dto != null)
                result.Add(dto);
        }

        return result;
    }

    public async Task AddParticipantAsync(ParticipantDto participantDto)
    {
        // Validation for PersonDto only
        if (participantDto is PersonDto personDto)
        {
            if (!IsValidEstonianIdCode(personDto.PersonalIdCode))
                throw new ArgumentException("Invalid Estonian Personal ID code.");

            // Map to Person
            var person = _mapper.Map<Person>(personDto);
            await _participantRepo.AddAsync(person);

            // Add to EventParticipant (linking Participant and Event)
            var eventParticipant = new EventParticipant
            {
                EventId = participantDto.EventId,
                ParticipantId = person.Id,  // Use the 'Id' from the person
                PaymentMethodId = participantDto.PaymentMethodId
            };

            await _eventParticipantRepository.AddAsync(eventParticipant);  // Adding to the EventParticipant repository
        }
        else if (participantDto is CompanyDto companyDto)
        {
            // Map to Company
            var company = _mapper.Map<Company>(companyDto);
            await _participantRepo.AddAsync(company);

            // Add to EventParticipant (linking Participant and Event)
            var eventParticipant = new EventParticipant
            {
                EventId = participantDto.EventId,
                ParticipantId = company.Id,  // Use the 'Id' from the company
                PaymentMethodId = participantDto.PaymentMethodId
            };

            await _eventParticipantRepository.AddAsync(eventParticipant);  // Adding to the EventParticipant repository
        }
        else
        {
            throw new ArgumentException("Invalid participant type.");
        }
    }

    public async Task UpdateParticipant(ParticipantDto participantDto)
    {
        var existing = await _participantRepo.GetByIdAsync(participantDto.Id);
        if (existing == null)
            throw new KeyNotFoundException("Participant not found");

        existing.AdditionalInfo = participantDto.AdditionalInfo;

        // Handle updates for PersonDto (Person) and CompanyDto (Company) separately
        if (participantDto is PersonDto personDto && existing is Person person)
        {
            // Ensure the Estonian ID code is valid for persons
            if (!IsValidEstonianIdCode(personDto.PersonalIdCode))
                throw new ArgumentException("Invalid Estonian Personal ID code.");

            // Update Person-specific properties
            person.FirstName = personDto.FirstName;
            person.LastName = personDto.LastName;
            person.PersonalIdCode = personDto.PersonalIdCode;
        }
        else if (participantDto is CompanyDto companyDto && existing is Company company)
        {
            // Update Company-specific properties
            company.LegalName = companyDto.LegalName;
            company.RegistrationCode = companyDto.RegistrationCode;

            // Optionally update the ParticipantsCount for companies
            company.ParticipantsCount = companyDto.ParticipantsCount ?? 0;
        }

        // Save the updated participant
        await _participantRepo.UpdateAsync(existing);
    }

    public async Task DeleteParticipant(Guid id)
    {
        var participant = await _participantRepo.GetByIdAsync(id);
        if (participant == null)
            throw new KeyNotFoundException("Participant not found");

        await _participantRepo.DeleteAsync(participant);
    }

    // Validation for Estonian Personal ID (e.g., length, format)
    private bool IsValidEstonianIdCode(string code)
    {
        return !string.IsNullOrWhiteSpace(code) && Regex.IsMatch(code, @"^\d{11}$");
    }
}
