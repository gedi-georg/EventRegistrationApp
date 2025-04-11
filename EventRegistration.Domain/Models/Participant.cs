
namespace EventRegistration.Domain.Models;

public class Participant : Entity
{
    public string? AdditionalInfo { get; set; }
    public ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();
}