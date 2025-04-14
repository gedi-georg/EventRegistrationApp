using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventRegistration.Application.DTOs;

public class ParticipantCreateViewModel
{
    public PersonDto? Person { get; set; } = new();
    public CompanyDto? Company { get; set; } = new();
    //public Guid PaymentMethodId { get; set; }
    //public string? AdditionalInfo { get; set; }
    public ParticipantDto Participant { get; set; } = new();
    public List<SelectListItem> PaymentMethods { get; set; } = new();
    public string ParticipantType { get; set; } = "Person"; // default
}