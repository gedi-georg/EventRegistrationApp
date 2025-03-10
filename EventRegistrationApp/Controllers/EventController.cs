using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EventRegistration.Application.DTOs;
using EventRegistration.Application.Interfaces;
using EventRegistration.Domain.Models;
using EventRegistration.Infra.Interfaces;
using EventRegistrationApp.Models;

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
        var events = await _eventService.GetAllEventsAsync();
        return View(events);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(EventDto eventDto)
    {
        if (ModelState.IsValid)
        {
            await _eventService.AddEventAsync(eventDto);
            return RedirectToAction(nameof(Index));
        }
        return View(eventDto);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var eventDetails = await _eventService.GetEventByIdAsync(id);
        if (eventDetails == null)
        {
            return NotFound();
        }

        var participants = await _participantService.GetParticipantByEventId(id);
        var viewModel = new EventParticipantsViewModel
        {
            Event = eventDetails,
            Participants = participants.ToList()
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddParticipant(Guid eventId, ParticipantDto participantDto)
    {
        if (ModelState.IsValid)
        {
            participantDto.EventId = eventId;
            await _participantService.AddParticipantAsync(participantDto);
            return RedirectToAction(nameof(Details), new { id = eventId });
        }
        return View(model);
    }
}