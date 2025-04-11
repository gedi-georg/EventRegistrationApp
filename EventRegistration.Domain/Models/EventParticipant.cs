
namespace EventRegistration.Domain.Models;

public class EventParticipant
{
    public Guid EventId { get; set; }
    public Event Event { get; set; } = default!;

    public Guid ParticipantId { get; set; }
    public Participant Participant { get; set; } = default!;

    public Guid PaymentMethodId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}