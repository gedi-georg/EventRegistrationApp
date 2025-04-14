namespace EventRegistration.Application.DTOs;

public class EventParticipantsViewModel
{
    public Guid EventId { get; set; }
    public string EventName { get; set; }
    public DateTime EventDate { get; set; }
    public string EventLocation { get; set; }

    public bool IsPastEvent => EventDate < DateTime.Now;

    public List<ParticipantDisplayDto> Participants { get; set; }
}

public class ParticipantDisplayDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string IdCode { get; set; } // Either PersonalIdCode or RegistrationCode
}