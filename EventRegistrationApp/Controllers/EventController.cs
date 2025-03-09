using EventRegistration.Domain.Models;
using EventRegistration.Infra.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistrationApp.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IParticipantService _participantService;

        public EventController(IEventService eventService, IParticipantService participantService)
        {
            _eventService = eventService;
            _participantService = participantService;
        }

        // Avaleht - Ürituste nimekiri
        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetAllAsync();
            return View(events);
        }

        // Lisa Üritus - Ürituse lisamise leht
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Event newEvent)
        {
            if (ModelState.IsValid)
            {
                await _eventService.AddAsync(newEvent);
                return RedirectToAction(nameof(Index));
            }
            return View(newEvent);
        }

        // Osalejate nimekiri + Osaleja lisamise vorm
        public async Task<IActionResult> ManageParticipants(int id)
        {
            var eventDetails = await _eventService.GetByIdAsync(id);
            if (eventDetails == null)
            {
                return NotFound();
            }

            var participants = await _participantService.GetByEventIdAsync(id);
            ViewData["EventId"] = id;
            return View(participants);
        }

        // Osavõtja detail - Osaleja andmete muutmine
        public async Task<IActionResult> ParticipantDetails(int id)
        {
            var participant = await _participantService.GetByIdAsync(id);
            if (participant == null)
            {
                return NotFound();
            }
            return View(participant);
        }

        [HttpPost]
        public async Task<IActionResult> ParticipantDetails(Participant participant)
        {
            if (ModelState.IsValid)
            {
                await _participantService.UpdateAsync(participant);
                return RedirectToAction(nameof(ManageParticipants), new { id = participant.EventId });
            }
            return View(participant);
        }

        // Ürituse kustutamine
        public async Task<IActionResult> Delete(int id)
        {
            var eventToDelete = await _eventService.GetByIdAsync(id);
            if (eventToDelete == null)
            {
                return NotFound();
            }
            await _eventService.DeleteAsync(eventToDelete);
            return RedirectToAction(nameof(Index));
        }
    }
}
