
namespace EventRegistration.Application.DTOs;

public class EventDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public string? AdditionalInfo { get; set; }
    //public int ParticipantsCount { get; set; }
    public List<ParticipantDto> Participants { get; set; } = new();
}