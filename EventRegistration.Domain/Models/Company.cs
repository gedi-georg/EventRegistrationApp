
namespace EventRegistration.Domain.Models;

public class Company : Participant
{
    public string LegalName { get; set; }
    public string RegistrationCode { get; set; }
    public int ParticipantsCount { get; set; }
}