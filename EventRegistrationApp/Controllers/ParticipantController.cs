using EventRegistration.Domain.Models;
using EventRegistration.Infra.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistration.UI.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly IParticipantService _participantService;
        private readonly IEventService _eventService;

        public ParticipantController(IParticipantService participantService, IEventService eventService)
        {
            _participantService = participantService;
            _eventService = eventService;
        }

        // Osavõtja lisamine
        public IActionResult Create(int eventId)
        {
            ViewData["EventId"] = eventId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Participant newParticipant)
        {
            if (ModelState.IsValid)
            {
                await _participantService.AddAsync(newParticipant);
                return RedirectToAction("ManageParticipants", "Event", new { id = newParticipant.EventId });
            }
            return View(newParticipant);
        }

        // Osavõtja detailide vaatamine/muutmine
        public async Task<IActionResult> Edit(int id)
        {
            var participant = await _participantService.GetByIdAsync(id);
            if (participant == null)
            {
                return NotFound();
            }
            return View(participant);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Participant updatedParticipant)
        {
            if (ModelState.IsValid)
            {
                await _participantService.UpdateAsync(updatedParticipant);
                return RedirectToAction("ManageParticipants", "Event", new { id = updatedParticipant.EventId });
            }
            return View(updatedParticipant);
        }

        // Osavõtja kustutamine
        public async Task<IActionResult> Delete(int id)
        {
            var participant = await _participantService.GetByIdAsync(id);
            if (participant == null)
            {
                return NotFound();
            }

            await _participantService.DeleteAsync(id);
            return RedirectToAction("ManageParticipants", "Event", new { id = participant.EventId });
        }
    }
}
