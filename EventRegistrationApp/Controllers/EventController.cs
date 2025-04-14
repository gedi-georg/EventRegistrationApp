using EventRegistration.Application.DTOs;
using EventRegistration.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistrationApp.Controllers;

public class EventController : Controller
{
    private readonly IEventService _eventService;
    private readonly IParticipantService _participantService;

    public EventController(IEventService eventService, IParticipantService participantService)
    {
        _eventService = eventService;
        _participantService = participantService;
    }

    public async Task<IActionResult> Index()
    {
        var events = await _eventService.GetAllAsync();
        return View(events);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(EventDto eventDto)
    {
        if (ModelState.IsValid)
        {
            await _eventService.AddAsync(eventDto);
            return RedirectToAction(nameof(Index));
        }
        return View(eventDto);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var eventDto = await _eventService.GetByIdAsync(id);
        if (eventDto == null) return NotFound();

        var participants = await _participantService.GetParticipantsByEventId(id);

        var participantDtos = participants.Select(p => new ParticipantDisplayDto
        {
            Id = p.Id,
            FullName = p.FullName,
            IdCode = p is PersonDto person
                ? person.PersonalIdCode
                : p is CompanyDto company
                    ? company.RegistrationCode
                    : ""
        }).ToList();

        var viewModel = new EventParticipantsViewModel
        {
            EventId = eventDto.Id,
            EventName = eventDto.Name,
            EventDate = eventDto.Date,
            EventLocation = eventDto.Location,
            Participants = participantDtos
        };

        return View(viewModel);
    }






    public async Task<IActionResult> AddParticipant(Guid eventId)
    {
        var eventDto = await _eventService.GetByIdAsync(eventId);
        return RedirectToPage("Create", "Participant", eventId);
        return NotFound();
    }
    
    public async Task<IActionResult> DeleteParticipant(Guid id)
    {
        var participant = await _participantService.GetParticipantById(id);
        if (participant == null)
        {
            return NotFound();
        }

        await _participantService.DeleteParticipant(id);
        return RedirectToAction(nameof(Details), new { id = participant.EventId });
    }
    
    public async Task<IActionResult> Delete(Guid id)
    {
        var eventDetails = await _eventService.GetByIdAsync(id);
        if (eventDetails == null)
        {
            return NotFound();
        }

        await _eventService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }


}