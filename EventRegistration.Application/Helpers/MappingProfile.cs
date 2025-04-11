using AutoMapper;
using EventRegistration.Application.DTOs;
using EventRegistration.Domain.Models;

namespace EventRegistration.Application.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventDto>().ReverseMap();

            CreateMap<Person, ParticipantDto>().ReverseMap();
            CreateMap<Company, ParticipantDto>().ReverseMap();

            CreateMap<EventParticipant, ParticipantDto>()
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dest => dest.PaymentMethodId, opt => opt.MapFrom(src => src.PaymentMethodId))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => GetParticipantFullName(src.Participant)));


        }

        private static string GetParticipantFullName(Participant participant)
        {
            return participant switch
            {
                Person p => $"{p.FirstName} {p.LastName}",
                Company c => c.LegalName,
                _ => "Unknown"
            };
        }

    }
}