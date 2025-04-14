
namespace EventRegistration.Application.DTOs;

public class ParticipantDto
{
    public Guid Id { get; set; }
    public string PaymentMethodName { get; set; }
    public string AdditionalInfo { get; set; }
    public virtual string FullName { get; }
    public Guid EventId { get; set; }
    public Guid PaymentMethodId { get; set; }
    public virtual string IdCode { get; set; }
}

public class PersonDto : ParticipantDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PersonalIdCode { get; set; }

    public override string FullName => $"{FirstName} {LastName}";
    public override string IdCode => PersonalIdCode;
}


public class CompanyDto : ParticipantDto
{
    public string LegalName { get; set; }
    public string RegistrationCode { get; set; }
    public int? ParticipantsCount { get; set; }

    public override string FullName => LegalName;
    public override string IdCode => RegistrationCode;
}
