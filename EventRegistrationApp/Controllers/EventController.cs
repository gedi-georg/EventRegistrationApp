using EventRegistration.Domain.Models;
using EventRegistration.Infra.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistration.UI.Controllers
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

        // Kuvab kõik üritused
        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetAllAsync();
            return View(events); // Edastame andmed vaatesse
        }

        // Ürituse lisamise vaade
        public IActionResult Create()
        {
            return View();
        }

        // Ürituse lisamine POST päring
        [HttpPost]
        public async Task<IActionResult> Create(Event newEvent)
        {
            if (ModelState.IsValid)
            {
                await _eventService.AddAsync(newEvent);
                return RedirectToAction(nameof(Index)); // Ürituse lisamine viib tagasi ürituste loetellu
            }
            return View(newEvent);
        }

        // Ürituse detailide kuvamine
        public async Task<IActionResult> Details(int id)
        {
            var eventDetails = await _eventService.GetByIdAsync(id);
            if (eventDetails == null)
            {
                return NotFound();
            }
            return View(eventDetails);
        }

        // Ürituse kustutamine
        public async Task<IActionResult> Delete(int id)
        {
            await _eventService.DeleteAsync(id);
            return RedirectToAction(nameof(Index)); // Pärast kustutamist suunatakse tagasi loetellu
        }

        // Osavõtjate haldamine
        public async Task<IActionResult> ManageParticipants(int id)
        {
            var eventDetails = await _eventService.GetByIdAsync(id);
            if (eventDetails == null)
            {
                return NotFound();
            }

            var participants = await _participantService.GetParticipantsByEventIdAsync(id);
            return View(participants); // Kuvame osavõtjate nimekirja
        }
    }
}
